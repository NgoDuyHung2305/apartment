using Apartment_Management.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartment_Management.Service
{
    public class UserAccount
    {
        private static UserAccount _instance;
        public static UserAccount Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UserAccount();
                return _instance;
            }
        }
        public User User { get; set; }
        private UserAccount() { }
    }

    
}
