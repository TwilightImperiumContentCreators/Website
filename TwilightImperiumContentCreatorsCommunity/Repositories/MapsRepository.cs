using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwilightImperiumContentCreatorsCommunity.Models.Domain.GalleryMaps;

namespace TwilightImperiumContentCreatorsCommunity.Repositories
{
    public abstract class MapsRepository
    {
        protected readonly ILogger<MapsRepository> logger;

        public MapsRepository(ILogger<MapsRepository> logger)
        {
            this.logger = logger;
        }

        public abstract Task<List<GalleryMap>> FindAsync(int players);

        public abstract Task<GalleryMap> GetAsync(MapShortName shortName);

        public abstract Task CreateAsync(GalleryMap galleryMap);
    }
}
