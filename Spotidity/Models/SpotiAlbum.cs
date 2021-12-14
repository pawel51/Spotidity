using Spotidity.Commands;
using Spotidity.Services;
using Spotidity.Stores;
using Spotidity.ViewModels;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Spotidity.Models
{
    public class SpotiAlbum : SpotifyAPI.Web.FullAlbum, INotifyPropertyChanged
    {

        public ObservableCollection<SpotiArtist> ArtistsCollection { get; set; }


        public string AlbumGroup { get; }

        private string _Name;

        public string DName
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged(nameof(DName)); }
        }
        private string _DReleaseDate;

        public string DReleaseDate
        {
            get { return _DReleaseDate; }
            set { _DReleaseDate = value; OnPropertyChanged(nameof(DReleaseDate)); }
        }
        private string _DAlbumType;

        public string DAlbumType
        {
            get { return _DAlbumType; }
            set { _DAlbumType = value; OnPropertyChanged(nameof(DAlbumType)); }
        }
        private int _DTotalTracks;

        public int DTotalTracks
        {
            get { return _DTotalTracks; }
            set { _DTotalTracks = value; OnPropertyChanged(nameof(DTotalTracks)); }
        }
        private int _DPopularity;

        public int DPopularity
        {
            get { return _DPopularity; }
            set { _DPopularity = value; OnPropertyChanged(nameof(DPopularity)); }
        }
        private string _DGenresStr;

        public string DGenresStr
        {
            get { return _DGenresStr; }
            set { _DGenresStr = value; OnPropertyChanged(nameof(DGenresStr)); }
        }

        private bool selectedArtistWasChanged = false;

        private SpotiArtist _SelectedArtist;

        public SpotiArtist SelectedArtist
        {
            get { return _SelectedArtist; }
            set {
                
                _SelectedArtist = value;
                OnPropertyChanged(nameof(SelectedArtist));
                GoToArtistCMD.Execute(null);
            }
        }


        

        private string _GenresStr;

        public string GenresStr
        {
            get { return _GenresStr; }
            set { _GenresStr = value; }
        }

        public ICommand GoToAlbumTracksCMD { get; }
        public ICommand GoToArtistCMD { get; }

        public SpotiAlbum(string id, string name)
        {
            Id = id;
            Name = name;

            NavigationStore navigationStore = NavigationStore.GetInstace();
            //Goto album details
            NavigationService<AlbumTracksViewModel> navigationService = new NavigationService<AlbumTracksViewModel>(
                navigationStore, () => new AlbumTracksViewModel(this.Id));
            GoToAlbumTracksCMD = new NavigateCMD<AlbumTracksViewModel>(navigationService);
        }


        public SpotiAlbum(string id
            , string name
            , List<SimpleArtist> artists
            , string releaseDate
            , string albumType
            , string albumGroup
            , int totalTracks
            , string type)
        {
            Id = id;
            Name = name;
            ArtistsCollection = new ObservableCollection<SpotiArtist>();
            foreach (SimpleArtist sa in artists)
            {
                ArtistsCollection.Add(new SpotiArtist(sa.Id, sa.Name));
            }

            Artists = artists;
            ReleaseDate = releaseDate;
            AlbumType = albumType;
            AlbumGroup = albumGroup;
            TotalTracks = totalTracks;
            Type = type;


            NavigationStore navigationStore = NavigationStore.GetInstace();
            //goto artist details
            NavigationService<ArtistDetailsViewModel> navigationArtistsService = new NavigationService<ArtistDetailsViewModel>(
                navigationStore, () => new ArtistDetailsViewModel(this.SelectedArtist.Id));
            GoToArtistCMD = new NavigateCMD<ArtistDetailsViewModel>(navigationArtistsService);

            //Goto album details
            NavigationService<AlbumTracksViewModel> navigationService = new NavigationService<AlbumTracksViewModel>(
                navigationStore, () => new AlbumTracksViewModel(this.Id));
            GoToAlbumTracksCMD = new NavigateCMD<AlbumTracksViewModel>(navigationService);

        }

        

        public SpotiAlbum(FullAlbum res)
        {
            Id = res.Id;
            DName = res.Name;
            ArtistsCollection = new ObservableCollection<SpotiArtist>();
            foreach (SimpleArtist sa in res.Artists)
            {
                ArtistsCollection.Add(new SpotiArtist(sa.Id, sa.Name));
            }
            
            DReleaseDate = res.ReleaseDate;
            DAlbumType = res.AlbumType;
            DTotalTracks = res.TotalTracks;
            DPopularity = res.Popularity;
            DGenresStr = String.Join(", ", res.Genres);

            NavigationStore navigationStore = NavigationStore.GetInstace();
            //go to artists
            NavigationService<ArtistDetailsViewModel> navigationArtistsService = new NavigationService<ArtistDetailsViewModel>(
                navigationStore, () => new ArtistDetailsViewModel(this.SelectedArtist.Id));
            GoToArtistCMD = new NavigateCMD<ArtistDetailsViewModel>(navigationArtistsService);
            //go to albums
            NavigationService<AlbumTracksViewModel> navigationService = new NavigationService<AlbumTracksViewModel>(
                navigationStore, () => new AlbumTracksViewModel(this.Id));
            GoToAlbumTracksCMD = new NavigateCMD<AlbumTracksViewModel>(navigationService);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
