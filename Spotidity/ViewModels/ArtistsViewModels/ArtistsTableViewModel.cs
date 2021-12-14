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
    public class ArtistsTableViewModel : BaseViewModel
    {
        public ArtistsTableViewModel()
        {
            ;
        }


        private NotifyTaskCompletion<ObservableCollection<SpotiArtist>> _SearchedArtists;
        public NotifyTaskCompletion<ObservableCollection<SpotiArtist>> SearchedArtists
        {
            get { return _SearchedArtists; }
            set { _SearchedArtists = value; OnPropertyChanged(nameof(SearchedArtists)); }
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


        private bool CanSearch()
        {
            return true;
        }

        private void Search()
        {
            SearchedArtists = new NotifyTaskCompletion<ObservableCollection<SpotiArtist>>
                (
                    ApiService.GetInstace().GetSearchedArtists(Query)
                );
        }

        public override BaseViewModel DeepClone()
        {
            ArtistsTableViewModel viewModel = new ArtistsTableViewModel();
            viewModel.Query = this.Query;
            viewModel.SearchCMD.Execute(null);
            return viewModel;
        }
    }
}
