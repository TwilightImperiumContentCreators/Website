using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwilightImperiumContentCreatorsCommunity.Models.Domain.GalleryMaps;

namespace TwilightImperiumContentCreatorsCommunity.Models.Requests
{
    public class CreateMapRequest
    {
        public GalleryMap Map { get; set; }
    }
}
