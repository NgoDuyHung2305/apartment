using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartment_Management.Model
{
	public class Payment
	{
		private string PaymentID { get; set; }
		public bool Status { get; set; }
		public DateTime Create_At { get; set; }
		public DateTime Update_At { get; set; }
		public string Create_By { get; set; }
		public string Update_By { get; set; }
		public DateTime Delete_At { get; set; }
		public Payment() { }	

		public Payment(string paymentID, bool status, DateTime create_At, DateTime update_At, string create_By, string update_By, DateTime delete_At)
		{
			PaymentID = paymentID;
			Status = status;
			Create_At = create_At;
			Update_At = update_At;
			Create_By = create_By;
			Update_By = update_By;
			Delete_At = delete_At;
		}
	}
}
