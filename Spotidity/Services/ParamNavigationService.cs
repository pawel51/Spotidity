using Spotidity.Stores;
using Spotidity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotidity.Services
{
    public class ParamNavigationService<TParameter, TViewModel>
        where TViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;

        private readonly Func<TParameter, TViewModel> _createViewModel;

        public ParamNavigationService(NavigationStore navigationStore, Func<TParameter, TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate(TParameter parameter)
        {
            _navigationStore.CurrentViewModel = _createViewModel(parameter);
        }
    }
}
