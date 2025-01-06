using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Apartment_Management.Model
{
	public class User
	{
		private string UserID { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set; }

        [JsonProperty("user_birthday")]
        public DateTime UserBirth { get; set; }

        [JsonProperty("user_sex")]
        public string UserSex { get; set; }

        [JsonProperty("user_date_start")]
        public DateTime Date_Start { get; set; }

        [JsonProperty("user_phone_num")]
        public string UserPhone { get; set; }

        [JsonProperty("user_mail")]
        public string UserMail { get; set; }

        [JsonProperty("user_password")]
        public string UserPassword { get; set; }

        [JsonProperty("create_at")]
        public DateTime Create_At { get; set; }
		public DateTime Update_At { get; set; }
		public string Create_By { get; set; }
		public string Update_By { get; set; }
		public DateTime Delete_At { get; set; }

		public User()
		{
		}

		public User(string userID, string userName, DateTime userBirth, string userSex, DateTime date_Start, string userPhone, string userMail, string userPassword, DateTime create_At, DateTime update_At, string create_By, string update_By, DateTime delete_At)
		{
			UserID = userID;
			UserName = userName;
			UserBirth = userBirth;
			UserSex = userSex;
			Date_Start = date_Start;
			UserPhone = userPhone;
            UserMail = userMail;
            UserPassword = userPassword;
			Create_At = create_At;
			Update_At = update_At;
			Create_By = create_By;
			Update_By = update_By;
			Delete_At = delete_At;
		}
		public User(User user)
		{
			this.UserID = user.UserID;
			this.UserName = user.UserName;
			this.UserBirth = user.UserBirth;
			this.UserSex = user.UserSex;
			this.Date_Start = user.Date_Start;
			this.UserPhone= user.UserPhone;
			this.UserMail = user.UserMail;
			this.UserPassword = user.UserPassword;
			this.Create_At = user.Create_At;
			this.Update_At = user.Update_At;
			this.Create_By = user.Create_By;
			this.Update_By = user.Update_By;
			this.Delete_At = user.Delete_At;
		}
    }
}
