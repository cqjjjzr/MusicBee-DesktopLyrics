using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MusicBeePlugin
{
    [TypeConverter(typeof(FontConverter))]
    internal class FontSerializationHelper
    {
        public static Font Deserialize(string value)
        {
            var m = Regex.Match(value, "^(?<Font>[\\w ]+),(?<Size>(\\d+(\\.\\d+)?))(,(?<Style>(R|[BIU]{1,3})))?$", RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);

            if (!m.Success) throw new FormatException("Value is not properly formatted.");

            if (m.Groups.Count < 4 || m.Groups[3].Value == "R")
                return new Font(m.Groups["Font"].Value, float.Parse(m.Groups["Size"].Value));

            var fs = (m.Groups[3].Value.Contains("B") ? FontStyle.Bold : FontStyle.Regular)
                           | (m.Groups[3].Value.Contains("I") ? FontStyle.Italic : FontStyle.Regular)
                           | (m.Groups[3].Value.Contains("U") ? FontStyle.Underline : FontStyle.Regular);
            return new Font(m.Groups["Font"].Value, float.Parse(m.Groups["Size"].Value), fs);
        }

        public static string Serialize(Font value)
        {
            var str = value.Name + "," + value.Size.ToString(CultureInfo.InvariantCulture) + ",";
            if (value.Style == FontStyle.Regular)
            {
                str += "R";
            }
            else
            {
                if (value.Bold) str += "B";
                if (value.Italic) str += "I";
                if (value.Underline) str += "U";
            }

            return str;
        }
    }
}