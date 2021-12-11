using Spotidity.Services;
using Spotidity.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotidity.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        private NavigationStore _navigationStore;

        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;

        private BaseViewModel _NavbarViewModel = new NavigationBarViewModel();

        public BaseViewModel NavbarViewModel
        {
            get { return _NavbarViewModel; }
            set { _NavbarViewModel = value; OnPropertyChanged(nameof(NavbarViewModel)); }
        }



        public MainViewModel(NavigationStore navigationStore)
        {
            _ = ApiService.GetInstace().AuthAsync();
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }




    }
}
