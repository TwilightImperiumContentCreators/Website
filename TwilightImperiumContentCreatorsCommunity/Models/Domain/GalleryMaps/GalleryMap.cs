using Microsoft.AspNetCore.Razor.Language.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwilightImperiumContentCreatorsCommunity.Models.Domain.GalleryMaps
{
    public class GalleryMap
    {
        public string MapString { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }

        public int Players { get; set; }

        public MapQuality MapQuality { get; }

        public string Category { get; }

        public Owner Owner { get; set; }

        public GalleryMap(string mapString,
                          string mapShortName,
                          string mapDescription,
                          int players,
                          MapQuality mapQuality,
                          string category,
                          Owner owner)
        {
            this.MapString = mapString;
            this.ShortName = mapShortName;
            this.Description = mapDescription;
            
            if (players <= 1 || players > 10)
            {
                throw new ArgumentOutOfRangeException("The map must be between 2 and 10 players", nameof(GalleryMap));
            }

            this.Players = players;
            this.MapQuality = mapQuality;
            this.Category = category;
            this.Owner = owner;
        }

        public GalleryMap()
        {

        }
    }
}
