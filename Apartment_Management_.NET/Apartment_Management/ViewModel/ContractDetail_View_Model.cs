using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apartment_Management.Model;
namespace Apartment_Management.ViewModel
{
	public class ContractDetail_View_Model
	{

		public string dweller_name { get; set; }
		public string room_name { get; set; }
		public string start_date { get; set; }
		public string end_date { get; set; }
		public string price { get; set; }
		public string description { get; set; }

		public ContractDetail_View_Model(Contract c )
		{
			dweller_name = c.Dweller_Name;
			room_name = c.Room_Id;
			start_date = c.Contract_StartDate.ToString();
			end_date = c.Contract_EndDate.ToString();
			price = c.Contract_Price.ToString();
			description = c.Contract_Description;
		}
	}
}
