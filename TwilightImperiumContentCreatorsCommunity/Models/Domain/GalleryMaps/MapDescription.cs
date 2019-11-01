using System;

namespace TwilightImperiumContentCreatorsCommunity.Models.Domain.GalleryMaps
{
    public struct MapDescription
    {
        private readonly string value;

        public MapDescription(string mapDescription)
        {
            if (string.IsNullOrWhiteSpace(mapDescription))
            {
                throw new ArgumentException("mapDescription may not be missing or whitespace", nameof(mapDescription));
            }

            if (mapDescription.Length > 256)
            {
                throw new ArgumentException("mapDescription may be no more than 256 characters.", nameof(mapDescription));
            }

            this.value = mapDescription;
        }

        public static implicit operator MapDescription(string mapDescription)
        {
            return new MapDescription(mapDescription);
        }

        public static implicit operator string(MapDescription mapDescription)
        {
            return mapDescription.value;
        }
    }
}