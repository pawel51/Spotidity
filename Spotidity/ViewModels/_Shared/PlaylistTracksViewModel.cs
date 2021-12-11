using Spotidity.Services;
using Spotidity.Stores;
using Spotidity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using System.Collections.ObjectModel;

namespace Spotidity.ViewModels
{
    class PlaylistTracksViewModel : BaseViewModel
    {

        private SpotiPlaylist _SpotiPlaylist;

        public SpotiPlaylist SpotiPlaylist
        {
            get { return _SpotiPlaylist; }
            set { _SpotiPlaylist = value; OnPropertyChanged(nameof(SpotiPlaylist)); }
        }

        private NotifyTaskCompletion<ObservableCollection<FullTrack>> _PlaylistTracks;
        public NotifyTaskCompletion<ObservableCollection<FullTrack>> PlaylistTracks
        {
            get { return _PlaylistTracks; }
            set { _PlaylistTracks = value; OnPropertyChanged(nameof(PlaylistTracks)); }
        }

        public PlaylistTracksViewModel(SpotiPlaylist spotiPlaylist)
        {
            NavigationStore navigationStore = NavigationStore.GetInstace();
            SpotiPlaylist = spotiPlaylist;
            PlaylistTracks = new NotifyTaskCompletion<ObservableCollection<FullTrack>>
                (
                    ApiService.GetInstace().GetTracksFromPlaylist(spotiPlaylist.Id)
                );
        }
    }
}
