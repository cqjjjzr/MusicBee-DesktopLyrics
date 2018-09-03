using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Timers;
using System.Windows.Forms;
using Newtonsoft.Json;
using Exception = System.Exception;
using Timer = System.Timers.Timer;

namespace MusicBeePlugin
{
    public partial class Plugin
    {
        private const long UpdateIntervalMs = 300L;
        private const string SettingsFileName = "desktopLyrics.json";
        private const string SettingsFileName2 = "desktopLyrics_Window.set";

        // MB related
        private MusicBeeApiInterface mbApiInterface;
        private readonly PluginInfo about = new PluginInfo();
        private string SettingsPath => Path.Combine(mbApiInterface.Setting_GetPersistentStoragePath(), SettingsFileName);
        public string SettingsPath2 => Path.Combine(mbApiInterface.Setting_GetPersistentStoragePath(), SettingsFileName2);
        // Customed
        private volatile SettingsObj _settings;
        private volatile FrmLyrics _frmLyrics;
        private Timer _timer;
        private LyricsController _lyricsCtrl;

        //private 

        public PluginInfo Initialise(IntPtr apiInterfacePtr)
        {
            mbApiInterface = new MusicBeeApiInterface();
            mbApiInterface.Initialise(apiInterfacePtr);
            about.PluginInfoVersion = PluginInfoVersion;
            about.Name = "Desktop Lyrics";
            about.Description = "Display lyrics on your desktop!";
            about.Author = "Charlie Jiang";
            about.TargetApplication = "";   // current only applies to artwork, lyrics or instant messenger name that appears in the provider drop down selector or target Instant Messenger
            about.Type = PluginType.General;
            about.VersionMajor = 1;  // your plugin version
            about.VersionMinor = 0;
            about.Revision = 1;
            about.MinInterfaceVersion = MinInterfaceVersion;
            about.MinApiRevision = MinApiRevision;
            about.ReceiveNotifications = ReceiveNotificationFlags.PlayerEvents;
            about.ConfigurationPanelHeight = 32;   // height in pixels that musicbee should reserve in a panel for config settings. When set, a handle to an empty panel will be passed to the Configure function

            _lyricsCtrl = new LyricsController(mbApiInterface);
            return about;
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
                var settingsForm = new FrmSettings(SettingsPath);
                settingsForm.ShowDialog();
                _settings = settingsForm.Settings; 
                File.WriteAllText(SettingsPath, JsonConvert.SerializeObject(_settings));
                _frmLyrics?.UpdateFromSettings(_settings);
            };
            configPanel.Controls.Add(btnSettings);

            return false;
        }
       
        public void SaveSettings()
        {
            // we've already saved our settings, so no need to do anything here.
        }

        // MusicBee is closing the plugin (plugin is being disabled by user or MusicBee is shutting down)
        public void Close(PluginCloseReason reason)
        {
            _timer.Stop();
            Control.FromHandle(mbApiInterface.MB_GetWindowHandle()).Invoke(new Action(() =>
            {
                _frmLyrics?.Dispose();
                _frmLyrics = null;
            }));
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
                    // while (!Debugger.IsAttached) Thread.Sleep(1);
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

                        _frmLyrics?.Dispose();
                        var f = (Form) Control.FromHandle(mbApiInterface.MB_GetWindowHandle());
                        f.Invoke(new Action(() =>
                        {
                            _frmLyrics = new FrmLyrics(_settings, SettingsPath2, f);
                            _frmLyrics.Show();
                        }));

                        if (_timer != null && _timer.Enabled) _timer.Enabled = false;
                        _timer = new Timer(UpdateIntervalMs) {AutoReset = false};
                        _timer.Elapsed += TimerTick;
                        _timer.Start();
                    }
                    catch (Exception e)
                    {
                        mbApiInterface.MB_Trace(e.ToString());
                    }
                    break;
            }
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
                mbApiInterface.MB_Trace(e.ToString());
            }
            ((Timer) sender).Start();
        }

        private string _line1, _line2;
        private void UpdateLyrics()
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

        public string[] GetProviders() { return null; }
   }
}