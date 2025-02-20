using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;
using Cyotek.Windows.Forms;
using Newtonsoft.Json;

namespace MusicBeePlugin
{
    [SuppressMessage("ReSharper", "LocalizableElement")]
    public partial class FrmSettings : Form
    {
        private Font _font = DefaultFont;
        private readonly SettingsObj _settings;
        private readonly ColorPickerDialog _colorDialog = new ColorPickerDialog();

        public event EventHandler<SettingsObj> SettingsChanged;

        public FrmSettings(SettingsObj settings)
        {
            _settings = settings;
            InitializeComponent();
            comboBoxGradientType.SelectedIndex = 0;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            _font = _settings.Font;
            try
            {
                btnFont.Text = _font.Name + " "+ _font.Size;
                comboBoxGradientType.SelectedIndex = _settings.GradientType;
                comboBoxAlignment.SelectedIndex = _settings.AlignmentType;
                btnColor1.BackColor = _settings.Color1;
                btnColor2.BackColor = _settings.Color2;
                btnBorderColor.BackColor = _settings.BorderColor;
                barBackgroundOpacity.Value = _settings.BackgroundOpacity;
                checkBoxPreserveSlash.Checked = _settings.PreserveSlash;
                checkBoxAutoHide.Checked = _settings.AutoHide;
                checkBoxNextLineWhenNoTranslation.Checked = _settings.NextLineWhenNoTranslation;
                checkBoxHideWhenUnavailable.Checked = _settings.HideWhenUnavailable;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            dlgFont.Font = _font;
            var res = dlgFont.ShowDialog();
            if (res != DialogResult.OK && res != DialogResult.Yes) return;
            var font = dlgFont.Font;
            // Force point unit
            _font = new Font(font.FontFamily, font.Size, font.Style, GraphicsUnit.Point, font.GdiCharSet);
            btnFont.Text = _font.Name + " " + _font.Size;
            _settings.Font = _font;
            SettingsChanged?.Invoke(this, _settings);
        }

        private void btnColors_Click(object sender, EventArgs e)
        {
            var button = (Button) sender;
            _colorDialog.Color = button.BackColor;
            var res = _colorDialog.ShowDialog();
            if (res == DialogResult.OK || res == DialogResult.Yes)
                button.BackColor = _colorDialog.Color;
            _settings.Color1 = btnColor1.BackColor;
            _settings.Color2 = btnColor2.BackColor;
            _settings.BorderColor = btnBorderColor.BackColor;
            SettingsChanged?.Invoke(this, _settings);
        }

        private void checkBoxPreserveSlash_CheckedChanged(object sender, EventArgs e)
        {
            _settings.PreserveSlash = checkBoxPreserveSlash.Checked;
            SettingsChanged?.Invoke(this, _settings);
        }

        private void checkBoxAutoHide_CheckedChanged(object sender, EventArgs e)
        {
            _settings.AutoHide = checkBoxAutoHide.Checked;
            SettingsChanged?.Invoke(this, _settings);
        }

        private void checkBoxNextLineWhenNoTranslation_CheckedChanged(object sender, EventArgs e)
        {
            _settings.NextLineWhenNoTranslation = checkBoxNextLineWhenNoTranslation.Checked;
            SettingsChanged?.Invoke(this, _settings);
        }

        private void barBackgroundOpacity_Scroll(object sender, EventArgs e)
        {
            _settings.BackgroundOpacity = barBackgroundOpacity.Value;
            SettingsChanged?.Invoke(this, _settings);
        }

        private void comboBoxGradientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _settings.GradientType = comboBoxGradientType.SelectedIndex;
            SettingsChanged?.Invoke(this, _settings);
        }

        private void comboBoxAlignment_SelectedIndexChanged(object sender, EventArgs e)
        {
            _settings.AlignmentType = comboBoxAlignment.SelectedIndex;
            SettingsChanged?.Invoke(this, _settings);
        }

        private void checkBoxHideWhenUnavailable_CheckedChanged(object sender, EventArgs e)
        {
            _settings.HideWhenUnavailable = checkBoxHideWhenUnavailable.Checked;
            SettingsChanged?.Invoke(this, _settings);
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            _settings.Font = _font;
            _settings.Color1 = btnColor1.BackColor;
            _settings.Color2 = btnColor2.BackColor;
            _settings.BorderColor = btnBorderColor.BackColor;
            _settings.GradientType = comboBoxGradientType.SelectedIndex;
            _settings.AlignmentType = comboBoxAlignment.SelectedIndex;
            _settings.BackgroundOpacity = barBackgroundOpacity.Value;
            _settings.PreserveSlash = checkBoxPreserveSlash.Checked;
            _settings.AutoHide = checkBoxAutoHide.Checked;
            _settings.HideWhenUnavailable = checkBoxHideWhenUnavailable.Checked;
        }
    }

    public class SettingsObj
    {
        public Color Color1;
        public Color Color2;
        public Color BorderColor;
        public int GradientType;
        public int AlignmentType;
        public int BackgroundOpacity;
        public bool PreserveSlash;
        public bool AutoHide;
        public bool NextLineWhenNoTranslation;
        public bool HideOnStartup;
        public bool HideWhenUnavailable;
        public int PosY = -1;
        public int PosX = -1;
        [JsonConverter(typeof(FontConverter))]
        public Font Font;

        public static SettingsObj GenerateDefault()
        {
            var font = new Font(FontFamily.GenericSansSerif, 34.0f, FontStyle.Regular, GraphicsUnit.Point);
            return new SettingsObj
            {
                BorderColor = Color.Black,
                Color1 = Color.GhostWhite,
                Color2 = Color.LightGray,
                Font = font,
                GradientType = 1,
                AlignmentType = 0,
                BackgroundOpacity = 40,
            };
        }
    }

    // This corresponds to items of comboBoxGradientType!
    public enum GradientType
    {
        NoGradient = 0,
        DoubleColor = 1,
        TripleColor = 2
    }

    // This corresponds to items of comboBoxAlignment!
    public enum AlignmentType
    {
        Center = 0,
        Left = 1,
        Right = 2
    }
}
