using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Apartment_Management.Model
{
	public class Order
	{
		public string OrderID { get; set; }

        [JsonProperty("idDweller")]
        public string DwellerID { get; set; }

        [JsonProperty("idRoom")]
        public string RoomID { get; set; }

        [JsonProperty("ord_description")]
        public string OrderDescription { get; set; }

        [JsonProperty("ord_status")]
        public string OrderStatus { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
        public string EmployeeID { get; set; }

        [JsonProperty("create_at")]

        public DateTime Create_At { get; set; }
		public DateTime Update_At { get; set; }
		public string Create_By { get; set; }
		public string Update_By { get; set; }
		public DateTime Delete_At { get; set; }

		public Order() { }

		public Order(string orderID, string dwellerID, string roomID, string orderDescription, string orderStatus, string employeeID, string type, DateTime create_At, DateTime update_At, string create_By, string update_By, DateTime delete_At)
		{
			OrderID = orderID;
			DwellerID = dwellerID;
			RoomID = roomID;
			OrderDescription = orderDescription;
			OrderStatus = orderStatus;
			EmployeeID = employeeID;
			Type = type;
			Create_At = create_At;
			Update_At = update_At;
			Create_By = create_By;
			Update_By = update_By;
			Delete_At = delete_At;
		}
	}
}
