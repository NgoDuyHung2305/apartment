using Apartment_Management.Model;
using Apartment_Management.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apartment_Management.ViewModel
{
	internal class Account_View_Model:Base_View_Model
	{
		public User CurrentUser { get; set; }
		public Account_View_Model()
		{
            CurrentUser = new User(UserAccount.Instance.User);
		}
	}
}
