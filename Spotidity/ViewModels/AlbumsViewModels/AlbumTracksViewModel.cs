using Spotidity.Services;
using Spotidity.Stores;
using Spotidity.Models;
using SpotifyAPI.Web;
using Spotidity.Helpers;
using System.Collections.ObjectModel;


namespace Spotidity.ViewModels
{
    public class AlbumTracksViewModel : BaseViewModel
    {

        private NotifyTaskCompletion<SpotiAlbum> _SpotiAlbum;

        public NotifyTaskCompletion<SpotiAlbum> SpotiAlbum
        {
            get { return _SpotiAlbum; }
            set { _SpotiAlbum = value; OnPropertyChanged(nameof(SpotiAlbum)); }
        }

        private NotifyTaskCompletion<ObservableCollection<SpotiTrack>> _AlbumTracks;
        public NotifyTaskCompletion<ObservableCollection<SpotiTrack>> AlbumTracks
        {
            get { return _AlbumTracks; }
            set { _AlbumTracks = value; OnPropertyChanged(nameof(AlbumTracks)); }
        }

        



        public AlbumTracksViewModel(string id)
        {
            NavigationStore navigationStore = NavigationStore.GetInstace();
            // get additional info
            SpotiAlbum = new NotifyTaskCompletion<SpotiAlbum>
                (
                    ApiService.GetInstace().GetAlbumById(id)
                );


            AlbumTracks = new NotifyTaskCompletion<ObservableCollection<SpotiTrack>>
                (
                    ApiService.GetInstace().GetTracksFromAlbum(id)
                );
        }

        public override BaseViewModel DeepClone()
        {
            if (SpotiAlbum.Result == null) 
                return new HomeViewModel(new Credentials("error", "error"));

            return new AlbumTracksViewModel(this.SpotiAlbum.Result.Id);
        }
    }
}
