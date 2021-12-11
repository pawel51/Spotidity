using Spotidity.Commands;
using Spotidity.Models;
using Spotidity.Services;
using Spotidity.Stores;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Spotidity.ViewModels
{
    public class CategoryTableViewModel : BaseViewModel
    {
        #region Properties
        

        private NotifyTaskCompletion<ObservableCollection<SpotiCategory>> _Categories;
        public NotifyTaskCompletion<ObservableCollection<SpotiCategory>> Categories
        {
            get { return _Categories; }
            set { _Categories = value; OnPropertyChanged(nameof(Categories)); }
        }

        private RelayCommand _SearchCMD;
        public RelayCommand SearchCMD
        {
            get
            {
                if (_SearchCMD == null)
                    _SearchCMD = new RelayCommand(param => this.Search(), param => this.CanSearch());
                return _SearchCMD;
            }
            set { _SearchCMD = value; }
        }


        public ICommand ReturnHome { get; }




        #endregion

        #region Constructors

        public CategoryTableViewModel()
        {
            NavigationStore navigationStore = NavigationStore.GetInstace();

            Categories = new NotifyTaskCompletion<ObservableCollection<SpotiCategory>>
                (
                    ApiService.GetInstace().GetCategories()
                );



            ReturnHome = new NavigateCMD<HomeViewModel>(new NavigationService<HomeViewModel>
                (
                navigationStore, () => new HomeViewModel(new Credentials("", ""))
                ));
        }
        #endregion



        #region Methods
        private bool CanSearch()
        {
            return true;
        }

        private void Search()
        {
            throw new NotImplementedException();
        }
        #endregion




    }
}
