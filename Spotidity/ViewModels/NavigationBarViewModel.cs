using Spotidity.Commands;
using Spotidity.Services;
using Spotidity.Stores;
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
        }

    }
}
