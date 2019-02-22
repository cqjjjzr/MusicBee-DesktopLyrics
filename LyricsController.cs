namespace MusicBeePlugin
{
    public class LyricsController
    {
        private readonly Plugin.MusicBeeApiInterface _interface;
        private string _lastLyrics;
        private LyricParser.Lyrics _lyrics;
        public LyricsController(Plugin.MusicBeeApiInterface @interface)
        {
            _interface = @interface;
        }

        public LyricParser.LyricEntry UpdateLyrics()
        {
            var lyrics = _interface.NowPlaying_GetLyrics();
            if (lyrics != _lastLyrics)
            {
                _lyrics = LyricParser.ParseLyric(lyrics);
                _lastLyrics = lyrics;
            }

            if (_lyrics == null)
                return new LyricParser.LyricEntry(0.0,
                    _interface.NowPlaying_GetFileTag(Plugin.MetaDataType.TrackTitle) + " - " +
                    _interface.NowPlaying_GetFileTag(Plugin.MetaDataType.Artist), null);
            var time = _interface.Player_GetPosition();
            var nTime = time + _lyrics.Offset;
            var entries = _lyrics.Entries;
            for (var i = 0; i < entries.Count; i++)
            {
                if (entries[i].TimeMs > nTime && i > 0)
                {
                    return entries[i - 1];
                }
            }

            if (entries.Count <= 0) return null;
            return entries[entries.Count - 1];
        }
    }
}
