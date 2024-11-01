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
        public FrmSettings(SettingsObj settings)
        {
            InitializeComponent();
            comboBoxGradientType.SelectedIndex = 0;
            _settings = settings;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            try
            {
                _font = FontSerializationHelper.Deserialize(_settings.Font);
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                btnFont.Text = _font.Name + " "+ _font.Size;
                comboBoxGradientType.SelectedIndex = _settings.GradientType;
                comboBoxAlignment.SelectedIndex = _settings.AlignmentType;
                btnColor1.BackColor = _settings.Color1;
                btnColor2.BackColor = _settings.Color2;
                btnBorderColor.BackColor = _settings.BorderColor;
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
        }

        private void btnColors_Click(object sender, EventArgs e)
        {
            var button = (Button) sender;
            _colorDialog.Color = button.BackColor;
            var res = _colorDialog.ShowDialog();
            if (res == DialogResult.OK || res == DialogResult.Yes)
                button.BackColor = _colorDialog.Color;
        }

        private void checkBoxPreserveSlash_CheckedChanged(object sender, EventArgs e)
        {
            _settings.PreserveSlash = checkBoxPreserveSlash.Checked;
        }

        private void checkBoxAutoHide_CheckedChanged(object sender, EventArgs e)
        {
            _settings.AutoHide = checkBoxAutoHide.Checked;
        }

        private void checkBoxNextLineWhenNoTranslation_CheckedChanged(object sender, EventArgs e)
        {
            _settings.NextLineWhenNoTranslation = checkBoxNextLineWhenNoTranslation.Checked;
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            _settings.Font = FontSerializationHelper.Serialize(_font);
            _settings.Color1 = btnColor1.BackColor;
            _settings.Color2 = btnColor2.BackColor;
            _settings.BorderColor = btnBorderColor.BackColor;
            _settings.GradientType = comboBoxGradientType.SelectedIndex;
            _settings.AlignmentType = comboBoxAlignment.SelectedIndex;
            _settings.PreserveSlash = checkBoxPreserveSlash.Checked;
            _settings.AutoHide = checkBoxAutoHide.Checked;
            _settings.HideWhenUnavailable = checkBoxHideWhenUnavailable.Checked;
        }
    }

    public class SettingsObj
    {
        public string Font;
        public Color Color1;
        public Color Color2;
        public Color BorderColor;
        public int GradientType;
        public int AlignmentType;
        public bool PreserveSlash;
        public bool AutoHide;
        public bool NextLineWhenNoTranslation;
        public bool HideOnStartup;
        public bool HideWhenUnavailable;
        public int PosY = -1;
        public int PosX = -1;

        [JsonIgnore]
        public Font FontActual
        {
            get => FontSerializationHelper.Deserialize(Font);
            set => Font = FontSerializationHelper.Serialize(value);
        }

        public static SettingsObj GenerateDefault()
        {
            return new SettingsObj
            {
                BorderColor = Color.Black,
                Color1 = Color.GhostWhite,
                Color2 = Color.LightGray,
                FontActual = new Font(FontFamily.GenericSansSerif, 34.0f, FontStyle.Regular, GraphicsUnit.Point),
                GradientType = 1,
                AlignmentType = 0,
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
