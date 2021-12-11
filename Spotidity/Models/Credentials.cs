using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotidity.Models
{
    public class Credentials : BaseModel
    {
        private string valAppId;

        public string ValAppId
        {
            get { return valAppId; }
            set { valAppId = value; }
        }

        private string valSecret;
        

        public Credentials(string v1, string v2)
        {
            this.ValAppId = v1;
            this.ValSecret = v2;
        }

        public Credentials()
        {
        }

        public string ValSecret
        {
            get { return valSecret; }
            set { valSecret = value; }
        }

    }
}
