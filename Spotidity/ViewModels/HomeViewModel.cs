using Spotidity.Commands;
using Spotidity.Models;
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
    public class HomeViewModel : BaseViewModel
    {
        private readonly Credentials _credentials;

        
        public ICommand GoToCategoriesCMD { get; }
        public ICommand GoToArtistsCMD { get; }
        public ICommand GoToPlaylistsCMD { get; }
        public ICommand GoToTracksCMD { get; }



        public string ValAppId => _credentials.ValAppId;

        public string ValSecret => _credentials.ValSecret;

        public HomeViewModel(Credentials creds)
        {
            _credentials = creds;
            NavigationStore navigationStore = NavigationStore.GetInstace();

            GoToCategoriesCMD = new NavigateCMD<CategoryTableViewModel>(new NavigationService<CategoryTableViewModel>
                (
                navigationStore, () => new CategoryTableViewModel()
                ));


            
        } 
    }
}
