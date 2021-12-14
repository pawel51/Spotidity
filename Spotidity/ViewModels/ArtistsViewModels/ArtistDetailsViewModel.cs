using Spotidity.Services;
using Spotidity.Stores;
using Spotidity.Models;
using SpotifyAPI.Web;
using System.Collections.ObjectModel;

namespace Spotidity.ViewModels
{
    public class ArtistDetailsViewModel : BaseViewModel
    {
        private NotifyTaskCompletion<SpotiArtist> _SpotiArtist;

        public NotifyTaskCompletion<SpotiArtist> SpotiArtist
        {
            get { return _SpotiArtist; }
            set { _SpotiArtist = value; OnPropertyChanged(nameof(SpotiArtist)); }
        }

        private NotifyTaskCompletion<ObservableCollection<SpotiAlbum>> _ArtistAlbums;
        public NotifyTaskCompletion<ObservableCollection<SpotiAlbum>> ArtistAlbums
        {
            get { return _ArtistAlbums; }
            set { _ArtistAlbums = value; OnPropertyChanged(nameof(ArtistAlbums)); }
        }

        private NotifyTaskCompletion<ObservableCollection<SpotiTrack>> _TopTracks;
        public NotifyTaskCompletion<ObservableCollection<SpotiTrack>> TopTracks
        {
            get { return _TopTracks; }
            set { _TopTracks = value; OnPropertyChanged(nameof(TopTracks)); }
        }



        public ArtistDetailsViewModel(string id)
        {
            NavigationStore navigationStore = NavigationStore.GetInstace();
            // get additional info
            SpotiArtist = new NotifyTaskCompletion<SpotiArtist>
                (
                    ApiService.GetInstace().GetArtistById(id)
                );

            ArtistAlbums = new NotifyTaskCompletion<ObservableCollection<SpotiAlbum>>
                (
                    ApiService.GetInstace().GetAlbumsFromArtist(id)
                );

            TopTracks = new NotifyTaskCompletion<ObservableCollection<SpotiTrack>>
                (
                    ApiService.GetInstace().GetTopTracksFromArtist(id)
                );
            

        }

        public override BaseViewModel DeepClone()
        {
            if (this.SpotiArtist.Result == null)
                return new HomeViewModel(new Credentials("error", "error"));

            return new ArtistDetailsViewModel(this.SpotiArtist.Result.Id);
        }
    }
}
