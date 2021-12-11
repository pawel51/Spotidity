using Spotidity.Models;
using Spotidity.Stores;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotidity.Services
{
    public class ApiService
    {
        private SpotifyClient Spotify;
        private string Locale = "us_US";
        private string Country = "PL";
        private string Market = "pl";
        private int CategoriesLimit = 10;


        #region Singleton
        private static ApiService _instance;
        private static readonly object _lock = new object();
        private ApiService() { }

        public static ApiService GetInstace()
        {
            // pierwszy dostęp do serwisu
            if (_instance == null)
            {
                // zablokowanie dostępu przez pierwszy wątek
                lock (_lock)
                {
                    // drugi wątek nawet jeżeli będzie czekał na locku, to nie utworzy instacji 
                    if (_instance == null)
                    {
                        // utworzenie serwisu po raz pierwszy
                        _instance = new ApiService();
                    }
                }
            }
            return _instance;
        }

        #endregion

        static async Task Main()
        {

            ApiService service = new ApiService();
            /*
             * IDS {
         *  Mata = "0MIG6gMcQTSvFbKvUwK0id"
         *  Szafir Track = "5sGv8YdAa3XjggnbpF9NC9"
         *  Mata Album = "7vYR7oLh93zb38m880M8bd"
         *  Mata Playlist = "7CnLsDMKUghUXt8V1sO9Jk"
             */
            await service.AuthAsync();
            List<FullTrack> topTracks = await service.GetTopTracksFromArtist("0MIG6gMcQTSvFbKvUwK0id");
            Console.WriteLine(topTracks);



        }


        public async Task AuthAsync()
        {
            var config = SpotifyClientConfig.CreateDefault();
            var request = new ClientCredentialsRequest("e40838a072904689be92fda345f882e4", "30382f116bae49e9b90c04139e389d9f");
            var response = await new OAuthClient(config).RequestToken(request);
            Spotify = new SpotifyClient(config.WithToken(response.AccessToken));
        }

        public async Task<ObservableCollection<SpotiCategory>> GetCategories()
        {
            CategoriesRequest req = new CategoriesRequest()
            {
                Country = Country,
                Locale = Locale,
                Limit = CategoriesLimit,
                Offset = 0
            };

            try
            {
                var res = await Spotify.Browse.GetCategories(req);

                ObservableCollection<SpotiCategory> osc = new ObservableCollection<SpotiCategory>();
                foreach (Category c in res.Categories.Items)
                {
                    osc.Add(new SpotiCategory(c.Id, c.Name));
                }

                return osc;
            }
            catch (APIUnauthorizedException e)
            {
                try
                {
                    Console.WriteLine(e.Message);
                    await this.AuthAsync();
                    var res = await Spotify.Browse.GetCategories(req);
                    ObservableCollection<SpotiCategory> osc = new ObservableCollection<SpotiCategory>();
                    foreach (Category c in res.Categories.Items)
                    {
                        osc.Add(new SpotiCategory(c.Id, c.Name));
                    }

                    return osc;
                }
                catch (APIException m)
                {
                    Console.WriteLine(m.Message);
                }

            }
            catch (APITooManyRequestsException e)
            {
                Console.WriteLine(e.RetryAfter);
            }
            catch (APIException e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }

        public async Task<ObservableCollection<SpotiPlaylist>> GetCategoryPlaylists(string categoryId)
        {
            try
            {
                var res = await Spotify.Browse.GetCategoryPlaylists(categoryId);
                ObservableCollection<SpotiPlaylist> osp = new ObservableCollection<SpotiPlaylist>();
                foreach(SimplePlaylist sp in res.Playlists.Items)
                {
                    osp.Add(new SpotiPlaylist(sp.Id, sp.Name, sp.Owner, sp.Description));
                }
                return osp;
            }
            catch (APIUnauthorizedException e)
            {
                try
                {
                    Console.WriteLine(e.Message);
                    await this.AuthAsync();
                    var res = await Spotify.Browse.GetCategoryPlaylists(categoryId);
                    ObservableCollection<SpotiPlaylist> osp = new ObservableCollection<SpotiPlaylist>();
                    foreach (SimplePlaylist sp in res.Playlists.Items)
                    {
                        osp.Add(new SpotiPlaylist(sp.Id, sp.Name, sp.Owner, sp.Description));
                    }
                    return osp;
                }
                catch (APIException m)
                {
                    Console.WriteLine(m.Message);
                }

            }
            catch (APITooManyRequestsException e)
            {
                Console.WriteLine(e.RetryAfter);
            }
            catch (APIException e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }


        public async Task<List<SimpleAlbum>> GetSearchedAlbums(string query)
        {
            SearchRequest req = new SearchRequest(SearchRequest.Types.Album, query)
            {
                Market = "pl"
            };
            try
            {
                var res = await Spotify.Search.Item(req);
                return res.Albums.Items;
            }
            catch (APIUnauthorizedException e)
            {
                try
                {
                    Console.WriteLine(e.Message);
                    await this.AuthAsync();
                    var res = await Spotify.Search.Item(req);
                    return res.Albums.Items;
                }
                catch (APIException m)
                {
                    Console.WriteLine(m.Message);
                }

            }
            catch (APITooManyRequestsException e)
            {
                Console.WriteLine(e.RetryAfter);
            }
            catch (APIException e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task<List<FullTrack>> GetSearchedTracks(string query)
        {
            SearchRequest req = new SearchRequest(SearchRequest.Types.Album, query)
            {
                Market = "pl"
            };
            try
            {
                var res = await Spotify.Search.Item(req);
                return res.Tracks.Items;
            }
            catch (APIUnauthorizedException e)
            {
                try
                {
                    Console.WriteLine(e.Message);
                    await this.AuthAsync();
                    var res = await Spotify.Search.Item(req);
                    return res.Tracks.Items;
                }
                catch (APIException m)
                {
                    Console.WriteLine(m.Message);
                }

            }
            catch (APITooManyRequestsException e)
            {
                Console.WriteLine(e.RetryAfter);
            }
            catch (APIException e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task<List<FullArtist>> GetSearchedArtists(string query)
        {
            SearchRequest req = new SearchRequest(SearchRequest.Types.Artist, query)
            {
                Market = "pl"
            };
            try
            {
                var res = await Spotify.Search.Item(req);
                return res.Artists.Items;
            }
            catch (APIUnauthorizedException e)
            {
                try
                {
                    Console.WriteLine(e.Message);
                    await this.AuthAsync();
                    var res = await Spotify.Search.Item(req);
                    return res.Artists.Items;
                }
                catch (APIException m)
                {
                    Console.WriteLine(m.Message);
                }

            }
            catch (APITooManyRequestsException e)
            {
                Console.WriteLine(e.RetryAfter);
            }
            catch (APIException e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task<List<SimplePlaylist>> GetSearchedPlaylists(string query)
        {
            SearchRequest req = new SearchRequest(SearchRequest.Types.Playlist, query)
            {
                Market = "pl"
            };
            try
            {
                var res = await Spotify.Search.Item(req);
                return res.Playlists.Items;
            }
            catch (APIUnauthorizedException e)
            {
                try
                {
                    Console.WriteLine(e.Message);
                    await this.AuthAsync();
                    var res = await Spotify.Search.Item(req);
                    return res.Playlists.Items;
                }
                catch (APIException m)
                {
                    Console.WriteLine(m.Message);
                }

            }
            catch (APITooManyRequestsException e)
            {
                Console.WriteLine(e.RetryAfter);
            }
            catch (APIException e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task<FullArtist> GetArtistById(string id)
        {
            try
            {
                var res = await Spotify.Artists.Get(id);
                return res;
            }
            catch (APIUnauthorizedException e)
            {
                try
                {
                    Console.WriteLine(e.Message);
                    await this.AuthAsync();
                    var res = await Spotify.Artists.Get(id);
                    return res;
                }
                catch (APIException m)
                {
                    Console.WriteLine(m.Message);
                }

            }
            catch (APITooManyRequestsException e)
            {
                Console.WriteLine(e.RetryAfter);
            }
            catch (APIException e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task<FullAlbum> GetAlbumById(string id)
        {
            try
            {
                var res = await Spotify.Albums.Get(id);
                return res;
            }
            catch (APIUnauthorizedException e)
            {
                try
                {
                    Console.WriteLine(e.Message);
                    await this.AuthAsync();
                    var res = await Spotify.Albums.Get(id);
                    return res;
                }
                catch (APIException m)
                {
                    Console.WriteLine(m.Message);
                }

            }
            catch (APITooManyRequestsException e)
            {
                Console.WriteLine(e.RetryAfter);
            }
            catch (APIException e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task<FullPlaylist> GetPlaylistById(string id)
        {
            try
            {
                var res = await Spotify.Playlists.Get(id);
                return res;
            }
            catch (APIUnauthorizedException e)
            {
                try
                {
                    Console.WriteLine(e.Message);
                    await this.AuthAsync();
                    var res = await Spotify.Playlists.Get(id);
                    return res;
                }
                catch (APIException m)
                {
                    Console.WriteLine(m.Message);
                }

            }
            catch (APITooManyRequestsException e)
            {
                Console.WriteLine(e.RetryAfter);
            }
            catch (APIException e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task<FullTrack> GetTrackById(string id)
        {
            try
            {
                var res = await Spotify.Tracks.Get(id);
                return res;
            }
            catch (APIUnauthorizedException e)
            {
                try
                {
                    Console.WriteLine(e.Message);
                    await this.AuthAsync();
                    var res = await Spotify.Tracks.Get(id);
                    return res;
                }
                catch (APIException m)
                {
                    Console.WriteLine(m.Message);
                }

            }
            catch (APITooManyRequestsException e)
            {
                Console.WriteLine(e.RetryAfter);
            }
            catch (APIException e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }




        public async Task<ObservableCollection<FullTrack>> GetTracksFromPlaylist(string id)
        {
            try
            {
                var res = await Spotify.Playlists.GetItems(id);
                ObservableCollection<FullTrack> oft = new ObservableCollection<FullTrack>();
                foreach (PlaylistTrack<IPlayableItem> item in res.Items)
                {
                    if (item.Track is FullTrack track)
                    {
                        oft.Add(new SpotiTrack(track.Id, track.Name, track.Artists, track.Popularity, track.DurationMs, track.Album, track.TrackNumber));
                    }
                }
                return oft;
            }
            catch (APIUnauthorizedException e)
            {
                try
                {
                    Console.WriteLine(e.Message);
                    await this.AuthAsync();
                    var res = await Spotify.Playlists.GetItems(id);
                    ObservableCollection<FullTrack> oft = new ObservableCollection<FullTrack>();
                    foreach (PlaylistTrack<IPlayableItem> item in res.Items)
                    {
                        if (item.Track is FullTrack track)
                        {
                            oft.Add(new SpotiTrack(track.Id, track.Name, track.Artists, track.Popularity, track.DurationMs, track.Album, track.TrackNumber));
                        }
                    }
                    return oft;
                }
                catch (APIException m)
                {
                    Console.WriteLine(m.Message);
                }

            }
            catch (APITooManyRequestsException e)
            {
                Console.WriteLine(e.RetryAfter);
            }
            catch (APIException e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task<List<SimpleAlbum>> GetAlbumsFromArtist(string id)
        {
            try
            {
                var res = await Spotify.Artists.GetAlbums(id);
                return res.Items;
            }
            catch (APIUnauthorizedException e)
            {
                try
                {
                    Console.WriteLine(e.Message);
                    await this.AuthAsync();
                    var res = await Spotify.Artists.GetAlbums(id);
                    return res.Items;
                }
                catch (APIException m)
                {
                    Console.WriteLine(m.Message);
                }

            }
            catch (APITooManyRequestsException e)
            {
                Console.WriteLine(e.RetryAfter);
            }
            catch (APIException e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task<List<SimpleTrack>> GetTracksFromAlbum(string id)
        {
            try
            {
                var res = await Spotify.Albums.GetTracks(id);
                return res.Items;
            }
            catch (APIUnauthorizedException e)
            {
                try
                {
                    Console.WriteLine(e.Message);
                    await this.AuthAsync();
                    var res = await Spotify.Albums.GetTracks(id);
                    return res.Items;
                }
                catch (APIException m)
                {
                    Console.WriteLine(m.Message);
                }

            }
            catch (APITooManyRequestsException e)
            {
                Console.WriteLine(e.RetryAfter);
            }
            catch (APIException e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task<List<FullTrack>> GetTopTracksFromArtist(string id)
        {
            ArtistsTopTracksRequest attr = new ArtistsTopTracksRequest(Market);
            try
            {
                var res = await Spotify.Artists.GetTopTracks(id, attr);
                return res.Tracks;
            }
            catch (APIUnauthorizedException e)
            {
                try
                {
                    Console.WriteLine(e.Message);
                    await this.AuthAsync();
                    var res = await Spotify.Artists.GetTopTracks(id, attr);
                    return res.Tracks;
                }
                catch (APIException m)
                {
                    Console.WriteLine(m.Message);
                }

            }
            catch (APITooManyRequestsException e)
            {
                Console.WriteLine(e.RetryAfter);
            }
            catch (APIException e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}
