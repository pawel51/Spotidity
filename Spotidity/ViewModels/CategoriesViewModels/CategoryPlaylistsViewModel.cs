using Spotidity.Commands;
using Spotidity.Models;
using Spotidity.Services;
using Spotidity.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Spotidity.ViewModels
{
    public class CategoryPlaylistsViewModel : BaseViewModel
    {


        private NotifyTaskCompletion<ObservableCollection<SpotiPlaylist>> _CategoryPlaylists;
        public NotifyTaskCompletion<ObservableCollection<SpotiPlaylist>> CategoryPlaylists
        {
            get { return _CategoryPlaylists; }
            set { _CategoryPlaylists = value; OnPropertyChanged(nameof(CategoryPlaylists)); }
        }

        private SpotiCategory _SpotiCategory;

        public SpotiCategory SpotiCategory
        {
            get { return _SpotiCategory; }
            set { _SpotiCategory = value; OnPropertyChanged(nameof(SpotiCategory)); }
        }


        public CategoryPlaylistsViewModel(SpotiCategory spotiCategory)//ApiService service
        {
            SpotiCategory = spotiCategory;
            NavigationStore navigationStore = NavigationStore.GetInstace();
            CategoryPlaylists = new NotifyTaskCompletion<ObservableCollection<SpotiPlaylist>>
                (
                    ApiService.GetInstace().GetCategoryPlaylists(spotiCategory.Id)
                );

          
            
        }

        public override BaseViewModel DeepClone()
        {
            return new CategoryPlaylistsViewModel(this.SpotiCategory);
        }
    }
}
