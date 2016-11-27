using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace WindowsFormsApplication1.GlobalVariable
{
    class GetIP
    {
        public string IP { get; set; }
        public GetIP()
        {
            IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipa = ipe.AddressList[0];
            this.IP = ipa.ToString();
        }
    }
}
