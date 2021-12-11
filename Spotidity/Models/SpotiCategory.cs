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

namespace Spotidity.Models
{
    public class SpotiCategory : Category
    {
    

        public SpotiCategory(string id, string name)
        {
            NavigationStore navigationStore = NavigationStore.GetInstace();
            this.Id = id;
            this.Name = name;

            NavigationService<CategoryPlaylistsViewModel> navService = new NavigationService<CategoryPlaylistsViewModel>(
                navigationStore, () => new CategoryPlaylistsViewModel(this));

            GoToPlaylistsOfCategoryCMD = new NavigateCMD<CategoryPlaylistsViewModel>(navService);

        }

        public ICommand GoToPlaylistsOfCategoryCMD { get; }

    }

}
