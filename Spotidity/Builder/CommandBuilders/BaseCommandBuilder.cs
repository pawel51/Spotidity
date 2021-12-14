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

namespace Spotidity.Builder.CommandBuilders
{
    public class BaseCommandBuilder
    {
        private NavigationStore navigationStore = NavigationStore.GetInstace();

        public BaseCommandBuilder()
        {

        }

        //public ICommand GoToCategories()
        //{
        //    GoToCategoriesCMD = new NavigateCMD<CategoryTableViewModel>(new NavigationService<CategoryTableViewModel>
        //        (
        //        navigationStore, () => new CategoryTableViewModel()
        //        ));
        //}
        //public ICommand GoToArtists()
        //{
        //    GoToArtistsCMD = new NavigateCMD<ArtistsTableViewModel>(new NavigationService<ArtistsTableViewModel>
        //        (
        //        navigationStore, () => new ArtistsTableViewModel()
        //        ));
        //}
        //public ICommand GoToPlaylists()
        //{
        //    GoToPlaylistsCMD = new NavigateCMD<PlaylistsTableViewModel>(new NavigationService<PlaylistsTableViewModel>
        //        (
        //        navigationStore, () => new PlaylistsTableViewModel()
        //        ));
        //}
        //public ICommand GoToTracks()
        //{
        //    GoToTracksCMD = new NavigateCMD<TracksTableViewModel>(new NavigationService<TracksTableViewModel>
        //        (
        //        navigationStore, () => new TracksTableViewModel()
        //        ));
        //}
        //public ICommand GoToHome()
        //{
        //    GoToHomeCMD = new NavigateCMD<HomeViewModel>(new NavigationService<HomeViewModel>
        //        (
        //        navigationStore, () => new HomeViewModel(new Models.Credentials("", ""))
        //        ));
        //}
        //public ICommand GoToAlbums()
        //{
        //    GoToAlbumsCMD = new NavigateCMD<AlbumsTableViewModel>(new NavigationService<AlbumsTableViewModel>
        //        (
        //        navigationStore, () => new AlbumsTableViewModel()
        //        ));
        //}
        //public ICommand GoBack()
        //{

        //}
        //public ICommand GoForward()
        //{

        //}
        public ICommand GoToAlbumTracks(string albumId)
        {
            NavigationService<AlbumTracksViewModel> navigationService = new NavigationService<AlbumTracksViewModel>(
                navigationStore, () => new AlbumTracksViewModel(albumId));
            return new NavigateCMD<AlbumTracksViewModel>(navigationService);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="artistId"></param>
        /// <returns></returns>
        public ICommand GoToArtist(string artistId)
        {
            NavigationService<ArtistDetailsViewModel> navigationArtistsService = new NavigationService<ArtistDetailsViewModel>(
                navigationStore, () => new ArtistDetailsViewModel(artistId));
            return new NavigateCMD<ArtistDetailsViewModel>(navigationArtistsService);

        }
        
    }
}
