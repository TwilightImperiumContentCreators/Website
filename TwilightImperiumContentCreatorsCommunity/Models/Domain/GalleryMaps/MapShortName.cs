using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace TwilightImperiumContentCreatorsCommunity.Models.Domain.GalleryMaps
{
    public struct MapShortName : IEquatable<MapShortName>
    {
        private static Regex shortNamePattern = new Regex("[a-zA-Z0-9.]{8,32}", RegexOptions.Compiled);

        private readonly string value;

        public MapShortName(string mapShortName)
        {
            if (string.IsNullOrWhiteSpace(mapShortName))
            {
                throw new ArgumentException("mapShortName may not be missing or whitespace", nameof(mapShortName));
            }

            if (!shortNamePattern.IsMatch(mapShortName))
            {
                throw new ArgumentException("mapShortName must be between 8 and 32 characters and only contain alphanumeric characters or the special characters '.' or '-'", nameof(mapShortName));
            }

            this.value = mapShortName;
        }

        public static implicit operator MapShortName(string mapShortName)
        {
            return new MapShortName(mapShortName);
        }

        public static implicit operator string(MapShortName mapShortName)
        {
            return mapShortName.value;
        }

        public bool Equals([AllowNull] MapShortName other) => this.value == other.value;
    }
}