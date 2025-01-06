using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartment_Management.Model
{
	public class TypeRoom
	{
		private string TypeRoomID { get; set; }
		public string TypeRoom_Name { get; set; }
		public int Restroom_Number { get; set; }
		public int Bedroom_Number { get; set; }
		public DateTime Create_At { get; set; }
		public DateTime Update_At { get; set; }
		public string Create_By { get; set; }
		public string Update_By { get; set; }
		public DateTime Delete_At { get; set; }

		public TypeRoom()
		{
		}

		public TypeRoom(string typeRoomID, string typeRoom_Name, int restroom_Number, int bedroom_Number, DateTime create_At, DateTime update_At, string create_By, string update_By, DateTime delete_At)
		{
			TypeRoomID = typeRoomID;
			TypeRoom_Name = typeRoom_Name;
			Restroom_Number = restroom_Number;
			Bedroom_Number = bedroom_Number;
			Create_At = create_At;
			Update_At = update_At;
			Create_By = create_By;
			Update_By = update_By;
			Delete_At = delete_At;
		}
	}
}
