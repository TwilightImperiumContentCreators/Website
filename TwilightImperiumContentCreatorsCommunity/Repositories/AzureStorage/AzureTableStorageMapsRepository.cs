using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Logging;
using TwilightImperiumContentCreatorsCommunity.Models.Domain.GalleryMaps;
using TwilightImperiumContentCreatorsCommunity.Models.Entities;
using TwilightImperiumContentCreatorsCommunity.Models.Domain;
using System.Runtime.Serialization;
using System;

namespace TwilightImperiumContentCreatorsCommunity.Repositories.AzureStorage
{
    public class AzureTableStorageMapsRepository : MapsRepository
    {
        private readonly CloudTableClient client;
        private const string MapsMasterTable = "mapsmaster";

        public AzureTableStorageMapsRepository(
            CloudTableClient client,
            ILogger<MapsRepository> logger) : base(logger)
        {
            this.client = client;
        }

        public async override Task CreateAsync(GalleryMap galleryMap)
        {
            CloudTable masterTable = await GetMasterTable();
            var insertOperation = TableOperation.Insert(GalleryMapEntity.From(galleryMap));
            await masterTable.ExecuteAsync(insertOperation);
        }

        public async override Task<List<GalleryMap>> FindAsync(int players)
        {
            CloudTable masterTable = await GetMasterTable();

            TableQuery<GalleryMapEntity> findByPlayersQuery = new TableQuery<GalleryMapEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartionKey", QueryComparisons.Equal, players.ToString()));

            return masterTable.ExecuteQuery(findByPlayersQuery)
                .Select(m => new GalleryMap(
                    m.MapString,
                    m.ShortName,
                    m.Description,
                    m.Players,
                    Enum.Parse<MapQuality>(m.MapQuality, true),
                    m.Category,
                    new Owner("N/A", m.OwnerId, m.OwnerIdentityProvider, "N/A", true)))
                .ToList();
            // TODO : needs owner data still!
        }

        public async override Task<GalleryMap> GetAsync(MapShortName shortName)
        {
            CloudTable masterTable = await GetMasterTable();
            var getFromShortNameOperation = TableOperation.Retrieve<GalleryMapEntity>(GalleryMapEntity.MasterPartitionKey, GalleryMapEntity.CreateCanonicalShortName(shortName));
            var retrieveResult = await masterTable.ExecuteAsync(getFromShortNameOperation);

            var m = retrieveResult.Result as GalleryMapEntity;

            return new GalleryMap(
                    m.MapString,
                    m.ShortName,
                    m.Description,
                    m.Players,
                    Enum.Parse<MapQuality>(m.MapQuality ?? "Community", true),
                    m.Category??"",
                    new Owner("N/A", m.OwnerId?? "", m.OwnerIdentityProvider??"", "N/A", true));
        }

        private async Task<CloudTable> GetMasterTable()
        {
            var masterTable = this.client.GetTableReference(MapsMasterTable);
            await masterTable.CreateIfNotExistsAsync();
            return masterTable;
        }

    }
}
