using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Timers;
using System.Windows.Forms;
using Newtonsoft.Json;
using Exception = System.Exception;
using Timer = System.Timers.Timer;

namespace MusicBeePlugin
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public partial class Plugin
    {
        private const long UpdateIntervalMs = 100L;
        private const string SettingsFileName = "desktopLyrics.json";
        private const string SettingsFileName2 = "desktopLyrics_Window.set";

        // MB related
        private MusicBeeApiInterface _mbApiInterface;
        private readonly PluginInfo _about = new PluginInfo();
        private string SettingsPath => Path.Combine(_mbApiInterface.Setting_GetPersistentStoragePath(), SettingsFileName);
        public string SettingsPath2 => Path.Combine(_mbApiInterface.Setting_GetPersistentStoragePath(), SettingsFileName2);
        // Customed
        private volatile SettingsObj _settings;
        private volatile FrmLyrics _frmLyrics;
        private Timer _timer;
        private LyricsController _lyricsCtrl;
        private object _lock = new object();

        public PluginInfo Initialise(IntPtr apiInterfacePtr)
        {
            var versions = Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');

            _mbApiInterface = new MusicBeeApiInterface();
            _mbApiInterface.Initialise(apiInterfacePtr);
            _about.PluginInfoVersion = PluginInfoVersion;
            _about.Name = "Desktop Lyrics";
            _about.Description = "Display lyrics on your desktop!";
            _about.Author = "Charlie Jiang";
            _about.TargetApplication = "";   // current only applies to artwork, lyrics or instant messenger name that appears in the provider drop down selector or target Instant Messenger
            _about.Type = PluginType.General;
            _about.VersionMajor = short.Parse(versions[0]);  // your plugin version
            _about.VersionMinor = short.Parse(versions[1]);
            _about.Revision = short.Parse(versions[2]);
            _about.MinInterfaceVersion = MinInterfaceVersion;
            _about.MinApiRevision = MinApiRevision;
            _about.ReceiveNotifications = ReceiveNotificationFlags.PlayerEvents;
            _about.ConfigurationPanelHeight = 32;   // height in pixels that musicbee should reserve in a panel for config settings. When set, a handle to an empty panel will be passed to the Configure function

            _lyricsCtrl = new LyricsController(_mbApiInterface);
            return _about;
        }

        public bool Configure(IntPtr panelHandle)
        {
            if (panelHandle == IntPtr.Zero) return false;
            var configPanel = (Panel) Control.FromHandle(panelHandle);
            var btnSettings = new Button
            {
                Location = new Point(0, 0),
                Size = new Size(150, 30),
                Text = @"Settings"
            };
            btnSettings.Click += (sender, args) =>
            {
                var settingsForm = new FrmSettings(_settings);
                settingsForm.ShowDialog();
                SaveSettings(_settings);
                _frmLyrics?.UpdateFromSettings(_settings);
                LyricParser.PreserveSlash = _settings.PreserveSlash;
            };
            configPanel.Controls.Add(btnSettings);

            return false;
        }

        private void SaveSettings(SettingsObj settings)
        {
            File.WriteAllText(SettingsPath, JsonConvert.SerializeObject(settings));
        }

        public void SaveSettings()
        {
            // Still, we've saved all settings already.
        }

        // MusicBee is closing the plugin (plugin is being disabled by user or MusicBee is shutting down)
        public void Close(PluginCloseReason reason)
        {
            _timer.Stop();
            var ctrl = Control.FromHandle(_mbApiInterface.MB_GetWindowHandle());
            _frmLyrics?.Invoke(new Action(() =>
            {
                _frmLyrics?.Dispose();
                _frmLyrics = null;
            }));
            SaveSettings(_settings);
        }
        
        public void Uninstall()
        {
            if (File.Exists(SettingsPath)) File.Delete(SettingsPath);
            if (File.Exists(SettingsPath2)) File.Delete(SettingsPath2); 
        }

        public void ReceiveNotification(string sourceFileUrl, NotificationType type)
        {
            // perform some action depending on the notification type
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (type)
            {
                case NotificationType.PluginStartup:
                    // while (!Debugger.IsAttached) System.Threading.Thread.Sleep(1);
                    try
                    {
                        try
                        {
                            _settings = JsonConvert.DeserializeObject<SettingsObj>(File.ReadAllText(SettingsPath));
                        }
                        catch (Exception)
                        {
                            _settings = new SettingsObj
                            {
                                BorderColor = Color.Black,
                                Color1 = Color.GhostWhite,
                                Color2 = Color.LightGray,
                                FontActual = new Font(FontFamily.GenericSansSerif, 34.0f, FontStyle.Regular, GraphicsUnit.Point),
                                GradientType = 1
                            };
                        }

                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);

                        StartupMenuItem();
                        if (!_settings.HideOnStartup)
                            StartupForm();

                        if (_timer != null && _timer.Enabled) _timer.Enabled = false;
                        _timer = new Timer(UpdateIntervalMs) {AutoReset = false};
                        _timer.Elapsed += TimerTick;
                        _timer.Start();
                    }
                    catch (Exception e)
                    {
                        _mbApiInterface.MB_Trace(e.ToString());
                    }
                    break;
                case NotificationType.PlayStateChanged:
                    UpdatePlayState(_mbApiInterface.Player_GetPlayState());
                    break;
                case NotificationType.NowPlayingLyricsReady:
                    try
                    {
                        UpdateLyrics();
                    }
                    catch (Exception e)
                    {
                        _mbApiInterface.MB_Trace(e.ToString());
                    }
                    break;
                case NotificationType.TagsChanged:
                    try
                    {
                        UpdateLyrics();
                    }
                    catch (Exception e)
                    {
                        _mbApiInterface.MB_Trace(e.ToString());
                    }
                    break;
            }
        }

        // Change form according to play state
        private void UpdatePlayState(PlayState state)
        {
            if (!_settings.AutoHide) return;
            if (_frmLyrics == null) return;
            switch (state)
            {
                case PlayState.Stopped:
                    _frmLyrics.Visible = false;
                    break;
                case PlayState.Playing:
                    _frmLyrics.Visible = true;
                    break;
            }
        }

        private void StartupMenuItem()
        {
            var menuItem = (ToolStripMenuItem) _mbApiInterface.MB_AddMenuItem("mnuView/Desktop Lyrics", "Toggle Desktop Lyrics visibility.",
                (sender, args) =>
                { });
            menuItem.CheckOnClick = true;
            menuItem.Checked = !_settings.HideOnStartup;
            menuItem.CheckedChanged += (sender, args) =>
            {
                if (!menuItem.Checked)
                    _frmLyrics?.Dispose();
                else
                    StartupForm();
                _settings.HideOnStartup = !menuItem.Checked;
                SaveSettings(_settings);
            };
        }

        private void StartupForm()
        {
            _frmLyrics?.Dispose();
            var f = (Form)Control.FromHandle(_mbApiInterface.MB_GetWindowHandle());
            f.Invoke(new Action(() =>
            {
                _frmLyrics = new FrmLyrics(_settings);
                _frmLyrics.Show();
            }));
        }

        private void TimerTick(object sender, ElapsedEventArgs args)
        {
            Debug.Assert(sender is Timer);
            try
            {
                UpdateLyrics();
            }
            catch (Exception e)
            {
                _mbApiInterface.MB_Trace(e.ToString());
            }
            ((Timer) sender).Start();
        }

        private string _line1, _line2;
        private void UpdateLyrics()
        {
            lock (_lock)
            {
                if (_frmLyrics == null) return;
                var entry = _lyricsCtrl.UpdateLyrics();
                if (entry == null && _line1 != "")
                {
                    _line1 = "";
                    _frmLyrics.Clear();
                    return;
                }

                if (entry == null) return;
                if (entry.LyricLine1 == _line1 && entry.LyricLine2 == _line2) return;
                _frmLyrics.BeginInvoke(new Action<string, string>((line1, line2) => _frmLyrics.UpdateLyrics(line1, line2)), entry.LyricLine1, entry.LyricLine2);
                _line1 = entry.LyricLine1;
                _line2 = entry.LyricLine2;
            }
        }

        public string[] GetProviders() { return null; }
   }
}