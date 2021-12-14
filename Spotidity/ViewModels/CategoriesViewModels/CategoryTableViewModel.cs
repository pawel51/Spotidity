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


        #endregion

        #region Constructors

        public CategoryTableViewModel()
        {

            Categories = new NotifyTaskCompletion<ObservableCollection<SpotiCategory>>
                (
                    ApiService.GetInstace().GetCategories()
                );



        }

        public override BaseViewModel DeepClone()
        {
            return new CategoryTableViewModel();
        }
        #endregion




    }
}
