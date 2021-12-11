using Spotidity.Services;
using Spotidity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotidity.Commands
{
    public class NavigateParamCMD<TParameter, TViewModel> : BaseCommand
        where TViewModel : BaseViewModel
    {
        private readonly ParamNavigationService<TParameter, TViewModel> navigationService;

        public NavigateParamCMD(ParamNavigationService<TParameter, TViewModel> navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            navigationService.Navigate((TParameter)parameter);
        }
    }
}
