using Microsoft.Azure.Cosmos.Table;
using System;
using TwilightImperiumContentCreatorsCommunity.Models.Domain.GalleryMaps;

namespace TwilightImperiumContentCreatorsCommunity.Models.Entities
{
    public class GalleryMapEntity : TableEntity
    {
        public const string MasterPartitionKey = "GalleryMap";

        public string MapString { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }

        public int Players { get; set; }

        public string MapQuality { get; }

        public string Category { get; }

        public string OwnerId { get; set; }

        public string OwnerIdentityProvider { get; set; }

        public GalleryMapEntity()
        {
        }

        protected GalleryMapEntity(GalleryMap galleryMap)
        {
            this.MapString = galleryMap.MapString;
            this.ShortName = galleryMap.ShortName;
            this.Description = galleryMap.Description;
            this.Players = galleryMap.Players;
            this.MapQuality = Enum.GetName(typeof(MapQuality), galleryMap.MapQuality);
            this.Category = galleryMap.Category;
            this.OwnerId = galleryMap.Owner.Id;
            this.OwnerIdentityProvider = galleryMap.Owner.IdentityProvider;

            this.PartitionKey = MasterPartitionKey;
            this.RowKey = CreateCanonicalShortName(galleryMap.ShortName);
        }

        public static string CreateCanonicalShortName(string shortName) => shortName.ToString().ToLowerInvariant();

        public static GalleryMapEntity From(GalleryMap galleryMap)
        {
            return new GalleryMapEntity(galleryMap);
        }
    }
}
