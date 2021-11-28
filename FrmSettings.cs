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
        private readonly string _savePath;

        private Font _font = DefaultFont;
        private readonly ColorPickerDialog _colorDialog = new ColorPickerDialog();
        public FrmSettings(string path)
        {
            InitializeComponent();
            _savePath = path;
            cbxGradientType.SelectedIndex = 0;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            if (!File.Exists(_savePath)) return;

            try
            {
                var config = JsonConvert.DeserializeObject<SettingsObj>(File.ReadAllText(_savePath));
                try
                {
                    _font = FontSerializationHelper.Deserialize(config.Font);
                }
                catch (Exception)
                {
                    // ignored
                }
                try
                {
                    cbxGradientType.SelectedIndex = config.GradientType;
                    btnColor1.BackColor = config.Color1;
                    btnColor2.BackColor = config.Color2;
                    btnBorderColor.BackColor = config.BorderColor;
                    checkBoxPreserveSlash.Checked = config.PreserveSlash;
                    checkBoxAutoHide.Checked = config.AutoHide;
                }
                catch (Exception)
                {
                    // ignored
                }
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
                _font = dlgFont.Font;
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
            var settings = new SettingsObj
            {
                Font = FontSerializationHelper.Serialize(_font),
                Color1 = btnColor1.BackColor,
                Color2 = btnColor2.BackColor,
                BorderColor = btnBorderColor.BackColor,
                GradientType = cbxGradientType.SelectedIndex,
                PreserveSlash = checkBoxPreserveSlash.Checked,
                AutoHide = checkBoxAutoHide.Checked
            };
            File.WriteAllText(_savePath, JsonConvert.SerializeObject(settings));
        }

        public SettingsObj Settings => new SettingsObj
        {
            Font = FontSerializationHelper.Serialize(_font),
            Color1 = btnColor1.BackColor,
            Color2 = btnColor2.BackColor,
            BorderColor = btnBorderColor.BackColor,
            GradientType = cbxGradientType.SelectedIndex,
            FontActual = _font,
            PreserveSlash = checkBoxPreserveSlash.Checked,
            AutoHide = checkBoxAutoHide.Checked
        };

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Settings.PreserveSlash = checkBoxPreserveSlash.Checked;
        }

        private void checkBoxAutoHide_CheckedChanged(object sender, EventArgs e)
        {
            Settings.AutoHide = checkBoxAutoHide.Checked;
        }
    }

    public class SettingsObj
    {
        public string Font;
        public Color Color1;
        public Color Color2;
        public Color BorderColor;
        public int GradientType;
        public bool PreserveSlash;
        public bool AutoHide;
        
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
