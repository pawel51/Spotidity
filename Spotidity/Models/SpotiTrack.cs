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

namespace Spotidity.Models
{
    public class SpotiTrack : FullTrack, INotifyPropertyChanged, ISpotiModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Properties
            

        

        private SimpleArtist _SelectedArtist;

        public SimpleArtist SelectedArtist
        {
            get { return _SelectedArtist; }
            set {
                _SelectedArtist = value;
                OnPropertyChanged(nameof(SelectedArtist));
                GoToArtistCMD.Execute(null);
            }
        }

        private string _DurationStr;

        public string DurationStr
        {
            get { return _DurationStr; }
            set { _DurationStr = value; OnPropertyChanged(nameof(DurationStr)); }
        }

        #endregion



        #region Private Fields
        private bool selectedArtistWasChanged = false;

        #endregion


        #region Commands

        public ICommand GoToArtistCMD { get; set; }

       
        public ICommand GoToAlbumCMD { get; set; }

        #endregion


        public SpotiTrack(string id, string name, List<SimpleArtist> artists, int popularity, int durationMs, SimpleAlbum album, int trackNumber)
        {
            

            Id = id;
            Name = name;
            Artists = artists;
            Popularity = popularity;
            DurationMs = durationMs;
            TimeSpan ts = TimeSpan.FromMilliseconds(durationMs);
            DurationStr = string.Format("{0:D2}m:{1:D2}s", ts.Minutes, ts.Seconds);
            Album = album;
            TrackNumber = trackNumber;

            

            NavigationStore navigationStore = NavigationStore.GetInstace();
            //goto artist
            NavigationService<ArtistDetailsViewModel> navigationArtistsService = new NavigationService<ArtistDetailsViewModel>(
                navigationStore, () => new ArtistDetailsViewModel(this.SelectedArtist.Id));
            GoToArtistCMD = new NavigateCMD<ArtistDetailsViewModel>(navigationArtistsService);

            //goto album
            NavigationService<AlbumTracksViewModel> navigationAlbumsService = new NavigationService<AlbumTracksViewModel>(
                navigationStore, () => new AlbumTracksViewModel(this.Album.Id));
            GoToAlbumCMD = new NavigateCMD<AlbumTracksViewModel>(navigationAlbumsService);

        }

        public SpotiTrack(string id, string name, List<SimpleArtist> artists, int durationMs, int trackNumber)
        {
            //Tracks in album
            Id = id;
            Name = name;
            Artists = artists;
            DurationMs = durationMs;
            TimeSpan ts = TimeSpan.FromMilliseconds(durationMs);
            DurationStr = string.Format("{0:D2}m:{1:D2}s", ts.Minutes, ts.Seconds);
            TrackNumber = trackNumber;

            NavigationStore navigationStore = NavigationStore.GetInstace();
            //goto artist
            NavigationService<ArtistDetailsViewModel> navigationArtistsService = new NavigationService<ArtistDetailsViewModel>(
                navigationStore, () => new ArtistDetailsViewModel(this.SelectedArtist.Id));
            GoToArtistCMD = new NavigateCMD<ArtistDetailsViewModel>(navigationArtistsService);
        }

        
        public SpotiTrack(FullTrack t)
        {

            Id = t.Id;
            Name = t.Name;
            Artists = t.Artists;
            Popularity = t.Popularity;
            DurationMs = t.DurationMs;
            TimeSpan ts = TimeSpan.FromMilliseconds(DurationMs);
            DurationStr = string.Format("{0:D2}m:{1:D2}s", ts.Minutes, ts.Seconds);
            Album = t.Album;
            TrackNumber = t.TrackNumber;

            
            NavigationStore navigationStore = NavigationStore.GetInstace();
            //goto artist
            NavigationService<ArtistDetailsViewModel> navigationArtistsService = new NavigationService<ArtistDetailsViewModel>(
                navigationStore, () => new ArtistDetailsViewModel(this.SelectedArtist.Id));
            GoToArtistCMD = new NavigateCMD<ArtistDetailsViewModel>(navigationArtistsService);

            //goto album

            NavigationService<AlbumTracksViewModel> navigationAlbumsService = new NavigationService<AlbumTracksViewModel>(
                navigationStore, () => new AlbumTracksViewModel(this.Album.Id));
            GoToAlbumCMD = new NavigateCMD<AlbumTracksViewModel>(navigationAlbumsService);
        }

        public SpotiTrack()
        {
        }
    }
}
