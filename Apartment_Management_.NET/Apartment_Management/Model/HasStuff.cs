using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartment_Management.Model
{
    public class HasStuff
    {
        public string HasStuffID { get; set; }
        [JsonProperty("idRoom")]

        public string RoomID { get; set; }
        [JsonProperty("idStuff")]

        public string StuffID { get; set; }
        [JsonProperty("number")]

        public int Number { get; set; }
        public DateTime CreateAt { get; set; }
        public String CreateBy { get;set; }
    }
}
