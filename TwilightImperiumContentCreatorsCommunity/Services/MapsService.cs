using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwilightImperiumContentCreatorsCommunity.Models.Domain.GalleryMaps;
using TwilightImperiumContentCreatorsCommunity.Repositories;

namespace TwilightImperiumContentCreatorsCommunity.Services
{
    public class MapsService
    {
        private readonly MapsRepository mapsRepository;
        private readonly ILogger<MapsService> logger;

        public MapsService(
            MapsRepository mapsRepository,
            ILogger<MapsService> logger)
        {
            this.mapsRepository = mapsRepository;
            this.logger = logger;
        }

        public async Task<QueryResult<List<GalleryMap>>> FindGalleryMaps(int players) =>
            await ExecuteQuery(Policies.Default, () => this.mapsRepository.FindAsync(players));
            

        public async Task<QueryResult<GalleryMap>> GetGalleryMap(MapShortName shortName) =>
            await ExecuteQuery(Policies.Default, () => this.mapsRepository.GetAsync(shortName));


        public async Task<CommandResult> CreateGalleryMap(GalleryMap galleryMap) =>
            await ExecuteCommand(Policies.Default, () => this.mapsRepository.CreateAsync(galleryMap));

        private async Task<CommandResult> ExecuteCommand(IAsyncPolicy policy, Func<Task> commandFunction)
        {
            try
            {
                await policy.ExecuteAsync(commandFunction);
                return new CommandResult();             
            }
            catch (Exception exc)
            {
                logger.LogError(exc, "An error occurred while attempted to execute the command.");

                return new CommandResult(exc);
            }
        }

        internal Task CreateGalleryMap(object map)
        {
            throw new NotImplementedException();
        }

        public async Task<QueryResult<TResult>> ExecuteQuery<TResult>(IAsyncPolicy policy, Func<Task<TResult>> queryFunction )
        {
            try
            {
                var policyResult = await policy.ExecuteAndCaptureAsync(queryFunction);

                if (policyResult.Outcome == OutcomeType.Successful)
                {
                    return new QueryResult<TResult>(policyResult.Result);
                }
                else
                {
                    logger.LogError(policyResult.FinalException, "An error occurred while attempted to execute the query.");

                    return new QueryResult<TResult>(policyResult.FinalException);
                }
            }
            catch (Exception exc)
            {
                logger.LogError(exc, "An error occurred while attempted to execute the query.");

                return new QueryResult<TResult>(exc);
            }
        }
    }
}
