using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Apartment_Management.Model
{
	public class Room
	{
		public string RoomID { get; set; }

		[JsonProperty("id_type")]
		public string TypeRoomID { get; set; }

        [JsonProperty("room_name")]
        public string RoomName { get; set; }

        [JsonProperty("id_block")]
		public string BlockID { get; set; }

		[JsonProperty("id_host")]
		public string HostID { get; set; }

		[JsonProperty("member_num")]
		public int Member_number { get; set; }

		[JsonProperty("room_status")]
		public string Status { get; set; }

		[JsonProperty("rent_price")]
		public decimal Price { get; set; }

		[JsonProperty("create_at")]
		public DateTime Create_At { get; set; }

		[JsonProperty("create_by")]
		public string Create_By { get; set; }

		public Room()
		{
		}

		public Room(string roomID, string typeRoomID, string blockID, string hostID, int member_number, string status, decimal price, DateTime create_At, string create_By)
		{
			RoomID = roomID;
			TypeRoomID = typeRoomID;
			BlockID = blockID;
			HostID = hostID;
			Member_number = member_number;
			Status = status;
			Price = price;
			Create_At = create_At;
			Create_By = create_By;
		}
        public Room(Room room)
        {
            RoomID = room.RoomID;
            TypeRoomID = room.TypeRoomID;
			RoomName = room.RoomName;
            BlockID = room.BlockID;
            HostID = room.HostID;
            Member_number = room.Member_number;
            Status = room.Status;
            Price = room.Price;
            Create_At = room.Create_At;
            Create_By = room.Create_By;
        }
    }
}
