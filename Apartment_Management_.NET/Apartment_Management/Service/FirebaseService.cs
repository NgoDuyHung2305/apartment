using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Database;
using Firebase.Database.Query;

namespace Apartment_Management.Service
{
	public class FirebaseService
	{
		private readonly FirebaseClient _firebaseClient;

		public FirebaseService()
		{
			_firebaseClient = new FirebaseClient("https://apartment-management-2h-default-rtdb.firebaseio.com/");
		}

		//Lấy Data từ Firebase

		public async Task<List<T>> GetDataAsync<T>(string path)
		{
			return (await _firebaseClient
				.Child(path)
				.OnceAsync<T>())
				.Select(item => item.Object)
				.ToList();
		}
        public async Task<List<T>> GetDataAsync<T>(string path, string idPropertyName) where T : new()
        {
            return (await _firebaseClient
                .Child(path)
                .OnceAsync<T>())
                .Select(item =>
                {
                    var obj = item.Object;

                    // Sử dụng Reflection để gán ID vào thuộc tính tương ứng
                    var idProperty = typeof(T).GetProperty(idPropertyName);
                    if (idProperty != null && idProperty.CanWrite)
                    {
                        idProperty.SetValue(obj, item.Key); // Gán giá trị Key vào thuộc tính
                    }

                    return obj;
                })
                .ToList();
        }


        //Thêm dữ liệu vào Firebase

        public async Task AddDataAsync<T>(string path, T data)
		{
			await _firebaseClient
				.Child(path)
				.PostAsync(data);
		}
        public async Task UpdateDataAsync<T>(string path, string key, T updatedData)
        {
            await _firebaseClient
                .Child(path)
                .Child(key)
                .PutAsync(updatedData);
        }
        public async Task UpdateFieldAsync<T>(string path, string key, string fieldName, T value)
        {
            await _firebaseClient
                .Child(path)
                .Child(key)
                .Child(fieldName)
                .PutAsync(value);
        }
        public async Task UpdateNumberAsync<T>(string path, string key, string fieldName, T value)
        {
            await _firebaseClient
                .Child(path)
                .Child(key)
                .Child(fieldName)
                .PutAsync(value);
        }

        public async Task<int> GetTodayOrderCountAsync()
		{
			var today = DateTime.Now.ToString("yyyy-MM-dd"); // Ngày hôm nay dưới định dạng yyyy-MM-dd
			var orders = await _firebaseClient
				.Child("Orders") // Đường dẫn đến Orders
				.OnceAsync<dynamic>(); // Lấy dữ liệu dưới dạng động (dynamic)

			// Lọc các Orders có create_at khớp với ngày hôm nay
			var todayOrders = orders
				.Where(order => order.Object.create_at == today) // Lọc theo ngày
				.ToList();

			return todayOrders.Count; // Trả về số lượng Orders hôm nay
		}

		public async Task<int> GetThisMonthNewDwellerCountAsync()
		{
			// Lấy tháng hiện tại (yyyy-MM)
			var thisMonth = DateTime.Now.ToString("yyyy-MM");

			// Lấy dữ liệu từ Firebase
			var dwellers = await _firebaseClient
				.Child("Dwellers") // Đường dẫn đến "Dwellers" trên Firebase
				.OnceAsync<dynamic>(); // Lấy dữ liệu dưới dạng dynamic

			// Lọc những dwellers có create_at trong tháng hiện tại
			var thisMonthDwellers = dwellers
				.Where(dweller =>
				{
					var createAt = dweller.Object.create_at?.ToString(); // Lấy giá trị create_at
					return createAt != null && createAt.StartsWith(thisMonth); // Kiểm tra tháng hiện tại
				})
				.ToList();

			return thisMonthDwellers.Count; // Trả về số lượng dwellers trong tháng
		}

		public async Task<double> GetRateFilledRoomAsync()
		{
			// Lấy tất cả các phòng từ Firebase
			var rooms = await _firebaseClient
				.Child("Rooms") // Đường dẫn đến "Rooms"
				.OnceAsync<dynamic>();

			// Đếm tổng số phòng
			int totalRooms = rooms.Count;

			// Đếm số phòng đã được lấp đầy
			int filledRooms = rooms
				.Count(room => room.Object.room_status?.ToString() == "Rented");

			// Tính tỷ lệ phòng lấp đầy (phần trăm)
			double rateFilledRoom = (double)filledRooms / totalRooms * 100;

			return Math.Round(rateFilledRoom, 2); // Làm tròn 2 chữ số thập phân
		}

		public async Task<int> GetTodaysSolvedOrderAsync()
		{
			var today = DateTime.Now.ToString("yyyy-MM-dd");

			// Lấy tất cả các orders từ Firebase
			var orders = await _firebaseClient
				.Child("Orders") // Đường dẫn đến "Orders"
				.OnceAsync<dynamic>();

			// Lọc những orders có trạng thái "Solved" và ngày tạo là hôm nay
			var todaysSolvedOrders = orders
				.Where(order =>
				{
					var createAt = order.Object.create_at?.ToString();
					var status = order.Object.ord_status?.ToString();
					return createAt == today && status == "Solved";
				})
				.ToList();

			return todaysSolvedOrders.Count; // Trả về số lượng orders được giải quyết
		}

		public async Task<int> GetThisMonthContractAsync()
		{
			// Lấy tháng hiện tại (định dạng yyyy-MM)
			var thisMonth = DateTime.Now.ToString("yyyy-MM");

			// Lấy tất cả các hợp đồng từ Firebase
			var contracts = await _firebaseClient
				.Child("Contracts") // Đường dẫn đến "Contracts"
				.OnceAsync<dynamic>();

			// Lọc những hợp đồng có ngày tạo thuộc tháng hiện tại
			var thisMonthContracts = contracts
				.Where(contract =>
				{
					var createAt = contract.Object.create_at?.ToString(); // Ngày tạo
					return createAt != null && createAt.StartsWith(thisMonth);
				})
				.ToList();

			return thisMonthContracts.Count; // Trả về số lượng hợp đồng trong tháng
		}
        public async Task DeleteDataAsync(string path, string key)
        {
            try
            {
                await _firebaseClient
                    .Child(path)
                    .Child(key)
                    .DeleteAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
