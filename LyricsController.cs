namespace MusicBeePlugin
{
    public class LyricsController
    {
        public bool NextLineWhenNoTranslation { get; set; }

        private readonly Plugin.MusicBeeApiInterface _interface;
        private string _lastLyrics;
        private LyricParser.Lyrics _lyrics;
        public LyricsController(Plugin.MusicBeeApiInterface @interface)
        {
            _interface = @interface;
        }

        public LyricView UpdateLyrics(bool useGeneratedWhenUnavailable)
        {
            // TODO passively change?
            var hasLyrics = _interface.NowPlaying_GetFileTag(Plugin.MetaDataType.HasLyrics);
            if (hasLyrics.StartsWith("Y")  || hasLyrics.Length == 0)
            {
                var lyrics = _interface.NowPlaying_GetLyrics();
                if (lyrics != _lastLyrics)
                {
                    _lyrics = LyricParser.ParseLyric(lyrics);
                    _lastLyrics = lyrics;
                }
            }
            else
            {
                _lyrics = null;
                _lastLyrics = null;
            }
            

            if (_lyrics == null)
            {
                if (useGeneratedWhenUnavailable)
                    return new LyricView(
                    _interface.NowPlaying_GetFileTag(Plugin.MetaDataType.TrackTitle) + " - " +
                    _interface.NowPlaying_GetFileTag(Plugin.MetaDataType.Artist), null);
                return null;
            }
                
            var time = _interface.Player_GetPosition();
            var nTime = time + _lyrics.Offset;
            var entries = _lyrics.Entries;

            LyricParser.LyricEntry currentEntry = null;
            LyricParser.LyricEntry nextEntry = null;

            for (var i = 0; i < entries.Count; i++)
            {
                if (entries[i].TimeMs > nTime && i > 0)
                {
                    currentEntry = entries[i - 1];
                    nextEntry = entries[i];
                    break;
                }
            }

            if (currentEntry == null)
            {
                if (entries.Count <= 0) return null;
                else currentEntry = entries[entries.Count - 1];
            }

            if (_lyrics.HasTranslation || nextEntry == null || !NextLineWhenNoTranslation)
                return new LyricView(currentEntry.LyricLine1, currentEntry.LyricLine2);
            return new LyricView(currentEntry.LyricLine1, nextEntry.LyricLine1);
        }

        public class LyricView
        { 
            // C# version < 8.0, can't use nullable reference feature....
            public string LyricLine1 { get; set; } // Nullable
            public string LyricLine2 { get; set; } // Nullable

            public LyricView(string lyricLine1, string lyricLine2)
            {
                LyricLine1 = lyricLine1;
                LyricLine2 = lyricLine2;
            }

            public override string ToString()
            {
                return $"[LyricView: {LyricLine1}, {LyricLine2}]";
            }
        }
    }
}
