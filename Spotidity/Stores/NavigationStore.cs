using Spotidity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotidity.Stores
{
    public class NavigationStore
    {
        public event Action CurrentViewModelChanged;

        private List<BaseViewModel> _PrevViewModelList = new List<BaseViewModel>();

        public List<BaseViewModel> PrevViewModelList
        {
            get { return _PrevViewModelList; }
            set { _PrevViewModelList = value; }
        }

        private List<BaseViewModel> _NextViewModelList = new List<BaseViewModel>();

        public List<BaseViewModel> NextViewModelList
        {
            get { return _NextViewModelList; }
            set { _NextViewModelList = value; }
        }

        private BaseViewModel _CurrentViewModel;
        public BaseViewModel CurrentViewModel 
        {
            get => _CurrentViewModel;
            set
            {
                _CurrentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        
        #region Singleton
        private static NavigationStore _instance;
        private static readonly object _lock = new object();
        private NavigationStore() { }

        public static NavigationStore GetInstace()
        {
            // pierwszy dostęp do serwisu
            if (_instance == null)
            {
                // zablokowanie dostępu przez pierwszy wątek
                lock (_lock)
                {
                    // drugi wątek nawet jeżeli będzie czekał na locku, to nie utworzy instacji 
                    if (_instance == null)
                    {
                        // utworzenie serwisu po raz pierwszy
                        _instance = new NavigationStore();
                    }
                }
            }
            return _instance;
        }
        #endregion
    }
}
