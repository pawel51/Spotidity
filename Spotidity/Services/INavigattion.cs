using Spotidity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotidity.Services
{
    public interface INavigattion<TViewModel>
        where TViewModel : BaseViewModel
    {
        void Navigate();
    }
}
