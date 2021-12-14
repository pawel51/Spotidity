using Spotidity.Stores;
using Spotidity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotidity.Services
{
    public class ForwardNavigationService
    {
        private readonly NavigationStore _navigationStore;

        public ForwardNavigationService(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public void Navigate()
        {
            // operacje przed 
            if (_navigationStore.NextViewModelList == null || _navigationStore.NextViewModelList.Count == 0) return;

            var temp = _navigationStore.CurrentViewModel.DeepClone();

            // moment zmiany widoku
            _navigationStore.CurrentViewModel = _navigationStore.NextViewModelList.Last();
            
            // operacje po
            _navigationStore.NextViewModelList.RemoveAt(_navigationStore.NextViewModelList.Count - 1);
            _navigationStore.PrevViewModelList.Add(temp);
        }
    }
}