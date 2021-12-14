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
    public class NavigateBackCMD : BaseCommand
    {
        private readonly BackNavigationService navigationService;

        public NavigateBackCMD(BackNavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            navigationService.Navigate();
        }
    }
}