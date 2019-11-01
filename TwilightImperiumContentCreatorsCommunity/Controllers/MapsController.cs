using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TwilightImperiumContentCreatorsCommunity.Models.Requests;
using TwilightImperiumContentCreatorsCommunity.Services;

namespace TwilightImperiumContentCreatorsCommunity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapsController : ControllerBase
    {
        private readonly MapsService mapsService;

        public MapsController(MapsService mapsService)
        {
            this.mapsService = mapsService;
        }

        // GET: api/Maps/Players/{playerCount}
        [HttpGet("players/{playerCount}")]
        public async Task<ActionResult> GetMapsByPlayerCount(int playerCount)
        {
            var result = await mapsService.FindGalleryMaps(playerCount);
            return Ok(result);
        }

        [HttpGet("{shortName}", Name = "GetMapByShortName")]
        public async Task<ActionResult> GetMapByShortName(string shortName)
        {
            var result = await mapsService.GetGalleryMap(shortName);
            return Ok(result);
        }

        // POST: api/Maps
        [HttpPost(Name = "CreateMap")]
        public async Task<ActionResult> CreateMap([FromBody] CreateMapRequest requestBody)
        {
            var result = await mapsService.CreateGalleryMap(requestBody.Map);
            return CreatedAtAction(nameof(GetMapByShortName), new { shortName = requestBody.Map.ShortName }, $"Created map {requestBody.Map.ShortName}");
        }
    }
}
