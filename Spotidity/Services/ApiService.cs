using Newtonsoft.Json;
using Spotidity.Builder.SpotiBuilders;
using Spotidity.Models;
using Spotidity.Stores;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
            //await service.AuthAsync();
            //List<FullTrack> topTracks = await service.GetTopTracksFromArtist("0MIG6gMcQTSvFbKvUwK0id");
            //Console.WriteLine(topTracks);



        }

        
        internal class Creds{
            
            public string appid;
            public string secret;
        }


        public async Task AuthAsync()
        {

            
            try
            {
                Creds creds;
                using (StreamReader r = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "\\Users\\pawel\\source\\repos\\Spotidity001\\Spotidity\\Services\\secrets.json")))
                {
                    string json = r.ReadToEnd();
                    creds = JsonConvert.DeserializeObject<Creds>(json);
                }
                var config = SpotifyClientConfig.CreateDefault();
                var request = new ClientCredentialsRequest(creds.appid, creds.secret);
                var response = await new OAuthClient(config).RequestToken(request);
                Spotify = new SpotifyClient(config.WithToken(response.AccessToken));
            } catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
           

            
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


        public async Task<ObservableCollection<SpotiAlbum>> GetSearchedAlbums(string query)
        {
            SearchRequest req = new SearchRequest(SearchRequest.Types.Album, query)
            {
                Market = "us"
            };
            try
            {
                var res = await Spotify.Search.Item(req);
                ObservableCollection<SpotiAlbum> osp = new ObservableCollection<SpotiAlbum>();
                foreach (SimpleAlbum sa in res.Albums.Items)
                {
                    osp.Add(new SpotiAlbum(sa.Id, sa.Name, sa.Artists, sa.ReleaseDate, sa.AlbumType, sa.AlbumGroup, sa.TotalTracks, sa.Type));
                }
                return osp;
            }
            catch (APIUnauthorizedException e)
            {
                try
                {
                    Console.WriteLine(e.Message);
                    await this.AuthAsync();
                    var res = await Spotify.Search.Item(req);
                    ObservableCollection<SpotiAlbum> osp = new ObservableCollection<SpotiAlbum>();
                    foreach (SimpleAlbum sa in res.Albums.Items)
                    {
                        osp.Add(new SpotiAlbum(sa.Id, sa.Name, sa.Artists, sa.ReleaseDate, sa.AlbumType, sa.AlbumGroup, sa.TotalTracks, sa.Type));
                    }
                    return osp;
                }
                catch (APIException m)
                {
                    Console.WriteLine("API Searched Albums"+m.Message);
                }

            }
            catch (APITooManyRequestsException e)
            {
                Console.WriteLine(e.RetryAfter);
            }
            catch (APIException e)
            {
                Console.WriteLine("API Searched Albums" + e.Message);
            }
            return null;
        }

        public async Task<ObservableCollection<SpotiTrack>> GetSearchedTracks(string query)
        {
            SearchRequest req = new SearchRequest(SearchRequest.Types.Track, query)
            {
                Market = "pl"
            };
            try
            {
                var res = await Spotify.Search.Item(req);

                TrackBuilder builder = new TrackBuilder(res.Tracks.Items);
                builder.createProps();

                return new ObservableCollection<SpotiTrack>(builder.getResult().Cast<SpotiTrack>());
            }
            catch (APIUnauthorizedException e)
            {
                try
                {
                    Console.WriteLine(e.Message);
                    await this.AuthAsync();
                    var res = await Spotify.Search.Item(req);

                    TrackBuilder builder = new TrackBuilder(res.Tracks.Items);
                    builder.createProps();

                    return new ObservableCollection<SpotiTrack>(builder.getResult().Cast<SpotiTrack>());
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

        public async Task<ObservableCollection<SpotiArtist>> GetSearchedArtists(string query)
        {
            SearchRequest req = new SearchRequest(SearchRequest.Types.Artist, query)
            {
                Market = "pl"
            };
            try
            {
                var res = await Spotify.Search.Item(req);
                ObservableCollection<SpotiArtist> osp = new ObservableCollection<SpotiArtist>();
                foreach (FullArtist sa in res.Artists.Items)
                {
                    osp.Add(new SpotiArtist(sa.Id, sa.Name, sa.Type, sa.Followers.Total, sa.Popularity, sa.Genres));
                }
                return osp;
            }
            catch (APIUnauthorizedException e)
            {
                try
                {
                    Console.WriteLine(e.Message);
                    await this.AuthAsync();
                    var res = await Spotify.Search.Item(req);
                    ObservableCollection<SpotiArtist> osp = new ObservableCollection<SpotiArtist>();
                    foreach (FullArtist sa in res.Artists.Items)
                    {
                        osp.Add(new SpotiArtist(sa.Id, sa.Name, sa.Type, sa.Followers.Total, sa.Popularity, sa.Genres));
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

        public async Task<ObservableCollection<SpotiPlaylist>> GetSearchedPlaylists(string query)
        {
            SearchRequest req = new SearchRequest(SearchRequest.Types.Playlist, query)
            {
                Market = "pl"
            };
            try
            {
                var res = await Spotify.Search.Item(req);
                ObservableCollection<SpotiPlaylist> playlists = new ObservableCollection<SpotiPlaylist>();
                foreach (SimplePlaylist pl in res.Playlists.Items)
                {
                    playlists.Add(new SpotiPlaylist(pl.Id, pl.Name, pl.Owner, pl.Description));
                }


                return playlists;
            }
            catch (APIUnauthorizedException e)
            {
                try
                {
                    Console.WriteLine(e.Message);
                    await this.AuthAsync();
                    var res = await Spotify.Search.Item(req);
                    ObservableCollection<SpotiPlaylist> playlists = new ObservableCollection<SpotiPlaylist>();
                    foreach (SpotiPlaylist pl in res.Playlists.Items)
                    {
                        playlists.Add(new SpotiPlaylist(pl.Id, pl.Name, pl.Owner, pl.Description));
                    }


                    return playlists;
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

        public async Task<SpotiArtist> GetArtistById(string id)
        {
            try
            {
                var res = await Spotify.Artists.Get(id);
                return new SpotiArtist(res.Id, res.Name, res.Type, res.Followers.Total, res.Popularity, res.Genres);
            }
            catch (APIUnauthorizedException e)
            {
                try
                {
                    Console.WriteLine(e.Message);
                    await this.AuthAsync();
                    var res = await Spotify.Artists.Get(id);
                    return new SpotiArtist(res.Id, res.Name, res.Type, res.Followers.Total, res.Popularity, res.Genres);
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

        public async Task<SpotiAlbum> GetAlbumById(string id)
        {
            try
            {
                var res = await Spotify.Albums.Get(id);
                return new SpotiAlbum(res);
            }
            catch (APIUnauthorizedException e)
            {
                try
                {
                    Console.WriteLine(e.Message);
                    await this.AuthAsync();
                    var res = await Spotify.Albums.Get(id);
                    return new SpotiAlbum(res);
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
                ObservableCollection<FullTrack> oft = new ObservableCollection<FullTrack>();
                //foreach (FullTrack item in res)
                //{
                //    if (item is FullTrack track)
                //    {
                //        oft.Add(new SpotiTrack(track.Id, track.Name, track.Artists, track.Popularity, track.DurationMs, track.Album, track.TrackNumber));
                //    }
                //}
                //return oft;
            }
            catch (APIUnauthorizedException e)
            {
                try
                {
                    Console.WriteLine(e.Message);
                    await this.AuthAsync();
                    var res = await Spotify.Tracks.Get(id);
                    //ObservableCollection<FullTrack> oft = new ObservableCollection<FullTrack>();
                    //foreach (FullTrack track in res)
                    //{
                    //    oft.Add(new SpotiTrack(track.Id, track.Name, track.Artists, track.Popularity, track.DurationMs, track.Album, track.TrackNumber));
                    // }
                    //return oft;
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




        public async Task<ObservableCollection<SpotiTrack>> GetTracksFromPlaylist(string id)
        {
            try
            {
                var res = await Spotify.Playlists.GetItems(id);
                ObservableCollection<SpotiTrack> oft = new ObservableCollection<SpotiTrack>();
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
                    ObservableCollection<SpotiTrack> oft = new ObservableCollection<SpotiTrack>();
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

        public async Task<ObservableCollection<SpotiAlbum>> GetAlbumsFromArtist(string id)
        {
            try
            {
                var res = await Spotify.Artists.GetAlbums(id);
                ObservableCollection<SpotiAlbum> osa = new ObservableCollection<SpotiAlbum>();

                foreach(SimpleAlbum sa in res.Items)
                {
                    osa.Add(new SpotiAlbum(sa.Id, sa.Name, sa.Artists, sa.ReleaseDate, sa.AlbumType, sa.AlbumGroup, sa.TotalTracks, sa.Type));
                }
                
                return osa;
            }
            catch (APIUnauthorizedException e)
            {
                try
                {
                    Console.WriteLine(e.Message);
                    await this.AuthAsync();
                    var res = await Spotify.Artists.GetAlbums(id);
                    ObservableCollection<SpotiAlbum> osa = new ObservableCollection<SpotiAlbum>();

                    foreach (SimpleAlbum sa in res.Items)
                    {
                        osa.Add(new SpotiAlbum(sa.Id, sa.Name, sa.Artists, sa.ReleaseDate, sa.AlbumType, sa.AlbumGroup, sa.TotalTracks, sa.Type));
                    }

                    return osa;
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

        public async Task<ObservableCollection<SpotiTrack>> GetTracksFromAlbum(string id)
        {
            try
            {
                var res = await Spotify.Albums.GetTracks(id);
                ObservableCollection<SpotiTrack> oft = new ObservableCollection<SpotiTrack>();
                foreach (SimpleTrack track in res.Items)
                {

                    oft.Add(new SpotiTrack(track.Id, track.Name, track.Artists, track.DurationMs, track.TrackNumber));

                }
                return oft;
            }
            catch (APIUnauthorizedException e)
            {
                try
                {
                    Console.WriteLine(e.Message);
                    await this.AuthAsync();
                    var res = await Spotify.Albums.GetTracks(id);
                    ObservableCollection<SpotiTrack> oft = new ObservableCollection<SpotiTrack>();
                    foreach (SimpleTrack track in res.Items)
                    {

                            oft.Add(new SpotiTrack(track.Id, track.Name, track.Artists, track.DurationMs, track.TrackNumber));

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

        public async Task<ObservableCollection<SpotiTrack>> GetTopTracksFromArtist(string id)
        {
            ArtistsTopTracksRequest attr = new ArtistsTopTracksRequest(Market);
            try
            {
                var res = await Spotify.Artists.GetTopTracks(id, attr);
                ObservableCollection<SpotiTrack> ost = new ObservableCollection<SpotiTrack>();
                foreach(FullTrack t in res.Tracks)
                {
                    ost.Add(new SpotiTrack(t));
                }

                return ost;
            }
            catch (APIUnauthorizedException e)
            {
                try
                {
                    Console.WriteLine(e.Message);
                    await this.AuthAsync();
                    var res = await Spotify.Artists.GetTopTracks(id, attr);
                    ObservableCollection<SpotiTrack> ost = new ObservableCollection<SpotiTrack>();
                    foreach (FullTrack t in res.Tracks)
                    {
                        ost.Add(new SpotiTrack(t));
                    }

                    return ost;
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
