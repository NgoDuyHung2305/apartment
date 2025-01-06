using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Apartment_Management.Model
{
	public class Contract
	{
	
		public string Contract_Id { get; set; }


		[JsonProperty("id_dweller")]
		public string Dweller_Id { get; set; }


		[JsonProperty("id_room")]
		public string Room_Id { get; set; }


		[JsonProperty("contract_description")]
		public string Contract_Description { get; set; }


		[JsonProperty("contract_price")]
		public decimal Contract_Price { get; set; }


		[JsonProperty("contract_start")]
		public DateTime Contract_StartDate { get; set; }


		[JsonProperty("contract_end")]
		public DateTime Contract_EndDate { get; set; }



		[JsonProperty("create_by")]
		public string Create_By { get; set; }


		[JsonProperty("create_at")]
		public DateTime Create_At { get; set; }

		public string Dweller_Name { get; set; }


		public Contract()
		{

		}

		public Contract(string Contract_Id, string Dweller_Id, string Room_Id, string Contract_Description, decimal Contract_Price, DateTime Contract_StartDate, DateTime Contract_EndDate, string Create_By, DateTime create_At, string dweller_Name)
		{
			this.Contract_Id = Contract_Id;
			this.Dweller_Id = Dweller_Id;
			this.Room_Id = Room_Id;
			this.Contract_Description = Contract_Description;
			this.Contract_Price = Contract_Price;
			this.Contract_StartDate = Contract_StartDate;
			this.Contract_EndDate = Contract_EndDate;
			this.Create_By = Create_By;
			this.Create_At = create_At;
			this.Dweller_Name = dweller_Name;
		}
	}
}
