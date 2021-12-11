using Spotidity.Commands;
using Spotidity.Services;
using Spotidity.Stores;
using Spotidity.ViewModels;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Spotidity.Models
{
    public class SpotiPlaylist : SimplePlaylist
    {
        
        public string OwnerName { get; set; }

        public ICommand GoToPlaylistTracksCMD { get; }

        public SpotiPlaylist(string id, string name, PublicUser ownerName, string description)
        {
            NavigationStore navigationStore = NavigationStore.GetInstace();
            Id = id;
            Name = name;
            if (String.IsNullOrEmpty(ownerName.DisplayName))
                OwnerName = "Spotify";
            else
                OwnerName = ownerName.DisplayName;

            Description = description;
            

            NavigationService<PlaylistTracksViewModel> navService = new NavigationService<PlaylistTracksViewModel>(
                navigationStore, () => new PlaylistTracksViewModel(this));


            GoToPlaylistTracksCMD = new NavigateCMD<PlaylistTracksViewModel>(navService);
        }

        
    }
}
