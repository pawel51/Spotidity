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
    public class TracksTableViewModel : BaseViewModel
    {

        private NotifyTaskCompletion<ObservableCollection<SpotiTrack>> _SearchedTracks;
        public NotifyTaskCompletion<ObservableCollection<SpotiTrack>> SearchedTracks
        {
            get { return _SearchedTracks; }
            set { _SearchedTracks = value; OnPropertyChanged(nameof(SearchedTracks)); }
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

        

        public TracksTableViewModel()
        {
            
        }

        private bool CanSearch()
        {
            return true;
        }

        private void Search()
        {
            SearchedTracks = new NotifyTaskCompletion<ObservableCollection<SpotiTrack>>
                (
                    ApiService.GetInstace().GetSearchedTracks(Query)
                );
        }

        public override BaseViewModel DeepClone()
        {
            TracksTableViewModel viewModel = (TracksTableViewModel)this.MemberwiseClone();
            viewModel.SearchCMD.Execute(null);
            return viewModel;
        }
    }
}
