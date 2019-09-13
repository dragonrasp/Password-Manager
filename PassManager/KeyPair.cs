using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassManager
{
    public class KeyData
    {
        public string Name
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }

        public string Login
        {
            get;
            set;
        }

        public string URL
        {
            get;
            set;
        }

        public KeyData()
        {
            Name = "";
            Password = "";
            Login = "";
            URL = "";
        }

        public KeyData(string n, string p, string l, string u)
        {
            Name = n;
            Password = p;
            Login = l;
            URL = u;
        }

        public bool IsValid
        {
            get { return Name.Length > 0 && Name != null && Password.Length > 0 && Password != null; }
        }
        
    }
}
