using Spotidity.Commands;
using Spotidity.Models;
using Spotidity.Services;
using Spotidity.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Spotidity.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        readonly ApiService _apiService;
        private string appId;

        public string AppId
        {
            get { return appId; }
            set { appId = value; OnPropertyChanged(nameof(AppId)); }
        }

        private string appSecret;

        public string AppSecret
        {
            get { return appSecret; }
            set { appSecret = value; OnPropertyChanged(nameof(AppSecret)); }
        }



        public ICommand LoginCMD { get; }

        public LoginViewModel()
        {
            NavigationStore navigationStore = NavigationStore.GetInstace();

            _apiService = ApiService.GetInstace();
            _ =_apiService.AuthAsync();

            ParamNavigationService<Credentials, HomeViewModel> navigationService = new ParamNavigationService<Credentials, HomeViewModel>
            (
                navigationStore,
                (parameter) => new HomeViewModel(parameter)
            );

            LoginCMD = new LoginCMD(this, navigationService);
        }

    }
}
