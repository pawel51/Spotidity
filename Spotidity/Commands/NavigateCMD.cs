using Spotidity.Services;
using Spotidity.Stores;
using Spotidity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotidity.Commands
{
    public class NavigateCMD<TViewModel> : BaseCommand
        where TViewModel : BaseViewModel
    {
        private readonly INavigattion<TViewModel> navigationService;

        public NavigateCMD(INavigattion<TViewModel> navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            navigationService.Navigate();
        }
    }
}
