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
    public class SpotiTrack : FullTrack, INotifyPropertyChanged
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
            set { _SelectedArtist = value; OnPropertyChanged(nameof(SelectedArtist)); }
        }

        private string _DurationStr;

        public string DurationStr
        {
            get { return _DurationStr; }
            set { _DurationStr = value; }
        }

        #endregion



        #region Private Fields
        private bool selectedAlbumWasChanged = false;

        #endregion


        #region Commands

        public ICommand GoToArtistCMD { get; }
        public ICommand GoToAlbumCMD { get; }

        #endregion


        public SpotiTrack(string id, string name, List<SimpleArtist> artists, int popularity, int durationMs, SimpleAlbum album, int trackNumber)
        {
            NavigationStore navigationStore = NavigationStore.GetInstace();

            Id = id;
            Name = name;
            Artists = artists;
            Popularity = popularity;
            DurationMs = durationMs;
            TimeSpan ts = TimeSpan.FromMilliseconds(durationMs);
            DurationStr = string.Format("{0:D2}m:{1:D2}s", ts.Minutes, ts.Seconds);
            Album = album;
            TrackNumber = trackNumber;

            if (Artists.Count > 0)
            {
                SelectedArtist = Artists[0];
            }
            //goto artist


            //goto album


           


        }

    }
}
