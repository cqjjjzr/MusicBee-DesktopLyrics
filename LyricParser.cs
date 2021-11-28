using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace MusicBeePlugin
{
    public class LyricParser
    {
        private static readonly Regex LyricWordRegex = new Regex(@".*\](.*)", RegexOptions.Compiled);
        private static readonly Regex LyricTimeRegex = new Regex(@"\[([0-9.:]*)\]+(.*)", RegexOptions.Compiled);

        public static bool PreserveSlash { get; set; } = false;

        public static Lyrics ParseLyric(string lyric)
        {
            var lyricOffset = 0.0;
            if (lyric == null || lyric.Trim().Length == 0 || !lyric.Contains("[")) return null;
            var rawLyrics = new List<RawLyricEntry>();
            using (var reader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(lyric))))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!line.StartsWith("[")) continue;
                    if (line.StartsWith("[ti:") ||
                        line.StartsWith("[ar:") ||
                        line.StartsWith("[al:") ||
                        line.StartsWith("[by:")) continue;
                    if (line.StartsWith("[offset:"))
                    {
                        try
                        {
                            lyricOffset = ProcessOffset(line);
                        }
                        catch (Exception)
                        { /* IGNORED */ }
                        continue;
                    }
                    var wordMatch = LyricWordRegex.Match(line);
                    var word = wordMatch.Groups[1].Value;

                    if (word.Length == 0) word = " ";

                    var timeMatch = LyricTimeRegex.Matches(line);

                    foreach (Match item in timeMatch)
                    {
                        var time = TimeSpan.Parse("00:" + item.Groups[1].Value).TotalMilliseconds;
                        rawLyrics.Add(new RawLyricEntry(time, word));
                    }
                }
            }

            if (rawLyrics.Count <= 0) return null;
            var entries = CollapseLyric(rawLyrics);
            return new Lyrics
            {
                Entries = entries,
                Offset = lyricOffset
            };
        }

        private static List<LyricEntry> CollapseLyric(IEnumerable<RawLyricEntry> rawLyrics)
        {
            var entries = new List<LyricEntry>();
            foreach (var rawLyricEntry in rawLyrics)
            {
                if (!PreserveSlash && rawLyricEntry.LyricLine.Contains("/"))
                {
                    var segs = rawLyricEntry.LyricLine.Split(new[] {'/'}, 2);
                    entries.Add(new LyricEntry(rawLyricEntry.Time, segs[0], segs[1]));
                }
                else
                    entries.Add(new LyricEntry(rawLyricEntry.Time, rawLyricEntry.LyricLine, null));
            }
            entries.Sort(new LyricEntry.LyricEntryComparer());
            return entries;
        }

        private static double ProcessOffset(string line)
        {
            double.TryParse(line.Substring(line.IndexOf(':') + 1).TrimEnd(']'), out var offset);
            return offset;
        }

        public class LyricEntry
        {
            public double TimeMs { get; set; }
            public string LyricLine1 { get; set; }
            public string LyricLine2 { get; set; }

            public LyricEntry(double time, string lyricLine1, string lyricLine2)
            {
                TimeMs = time;
                LyricLine1 = lyricLine1;
                LyricLine2 = lyricLine2;
            }

            public override string ToString()
            {
                return "[Lyric time=" + TimeMs + " line1=" + LyricLine1 + " line2=" + LyricLine2 + "]\r";
            }

            public class LyricEntryComparer : Comparer<LyricEntry>
            {
                public override int Compare(LyricEntry x, LyricEntry y)
                {
                    if (x == null) return -1;
                    if (y == null) return 1;
                    return (int)(x.TimeMs - y.TimeMs);
                }
            }
        }

        public class RawLyricEntry
        {
            public double Time { get; set; }
            public string LyricLine { get; set; }

            public RawLyricEntry(double time, string lyricLine)
            {
                Time = time;
                LyricLine = lyricLine;
            }
        }

        public class Lyrics
        {
            public List<LyricEntry> Entries;
            public double Offset;
        }
    }
}
