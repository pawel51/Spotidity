using Spotidity.Models;
using Spotidity.Services;
using Spotidity.Stores;
using Spotidity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Spotidity.Commands
{
    public class LoginCMD : BaseCommand
    {
        private readonly LoginViewModel _viewModel;
        private readonly ParamNavigationService<Credentials, HomeViewModel> _navigationService;

        public LoginCMD(LoginViewModel viewModel, ParamNavigationService<Credentials, HomeViewModel> navigationService)
        {
            _viewModel = viewModel;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {

            MessageBox.Show($"Logging  in  DefaultUser");

            Credentials cred = new Credentials()
            {
                ValAppId = $"App Id: {_viewModel.AppId}",
                ValSecret = $"Secret: {_viewModel.AppSecret}"
            };


            _navigationService.Navigate(cred);
        }
    }
}
