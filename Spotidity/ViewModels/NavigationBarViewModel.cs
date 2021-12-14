using Spotidity.Commands;
using Spotidity.Services;
using Spotidity.Stores;
using Spotidity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Spotidity.ViewModels
{
    public class NavigationBarViewModel : BaseViewModel
    {
        public ICommand GoToCategoriesCMD { get; }
        public ICommand GoToArtistsCMD { get; }
        public ICommand GoToPlaylistsCMD { get; }
        public ICommand GoToTracksCMD { get; }
        public ICommand GoToHomeCMD { get; }
        public ICommand GoToAlbumsCMD { get; }
        public ICommand GoBackCMD { get; }
        public ICommand GoForwardCMD { get; }

        public NavigationBarViewModel()
        {
            NavigationStore navigationStore = NavigationStore.GetInstace();

            GoToCategoriesCMD = new NavigateCMD<CategoryTableViewModel>(new NavigationService<CategoryTableViewModel>
                (
                navigationStore, () => new CategoryTableViewModel()
                ));

            GoToArtistsCMD = new NavigateCMD<ArtistsTableViewModel>(new NavigationService<ArtistsTableViewModel>
                (
                navigationStore, () => new ArtistsTableViewModel()
                ));

            GoToPlaylistsCMD = new NavigateCMD<PlaylistsTableViewModel>(new NavigationService<PlaylistsTableViewModel>
                (
                navigationStore, () => new PlaylistsTableViewModel()
                ));

            GoToTracksCMD = new NavigateCMD<TracksTableViewModel>(new NavigationService<TracksTableViewModel>
                (
                navigationStore, () => new TracksTableViewModel()
                ));

            GoToHomeCMD = new NavigateCMD<HomeViewModel>(new NavigationService<HomeViewModel>
                (
                navigationStore, () => new HomeViewModel(new Models.Credentials("",""))
                ));

            GoToAlbumsCMD = new NavigateCMD<AlbumsTableViewModel>(new NavigationService<AlbumsTableViewModel>
                (
                navigationStore, () => new AlbumsTableViewModel()
                ));

            GoBackCMD = new NavigateBackCMD(new BackNavigationService(navigationStore));
            GoForwardCMD = new NavigateForwardCMD(new ForwardNavigationService(navigationStore));
        }

        public override BaseViewModel DeepClone()
        {
            throw new NotImplementedException();
        }
    }
}
