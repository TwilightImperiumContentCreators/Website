using System;
using System.Data;
using System.Security.Policy;
using TwilightImperiumContentCreatorsCommunity.Models.Domain;
using TwilightImperiumContentCreatorsCommunity.Models.Domain.GalleryMaps;
using Xunit;

namespace Tests
{
    public class GalleryMapTests
    {
        [Fact]
        public void BasicTest()
        {
            var scpt = new Owner(ownerName: "SCPT", ownerId: "SCPT", identityProvider:"Internal", emailAddress:  "test@test.com", publicOwner: true);

            GalleryMap galleryMap = new GalleryMap(
                mapString: "40 47 32 38 44 36 20 48 26 22 31 41 30 19 27 39 35 21 0 45 34 0 43 29 0 49 25 0 50 37 0 42 24 0 46 28",
                mapShortName: "scpttourny2020-prelim-v3", 
                mapDescription: "The map used for the Space Cats Peace Turtles TwilightImperium 4th edition 2020 Tabletop Simulator Tournament Prelims. v3.0",
                players: 6,
                mapQuality: MapQuality.Curated,
                category: "Tournament",
                owner: scpt);

            Assert.True(true);
        }
    }
}
