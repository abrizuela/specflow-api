using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net;
using TechTalk.SpecFlow;

namespace SpecFlowAPI
{
    [Binding]
    public class SpotifyPlaylistsServiceSteps : ServiceStepsBase
    {
        private readonly SpotifyPlaylistsService _spotifyPlaylistsService;

        private string _playlistId;


        public SpotifyPlaylistsServiceSteps(Helper helper, ServicesDriver driver, SpotifyPlaylistsService spotifyService) : base(helper, driver)
        {
            _spotifyPlaylistsService = spotifyService;
        }

        #region GIVEN

        [Given(@"a nonexistent User Id is populated")]
        public void GivenANonexistentUserIdIsPopulated()
        {
            var userId = Helper.Faker.Random.AlphaNumeric(28);
            _spotifyPlaylistsService.PopulateUserId(userId);
        }

        [Given(@"Random Playlist Id is populated")]
        public void GivenRandomPlaylistIdIsPopulated()
        {
            WhenGetUserSPlaylistsOperationIsExecuted();
            ResponseStatusCodeIs(HttpStatusCode.OK);
            var playlists = Response!.GetContent<PlaylistReponseModel>()!.Items;
            playlists!.Count.Should().BeGreaterThan(0, because: "should be at least a public playlist created by the user");
            _playlistId = Helper.Faker.PickRandom(playlists).Id;
        }

        [Given(@"a nonexistent playlist Id is populated")]
        public void GivenANonexistentPlaylistIdIsPopulated()
        {
            _playlistId = Helper.Faker.Random.AlphaNumeric(22);
        }

        #endregion

        #region WHEN

        [When(@"Get User's Playlists operation is executed")]
        public void WhenGetUserSPlaylistsOperationIsExecuted()
        {
            Response = _spotifyPlaylistsService.GetUserSPlaylists();
        }

        [When(@"Get Playlist operation is executed")]
        public void WhenGetPlaylistOperationIsExecuted()
        {
            Response = _spotifyPlaylistsService.GetPlaylist(_playlistId);
        }

        #endregion

        #region THEN
        #endregion
    }
}
