using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace MusicBeePlugin
{
    internal class FontConverter : JsonConverter<Font>
    {
        private static TypeConverter Converter { get; } = TypeDescriptor.GetConverter(typeof(Font));

        public override void WriteJson(JsonWriter writer, Font value, JsonSerializer serializer)
        {
            writer.WriteValue(Converter.ConvertToInvariantString(value));
        }

        public override Font ReadJson(JsonReader reader, Type objectType, Font existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var str = serializer.Deserialize<string>(reader);
            try
            {
                return (Font) Converter.ConvertFromInvariantString(str);
            }
            catch (Exception)
            {
                // Fallback to legacy
                return ConvertFromLegacyString(str);
            }
        }

        private static Font ConvertFromLegacyString(string str)
        {
            var m = Regex.Match(
                str,
                "^(?<Font>[\\w ]+),(?<Size>(\\d+(\\.\\d+)?))(,(?<Style>(R|[BIU]{1,3})))?$",
                RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);

            if (!m.Success) throw new FormatException("Font settings are not properly formatted.");

            if (m.Groups.Count < 4 || m.Groups[3].Value == "R")
                return new Font(m.Groups["Font"].Value, float.Parse(m.Groups["Size"].Value));

            var fs = (m.Groups[3].Value.Contains("B") ? FontStyle.Bold : FontStyle.Regular)
                     | (m.Groups[3].Value.Contains("I") ? FontStyle.Italic : FontStyle.Regular)
                     | (m.Groups[3].Value.Contains("U") ? FontStyle.Underline : FontStyle.Regular);
            return new Font(m.Groups["Font"].Value, float.Parse(m.Groups["Size"].Value), fs,
                GraphicsUnit.Point);
        }
    }
}