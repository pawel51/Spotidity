using Spotidity.Stores;
using Spotidity.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotidity.Services
{
    class HistoryProxy<TViewModel> : INavigattion<TViewModel>
        where TViewModel : BaseViewModel
    {


        INavigattion<TViewModel> realService;

        public HistoryProxy(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            this.realService = new NavigationService<TViewModel>(navigationStore, createViewModel);
        }

        public void Navigate()
        {
            _ = LogToFile();
            this.realService.Navigate();
        }

        private async Task LogToFile()
        {
            DateTime localDate = DateTime.Now;

            using (StreamWriter file = new StreamWriter("History.txt", append: true))
            {
                await file.WriteLineAsync(localDate.ToString() + " | NavigationService/Navigate()");
            }
        }
    }
}
