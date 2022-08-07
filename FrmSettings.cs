using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Cyotek.Windows.Forms;
using Newtonsoft.Json;

namespace MusicBeePlugin
{
    public partial class FrmSettings : Form
    {
        private Font _font = DefaultFont;
        private SettingsObj _settings;
        private readonly ColorPickerDialog _colorDialog = new ColorPickerDialog();
        public FrmSettings(SettingsObj settings)
        {
            InitializeComponent();
            cbxGradientType.SelectedIndex = 0;
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
                cbxGradientType.SelectedIndex = _settings.GradientType;
                btnColor1.BackColor = _settings.Color1;
                btnColor2.BackColor = _settings.Color2;
                btnBorderColor.BackColor = _settings.BorderColor;
                checkBoxPreserveSlash.Checked = _settings.PreserveSlash;
                checkBoxAutoHide.Checked = _settings.AutoHide;
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
            if (res == DialogResult.OK || res == DialogResult.Yes)
            {
                var font = dlgFont.Font;
                // Force point unit
                _font = new Font(font.FontFamily, font.Size, font.Style, GraphicsUnit.Point, font.GdiCharSet);
            }
        }

        private void btnColors_Click(object sender, EventArgs e)
        {
            var button = (Button) sender;
            _colorDialog.Color = button.BackColor;
            var res = _colorDialog.ShowDialog();
            if (res == DialogResult.OK || res == DialogResult.Yes)
                button.BackColor = _colorDialog.Color;
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            _settings.Font = FontSerializationHelper.Serialize(_font);
            _settings.Color1 = btnColor1.BackColor;
            _settings.Color2 = btnColor2.BackColor;
            _settings.BorderColor = btnBorderColor.BackColor;
            _settings.GradientType = cbxGradientType.SelectedIndex;
            _settings.PreserveSlash = checkBoxPreserveSlash.Checked;
            _settings.AutoHide = checkBoxAutoHide.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            _settings.PreserveSlash = checkBoxPreserveSlash.Checked;
        }

        private void checkBoxAutoHide_CheckedChanged(object sender, EventArgs e)
        {
            _settings.AutoHide = checkBoxAutoHide.Checked;
        }

        public SettingsObj Settings => _settings;
    }

    public class SettingsObj
    {
        public string Font;
        public Color Color1;
        public Color Color2;
        public Color BorderColor;
        public int GradientType;
        public bool PreserveSlash = false;
        public bool AutoHide = false;
        public bool HideOnStartup = false;
        public int PosY = -1;
        public int PosX = -1;

        [JsonIgnore]
        public Font FontActual
        {
            get => FontSerializationHelper.Deserialize(Font);
            set => Font = FontSerializationHelper.Serialize(value);
        }
    }

    public enum GredientType
    {
        NoGrendient = 0,
        DoubleColor = 1,
        TripleColor = 2
    }
}
