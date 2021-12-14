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
    public class HomeViewModel : BaseViewModel
    {
        private readonly Credentials _credentials;

        public string ValAppId => _credentials.ValAppId;

        public string ValSecret => _credentials.ValSecret;

        public HomeViewModel(Credentials creds)
        {
            _credentials = creds;
        }

        public override BaseViewModel DeepClone()
        {
            return (HomeViewModel)this.MemberwiseClone();
        }
    }
}
