using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.GlobalVariable
{
    public class LoginInfo
    {
        public string username { get; set; }
        public string authority { get; set; }
        public LoginInfo(string username, string authority)
        {
            this.username = username;
            this.authority = authority;
        }
        public LoginInfo()
        {
        }
    }
}
