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
    public class AlbumsTableViewModel : BaseViewModel
    {
        private NotifyTaskCompletion<ObservableCollection<SpotiAlbum>> _SearchedAlbums;
        public NotifyTaskCompletion<ObservableCollection<SpotiAlbum>> SearchedAlbums
        {
            get { return _SearchedAlbums; }
            set { _SearchedAlbums = value; OnPropertyChanged(nameof(SearchedAlbums)); }
        }

        private string _Query;

        public string Query
        {
            get { return _Query; }
            set { _Query = value; OnPropertyChanged(nameof(Query)); }
        }

        private SpotiAlbum _SpotiAlbum;

        public SpotiAlbum SpotiAlbum
        {
            get { return _SpotiAlbum; }
            set { _SpotiAlbum = value; OnPropertyChanged(nameof(SpotiAlbum)); }
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


        public AlbumsTableViewModel()
        {

        }

        private bool CanSearch()
        {
            return true;
        }

        private void Search()
        {
            SearchedAlbums = new NotifyTaskCompletion<ObservableCollection<SpotiAlbum>>
                (
                    ApiService.GetInstace().GetSearchedAlbums(Query)
                );
        }

        public override BaseViewModel DeepClone()
        {
            AlbumsTableViewModel viewModel = new AlbumsTableViewModel();
            viewModel.Query = this.Query;
            viewModel.SearchCMD.Execute(null);
            return viewModel;
        }
    }
}
