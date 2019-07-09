using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassManager
{
    public class KeyPair
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

        public KeyPair()
        {
            Name = "";
            Password = "";
        }

        public KeyPair(string n, string p)
        {
            Name = n;
            Password = p;
        }

        public bool IsValid
        {
            get { return Name.Length > 0 && Name != null && Password.Length > 0 && Password != null; }
        }
        
    }
}
