using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Apartment_Management.Helper;
using Apartment_Management.Model;
using Firebase.Database;
using Newtonsoft.Json;
using System.Windows;
using Firebase.Database.Query;

namespace Apartment_Management.ViewModel
{
	public class AddContract_View_Model:INotifyPropertyChanged
	{
		private const string firebaseUrl = "https://apartment-management-2h-default-rtdb.firebaseio.com/";
		private readonly FirebaseClient _firebaseClient;

		private string _price;
		private string _selectedRoomId;
		private ObservableCollection<Dweller> _Dwellers;
		private ObservableCollection<Room> _Rooms;
		private ObservableCollection<string> idRooms;
		private ObservableCollection<string> name_idDwellers;
		private ObservableCollection<string> nameDwellers;
		private string _dwellerId;
		private string _description;
		private DateTime? _startDate;
		private DateTime? _endDate;

		public event PropertyChangedEventHandler PropertyChanged;




		public ObservableCollection<Dweller> Dwellers
		{
			get => _Dwellers;
			set
			{
				_Dwellers = value;
				OnPropertyChanged();
			}
		}

		public ObservableCollection<string> NameDwellers
		{
			get => nameDwellers;
			set
			{
				nameDwellers = value;
				OnPropertyChanged();
			}
		}

		public ObservableCollection<string> Name_IdDwellers
		{
			get => name_idDwellers;
			set
			{
				name_idDwellers = value;
				OnPropertyChanged();
			}
		}
		public string Price
		{
			get => _price;
			set
			{
				_price = value;
				OnPropertyChanged();
			}
		}

		public string SelectedRoomId
		{
			get => _selectedRoomId;
			set
			{
				_selectedRoomId = value;
				OnPropertyChanged();
			}
		}


		public ObservableCollection<Room> Rooms
		{
			get => _Rooms;
			set
			{
				_Rooms = value;
				OnPropertyChanged();
			}
		}

		public ObservableCollection<string> IdRooms
		{
			get => idRooms;
			set
			{
				idRooms = value;
				OnPropertyChanged();
			}
		}

		public string DwellerId
		{
			get => _dwellerId;
			set
			{
				_dwellerId = value;
				OnPropertyChanged();
			}
		}

		public string Description
		{
			get => _description;
			set
			{
				_description = value;
				OnPropertyChanged();
			}
		}

		public DateTime? StartDate
		{
			get => _startDate;
			set
			{
				_startDate = value;
				OnPropertyChanged();
			}
		}


		public DateTime? EndDate
		{
			get => _endDate;
			set
			{
				_endDate = value;
				OnPropertyChanged();
			}
		}

		public ICommand SaveCommand { get; }
		public ICommand CancelCommand { get; }

		public AddContract_View_Model()
		{
			_firebaseClient = new FirebaseClient(firebaseUrl);
			Rooms = new ObservableCollection<Room>();
			IdRooms = new ObservableCollection<string>();
			Name_IdDwellers = new ObservableCollection<string>();
			NameDwellers = new ObservableCollection<string>();
			Dwellers = new ObservableCollection<Dweller>();
			LoadAvailableRooms();
			LoadRentedDwellers();
			SaveCommand = new RelayCommand(OnSave);
			CancelCommand = new RelayCommand(OnCancel);
		}

		private async void LoadAvailableRooms()
		{
			try
			{
				var firebaseRooms = await _firebaseClient
					.Child("Rooms")
					.OnceAsync<object>();
				Rooms.Clear();
				foreach (var item in firebaseRooms)
				{
					var roomJson = JsonConvert.SerializeObject(item.Object);
					var room = JsonConvert.DeserializeObject<Room>(roomJson);

					if (room != null)
					{
						room.RoomID = item.Key;
						Rooms.Add(room);
					}
					else
						MessageBox.Show("Room is null");
				}

				foreach (var room in Rooms)
				{
					if (room.Status == "Rented")
					{
						IdRooms.Add(room.RoomID);
					}
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}

		}


		private async void LoadRentedDwellers()
		{
			try
			{
				var firebaseRooms = await _firebaseClient
					.Child("Dwellers")
					.OnceAsync<object>();
				Dwellers.Clear();
				foreach (var item in firebaseRooms)
				{
					var dwellerJson = JsonConvert.SerializeObject(item.Object);
					var dweller = JsonConvert.DeserializeObject<Dweller>(dwellerJson);
					if (dweller != null)
					{
						dweller.DwellerID = item.Key;
						Dwellers.Add(dweller);
					}
					else
						MessageBox.Show("Dweller is null");
				}

				foreach (var dweller in Dwellers)
				{
					if (dweller.RoomID != null)
					{
						Name_IdDwellers.Add(dweller.DwellerName + " - " + dweller.DwellerID+" - "+dweller.RoomID);
						NameDwellers.Add(dweller.DwellerID);
					}
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}

		}


		private void OnCancel(object obj)

		{
			if (obj is Window window)
			{
				window.DialogResult = false;
				window.Close();
			}
			else 
			{
				MessageBox.Show("false");
			}
		}
		private async void OnSave(object obj)
		{

			if (!string.IsNullOrEmpty(DwellerId) &&
				   !string.IsNullOrEmpty(SelectedRoomId) &&
				   !string.IsNullOrEmpty(Description) &&
				   !string.IsNullOrEmpty(Price) &&
				   StartDate.HasValue &&
				   EndDate.HasValue)
			{
				var c = new Contract
				{
					Dweller_Id = DwellerId,
					Contract_Description = Description,
					Contract_Price = Convert.ToDecimal(Price),
					Room_Id = SelectedRoomId,
					Contract_StartDate = StartDate.Value,
					Contract_EndDate = EndDate.Value,
					Create_By = "idUser1",
					Create_At = DateTime.Now
				};

				try
				{
					try
					{
						await _firebaseClient
						.Child("Rooms")
						.Child(SelectedRoomId)
						.PutAsync(new { id_host = DwellerId });

						MessageBox.Show("Get Host for Room Success");
					}
					catch (Exception e)
					{
						MessageBox.Show(e.Message);
					}


					await _firebaseClient
						.Child("Contracts")
						.PostAsync(c);
					MessageBox.Show("Add Contract Success");
					if (obj is Window window)
					{
						window.DialogResult = true;
						window.Close();
					}
				}
				catch (Exception e)
				{
					MessageBox.Show(e.Message);
				}
			}
			else
			{
				MessageBox.Show(DwellerId + " " + Description + " " +Price+ " " +SelectedRoomId+ " " +StartDate.ToString()+" "+EndDate.ToString());
			}
		}

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName=null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
