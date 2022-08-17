using RestSharp;

namespace SpecFlowAPI
{
    public class SpotifyPlaylistsService : ServiceBase
    {
        private string _userId;

        public SpotifyPlaylistsService(
            GlobalSettings globalSettings,
            AccessTokenProvider accessTokenProvider
            ) : base(globalSettings.BaseUrl, accessTokenProvider)
        {
            _userId = globalSettings.UserId;
        }

        /// <summary>
        /// Populates internal _userId used in GetUsersSPlaylists
        /// </summary>
        /// <param name="userId"></param>
        public void PopulateUserId(string userId)
        {
            _userId = userId;
        }

        /// <summary>
        /// Executes GET /users/{user_id}/playlists endpoint
        /// </summary>
        /// <returns></returns>
        public IRestResponse GetUserSPlaylists()
        {
            return ExecuteGet($"users/{_userId}/playlists");
        }

        /// <summary>
        /// Executes GET /playlists/{playlist_id} endpoint
        /// </summary>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        internal IRestResponse GetPlaylist(string playlistId)
        {
            return ExecuteGet($"playlists/{playlistId}");
        }
    }
}
