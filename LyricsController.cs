namespace MusicBeePlugin
{
    public class LyricsController
    {
        public bool NextLineWhenNoTranslation { get; set; }

        private readonly Plugin.MusicBeeApiInterface _interface;
        private string _lastLyrics;
        private LyricParser.Lyrics _lyrics;
        private int _currentEntryIndex = -1;
        private double _currentEntryStart;
        private double _currentEntryEnd;

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
                    _currentEntryIndex = -1;
                }
            }
            else
            {
                _lyrics = null;
                _lastLyrics = null;
                _currentEntryIndex = -1;
            }
            

            if (_lyrics == null)
            {
                if (useGeneratedWhenUnavailable)
                    return new LyricView(
                    _interface.NowPlaying_GetFileTag(Plugin.MetaDataType.TrackTitle) + " - " +
                    _interface.NowPlaying_GetFileTag(Plugin.MetaDataType.Artist), null);
                return emptyEntry;
            }
                
            var time = _interface.Player_GetPosition();
            var nTime = time + _lyrics.Offset;
            var entries = _lyrics.Entries;

            LyricParser.LyricEntry currentEntry = null;
            LyricParser.LyricEntry nextEntry = null;


            if (_currentEntryIndex >= 0 && _currentEntryIndex < entries.Count && nTime >= _currentEntryStart)
            {
                if (nTime <= _currentEntryEnd)
                {
                    currentEntry = entries[_currentEntryIndex];
                    nextEntry = entries[_currentEntryIndex];
                }
                else
                {
                    for (var i = _currentEntryIndex; i < entries.Count; i++)
                    {
                        if (entries[i].TimeMs > nTime)
                        {
                            currentEntry = entries[i - 1];
                            nextEntry = entries[i];
                            _currentEntryIndex = i;
                            _currentEntryStart = currentEntry.TimeMs;
                            _currentEntryEnd = nextEntry.TimeMs;

                            break;
                        }
                    }
                }

            }
            else
            {
                for (var i = 0; i < entries.Count; i++)
                {
                    if (entries[i].TimeMs > nTime)
                    {
                        if (i > 0)
                        {
                            currentEntry = entries[i - 1];
                            nextEntry = entries[i];
                            _currentEntryIndex = i;
                            _currentEntryStart = currentEntry.TimeMs;
                            _currentEntryEnd = nextEntry.TimeMs;
                        }
                        else
                        {
                            return emptyEntry;
                        }

                        break;
                    }
                }
            }

            if (currentEntry == null)
            {
                if (entries.Count <= 0) return emptyEntry;
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

        public static LyricView emptyEntry = new LyricView("", "");
    }
}
