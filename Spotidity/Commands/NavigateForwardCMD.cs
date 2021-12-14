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
    public class NavigateForwardCMD : BaseCommand
    {
        private readonly ForwardNavigationService navigationService;

        public NavigateForwardCMD(ForwardNavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            navigationService.Navigate();
        }
    }
}
