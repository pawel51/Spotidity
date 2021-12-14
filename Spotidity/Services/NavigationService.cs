using Spotidity.Stores;
using Spotidity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotidity.Services
{
    public class NavigationService<TViewModel>
        where TViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            if (_navigationStore.PrevViewModelList?.Count > 5)
                _navigationStore.PrevViewModelList.RemoveAt(0);

            _navigationStore.PrevViewModelList.Add(_navigationStore.CurrentViewModel.DeepClone());
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
