using Spotidity.Commands;
using Spotidity.Services;
using Spotidity.Stores;
using Spotidity.ViewModels;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Spotidity.Models
{
    public class SpotiArtist : FullArtist, INotifyPropertyChanged
    {
        private string _Name;

        public string DName
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged(nameof(DName)); }
        }

        private string _Type;

        public string DType
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged(nameof(DType)); }
        }

        private string _Genres;

        public string GenresStr
        {
            get { return _Genres; }
            set { _Genres = value; OnPropertyChanged(nameof(GenresStr)); }
        }

        private int _TotalFollowers;

        public int TotalFollowers
        {
            get { return _TotalFollowers; }
            set { _TotalFollowers = value; OnPropertyChanged(nameof(TotalFollowers)); }
        }

        private int _DPopularity;

        public int DPopularity
        {
            get { return _DPopularity; }
            set { _DPopularity = value; OnPropertyChanged(nameof(DPopularity)); }
        }

        


        public SpotiArtist(string id, string name)
        {
            Id = id;
            Name = name;

            NavigationStore navigationStore = NavigationStore.GetInstace();

            NavigationService<ArtistDetailsViewModel> navigationService = new NavigationService<ArtistDetailsViewModel>(
                navigationStore, () => new ArtistDetailsViewModel(this.Id)
                );

            GoToArtistCMD = new NavigateCMD<ArtistDetailsViewModel>(navigationService);
        }

        public ICommand GoToArtistCMD { get; }

        public SpotiArtist(string id
            , string name
            , string type
            , int totalFollowers
            , int popularity
            , List<string> genresStr)
        {
            Id = id;
            DName = name;
            DType = type;
            TotalFollowers = totalFollowers;
            GenresStr = String.Join(", ", genresStr);
            Popularity = popularity;

            NavigationStore navigationStore = NavigationStore.GetInstace();

            NavigationService<ArtistDetailsViewModel> navigationService = new NavigationService<ArtistDetailsViewModel>(
                navigationStore, () => new ArtistDetailsViewModel(this.Id)
                );

            GoToArtistCMD = new NavigateCMD<ArtistDetailsViewModel>(navigationService);
        }

        

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
