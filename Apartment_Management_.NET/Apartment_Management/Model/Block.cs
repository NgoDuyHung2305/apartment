using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Apartment_Management.Model
{
	public class Block
	{

		public string BlockID { get; set; }

        [JsonProperty("block_name")]
        public string Block_Name { get; set; }

        [JsonProperty("block_floor")]
        public int No_Floors { get; set; }

        [JsonProperty("block_room")]
        public int No_Rooms { get; set; }

        [JsonProperty("block_room_available")]
        public int No_Rooms_Available { get; set; }

        [JsonProperty("create_at")]
        public DateTime Date_Created { get; set; }
		public DateTime Date_Updated { get; set; }
		public string Created_By { get; set; }
		public string Updated_By { get; set; }
		public DateTime Delete_At { get; set; }

		public Block()
		{
		}
		public Block(string blockID, string block_Name, int no_Floors, int no_Rooms, int no_Rooms_Available, DateTime date_Created, DateTime date_Updated, string created_By, string updated_By, DateTime delete_At)
		{
			BlockID = blockID;
			Block_Name = block_Name;
			No_Floors = no_Floors;
			No_Rooms = no_Rooms;
			No_Rooms_Available = no_Rooms_Available;
			Date_Created = date_Created;
			Date_Updated = date_Updated;
			Created_By = created_By;
			Updated_By = updated_By;
			Delete_At = delete_At;
		}
        public Block(Block block)
        {
            BlockID = block.BlockID;
            Block_Name = block.Block_Name;
            No_Floors = block.No_Floors;
            No_Rooms = block.No_Rooms;
            No_Rooms_Available = block.No_Rooms_Available;
            Date_Created = block.Date_Created;
            Date_Updated = block.Date_Updated;
            Created_By = block.Created_By;
            Updated_By = block.Updated_By;
            Delete_At = block.Delete_At;
        }
    }
}