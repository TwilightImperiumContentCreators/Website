using System;
using System.Text.RegularExpressions;

namespace TwilightImperiumContentCreatorsCommunity.Models.Domain.GalleryMaps
{
    // a mapstring is a sequence of less than 36 n-digit map tile ids with 00 being reserved for -not-a-tile- separated by a space (0x32) ?
    public struct MapString
    {
        private static Regex mapTilePattern = new Regex("[0-9]*", RegexOptions.Compiled);
        private static Regex mapStringPattern = new Regex(@"[0-9\s]*", RegexOptions.Compiled);

        private string value;

        public MapString(string mapString)
        {
            if (string.IsNullOrWhiteSpace(mapString))
            {
                throw new ArgumentException("mapString may not be missing or whitespace", nameof(mapString));
            }

            if (!mapStringPattern.IsMatch(mapString))
            {
                throw new ArgumentException("mapString must contain only digits and whitespace", nameof(mapString));
            }

            // unused maptiles data.
            var tilesStrings = mapTilePattern.Split(mapString);

            this.value = mapString.Trim();
        }

        public static implicit operator MapString(string mapString)
        {
            return new MapString(mapString);
        }

        public static implicit operator string(MapString mapString)
        {
            return mapString.value;
        }
    }
}