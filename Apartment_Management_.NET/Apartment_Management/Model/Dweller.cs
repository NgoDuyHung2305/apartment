using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Apartment_Management.Model
{
	public class Dweller
	{


		public string DwellerID { get; set; }


		[JsonProperty("dweller_name")]
		public string DwellerName { get; set; }

		[JsonProperty("dweller_phone")]
		public string DwellerPhone { get; set; }

		[JsonProperty("dweller_email")]
		public string DwellerEmail { get; set; }

		[JsonProperty("dweller_birthday")]
		public DateTime DwellerBirth { get; set; }

        [JsonProperty("dweller_hometown")]
        public String DwellerHomeTown { get; set; }

		[JsonProperty("dweller_sex")]
		public string DwellerSex { get; set; }

		[JsonProperty("dweller_password")]
		private string DwellerPassword { get; set; }

		[JsonProperty("dweller_date_start")]
		public DateTime Dweller_Date_Start { get; set; }

		[JsonProperty("create_at")]
		public DateTime Create_At { get; set; }

		[JsonProperty("create_by")]
		public string Create_By { get; set; }

		[JsonProperty("id_room")]
		public string RoomID { get; set; }

		[JsonProperty("dweller_login")]
		public string DwellerLogin { get; set; }

		public Dweller()
		{
		}
		public Dweller(string dwellerID, string dwellerName, string dwellerPhone, string dwellerEmail, DateTime dwellerBirth, string dwellerSex, string dwellerPassword, DateTime dweller_Date_Start, DateTime create_At, string create_By, string roomID, string dwellerLogin)
		{
			DwellerID = dwellerID;
			DwellerName = dwellerName;
			DwellerPhone = dwellerPhone;
			DwellerEmail = dwellerEmail;
			DwellerBirth = dwellerBirth;
			DwellerSex = dwellerSex;
			DwellerPassword = dwellerPassword;
			Dweller_Date_Start = dweller_Date_Start;
			Create_At = create_At;
			Create_By = create_By;
			RoomID = roomID;
			DwellerLogin = dwellerLogin;
		}
		public Dweller(Dweller dweller)
		{
			DwellerID = dweller.DwellerID;
			DwellerName = dweller.DwellerName;
			DwellerPhone = dweller.DwellerPhone;
			DwellerEmail = dweller.DwellerEmail;
			DwellerBirth = dweller.DwellerBirth;
			DwellerSex = dweller.DwellerSex;
			DwellerPassword = dweller.DwellerPassword;
			Dweller_Date_Start = dweller.Dweller_Date_Start;
            Create_At = dweller.Create_At;
            Create_By = dweller.Create_By;
            RoomID = dweller.RoomID;
            DwellerLogin = dweller.DwellerLogin;
        }
    }
	
}
