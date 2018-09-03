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
        private readonly string savePath;

        private Font _font = DefaultFont;
        private readonly ColorPickerDialog _colorDialog = new ColorPickerDialog();
        public FrmSettings(string path)
        {
            InitializeComponent();
            savePath = path;
            cbxGradientType.SelectedIndex = 0;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            if (!File.Exists(savePath)) return;

            try
            {
                var config = JsonConvert.DeserializeObject<SettingsObj>(File.ReadAllText(savePath));
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
                GradientType = cbxGradientType.SelectedIndex
            };
            File.WriteAllText(savePath, JsonConvert.SerializeObject(settings));
        }

        public SettingsObj Settings
        {
            get
            {
                return new SettingsObj
                {
                    Font = FontSerializationHelper.Serialize(_font),
                    Color1 = btnColor1.BackColor,
                    Color2 = btnColor2.BackColor,
                    BorderColor = btnBorderColor.BackColor,
                    GradientType = cbxGradientType.SelectedIndex,
                    FontActual = _font
                };
            }
        }
    }

    public class SettingsObj
    {
        public string Font;
        public Color Color1;
        public Color Color2;
        public Color BorderColor;
        public int GradientType;
        
        [JsonIgnore]
        public Font FontActual
        {
            get => FontSerializationHelper.Deserialize(Font);
            set => FontSerializationHelper.Serialize(value);
        }
    }

    public enum GredientType
    {
        NoGrendient = 0,
        DoubleColor = 1,
        TripleColor = 2
    }
}
