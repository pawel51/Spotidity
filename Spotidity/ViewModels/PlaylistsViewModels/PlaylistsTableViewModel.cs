using Spotidity.Services;
using Spotidity.Stores;
using Spotidity.Models;
using SpotifyAPI.Web;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Spotidity.Commands;
using System;

namespace Spotidity.ViewModels
{
    public class PlaylistsTableViewModel : BaseViewModel
    {
        private NotifyTaskCompletion<ObservableCollection<SpotiPlaylist>> _SearchedPlaylists;
        public NotifyTaskCompletion<ObservableCollection<SpotiPlaylist>> SearchedPlaylists
        {
            get { return _SearchedPlaylists; }
            set { _SearchedPlaylists = value; OnPropertyChanged(nameof(SearchedPlaylists)); }
        }

        private string _Query;

        public string Query
        {
            get { return _Query; }
            set { _Query = value; OnPropertyChanged(nameof(Query)); }
        }


        private RelayCommand _SearchCMD;
        public RelayCommand SearchCMD
        {
            get
            {
                if (_SearchCMD == null)
                    _SearchCMD = new RelayCommand(param => Search(), param => CanSearch());
                return _SearchCMD;
            }
            set { _SearchCMD = value; OnPropertyChanged(nameof(SearchCMD)); }
        }



        public PlaylistsTableViewModel()
        {

        }

        private bool CanSearch()
        {
            return true;
        }

        private void Search()
        {
            SearchedPlaylists = new NotifyTaskCompletion<ObservableCollection<SpotiPlaylist>>
                (
                    ApiService.GetInstace().GetSearchedPlaylists(Query)
                );
        }

        public override BaseViewModel DeepClone()
        {
            PlaylistsTableViewModel viewModel = new PlaylistsTableViewModel();
            viewModel.Query = this.Query;
            viewModel.SearchCMD.Execute(null);
            return viewModel;
        }
    }
}
