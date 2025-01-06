using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartment_Management.Model
{
	public class Stuff
	{
		public string StuffID { get; set; }

        [JsonProperty("stf_name")]

        public string Stuff_Name { get; set; }
		public int Stuff_Number { get; set; }
		private string RoomID { get; set; }
		public DateTime Create_At { get; set; }
		public DateTime Update_At { get; set; }
		public string Create_By { get; set; }
		public string Update_By { get; set; }
		public DateTime Delete_At { get; set; }

		public Stuff()
		{
		}

		public Stuff(string stuffID, string stuff_Name, int stuff_Number, string roomID, DateTime create_At, DateTime update_At, string create_By, string update_By, DateTime delete_At)
		{
			StuffID = stuffID;
			Stuff_Name = stuff_Name;
			Stuff_Number = stuff_Number;
			RoomID = roomID;
			Create_At = create_At;
			Update_At = update_At;
			Create_By = create_By;
			Update_By = update_By;
			Delete_At = delete_At;
		}
	}
}
