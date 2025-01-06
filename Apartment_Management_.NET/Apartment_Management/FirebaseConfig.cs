using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System.IO;
using System.Windows;


namespace Apartment_Management
{
	public static class FirebaseConfig
	{
		//Khởi tạo Firebase
		public static void InitFirebase()
		{
			try
			{


				var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "apartment-management-2h-firebase-adminsdk-17i68-032a678aec.json");
				//Chỉ cần khởi tạo khi chưa khởi tạo
				if (File.Exists(path))
				{
					if (FirebaseApp.DefaultInstance == null)
					{
						FirebaseApp.Create(new AppOptions()
						{
							Credential = GoogleCredential.FromFile(path)
						});
					}
				}
				else
				{
					MessageBox.Show("Firebase Config File Not Found");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
