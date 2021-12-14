using Spotidity.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotidity.Builder.SpotiBuilders
{
    public abstract class BaseSpotiBuilder
    {
        public abstract List<ISpotiModel> getResult();
        public abstract void createProps();
       
    }
}
