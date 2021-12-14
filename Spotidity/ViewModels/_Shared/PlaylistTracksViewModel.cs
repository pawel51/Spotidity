using Spotidity.Services;
using Spotidity.Stores;
using Spotidity.Models;
using SpotifyAPI.Web;
using System.Collections.ObjectModel;

namespace Spotidity.ViewModels
{
    public class PlaylistTracksViewModel : BaseViewModel
    {
        #region Clonable Properties
        private SpotiPlaylist _SpotiPlaylist;

        public SpotiPlaylist SpotiPlaylist
        {
            get { return _SpotiPlaylist; }
            set { _SpotiPlaylist = value; OnPropertyChanged(nameof(SpotiPlaylist)); }
        }

        private NotifyTaskCompletion<ObservableCollection<SpotiTrack>> _PlaylistTracks;
        public NotifyTaskCompletion<ObservableCollection<SpotiTrack>> PlaylistTracks
        {
            get { return _PlaylistTracks; }
            set { _PlaylistTracks = value; OnPropertyChanged(nameof(PlaylistTracks)); }
        }
        #endregion

        public PlaylistTracksViewModel(SpotiPlaylist spotiPlaylist)
        {
            NavigationStore navigationStore = NavigationStore.GetInstace();
            SpotiPlaylist = spotiPlaylist;
            PlaylistTracks = new NotifyTaskCompletion<ObservableCollection<SpotiTrack>>
                (
                    ApiService.GetInstace().GetTracksFromPlaylist(spotiPlaylist.Id)
                );
        }

        public override BaseViewModel DeepClone()
        {
            return new PlaylistTracksViewModel(this.SpotiPlaylist);
        }
    }
}
