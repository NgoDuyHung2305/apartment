using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Apartment_Management.Model
{
	public class Receipt
	{
		public string ReceiptID { get; set; }

		[JsonProperty("id_order")]
		public string OrderID { get; set; }

		[JsonProperty("amount")]
		public int amount { get; set; }


		[JsonProperty("id_room")]
		public string RoomID { get; set; }

		[JsonProperty("bill_status")]
		public string Status { get; set; }

		[JsonProperty("id_payment")]
		private string PaymentID { get; set; }

		[JsonProperty("bill_type")]
		public string Type { get; set; }

		[JsonProperty("bill_description")]
		public string ReceiptDescription { get; set; }

		[JsonProperty("create_at")]
		public DateTime Create_At { get; set; }


		[JsonProperty("create_by")]
		public string Create_By { get; set; }


		public Receipt()
		{
		}

		public Receipt(string receiptID, string orderID, string roomID, string status, string paymentID, string type, string receiptDescription, DateTime create_At, string create_By, int amount)
		{
			ReceiptID = receiptID;
			OrderID = orderID;
			RoomID = roomID;
			Status = status;
			PaymentID = paymentID;
			Type = type;
			ReceiptDescription = receiptDescription;
			Create_At = create_At;
			Create_By = create_By;
			this.amount = amount;	
		}
	}
}
