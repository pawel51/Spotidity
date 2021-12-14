using Spotidity.Stores;
using Spotidity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotidity.Services
{
    public class BackNavigationService
    {
        private readonly NavigationStore _navigationStore;

        public BackNavigationService(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public void Navigate()
        {
            if (_navigationStore.PrevViewModelList==null || _navigationStore.PrevViewModelList.Count == 0) return;

            var temp = _navigationStore.CurrentViewModel.DeepClone();
            
            _navigationStore.CurrentViewModel = _navigationStore.PrevViewModelList.Last();
            _navigationStore.PrevViewModelList.RemoveAt(_navigationStore.PrevViewModelList.Count-1);
            _navigationStore.NextViewModelList.Add(temp);
        }
    }
}