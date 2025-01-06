using Apartment_Management.Helper;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Apartment_Management.Service;
using System.Windows;
using Apartment_Management.Model;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System.Globalization;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;


namespace Apartment_Management.ViewModel
{
	public class Home_View_Model : INotifyPropertyChanged
	{



		private const string firebaseUrl = "https://apartment-management-2h-default-rtdb.firebaseio.com/";
		private readonly FirebaseClient _firebaseClient;

		public ObservableCollection<Contract> _NewContracts { get; set; }
		public ObservableCollection<Dweller> _NewDwellers { get; set; }
		public ObservableCollection<Order> _NewOrder { get; set; }
		public ObservableCollection<Room> _Rooms { get; set; }


		public ObservableCollection<Contract> _filterNewContracts { get; set; }
		public ObservableCollection<Contract> _filterOldContracts { get; set; }

		public ObservableCollection<Dweller> _filterNewDwellers { get; set; }
		public ObservableCollection<Dweller> _filterOldDwellers { get; set; }


		public ObservableCollection<Order> _filterNewOrder { get; set; }
		public ObservableCollection<Order> _filterOldOrder { get; set; }


		public ObservableCollection<Order> _filterSolvedOrder { get; set; }
		public ObservableCollection<Order> _filterOldSolvedOrder { get; set; }


		public ObservableCollection<Room> _filterRooms { get; set; }


		public List<int> listPublicOrders { get; set; }
		public List<int> listResidencedOrders { get; set; }
		public List<string> MonthLabels { get; set; }

		public SeriesCollection ChartData { get; set; }

		public ChartValues<int> publicor = new ChartValues<int>();
		public ChartValues<int> resior = new ChartValues<int>();




		private readonly FirebaseService _firebaseService;


		private int _todaysOrders;
		private int _yesterdaysOrders;


		private int _newDwellers;
		private int _lastmonthDwellers;


		private double _filledRoomRate;
		private double _solvedtoday;
		private double _solvedyesterday;


		private int _solvedOrders;
		private int _yesterdaysolvedOrders;


		private int _newContracts;
		private int _lastmonthContracts;

		public string publica;
		public string resida;





		public event PropertyChangedEventHandler PropertyChanged;

		// Thuộc tính


		public string Publica
		{
			get => publica;
			set { publica = value;OnPropertyChanged(nameof(Publica)); }
		}


		public string Resida
		{
			get => resida;
			set { resida = value; OnPropertyChanged(nameof(Resida)); }
		}


		public double SolveToday
		{
			get => _solvedtoday;
			set { _solvedtoday = value; OnPropertyChanged(nameof(SolveToday)); }
		}

		public double SolveYesterday
		{
			get => _solvedyesterday;
			set { _solvedyesterday = value; OnPropertyChanged(nameof(SolveYesterday)); }
		}

		public int YesterdayOrders
		{
			get => _yesterdaysOrders;
			set
			{
				_yesterdaysOrders = value;
				OnPropertyChanged(nameof(YesterdayOrders));
			}
		}

		public int LastMonthDwellers
		{
			get => _lastmonthDwellers;
			set { _lastmonthDwellers = value; OnPropertyChanged(nameof(LastMonthDwellers)); }
		}

		public int YesterdaysolvedOrders
		{
			get => _yesterdaysolvedOrders;
			set { _yesterdaysolvedOrders = value; OnPropertyChanged(nameof(YesterdaysolvedOrders)); }
		}

		public int LastMonthContracts
		{
			get => _lastmonthContracts;
			set { _lastmonthContracts = value; OnPropertyChanged(nameof(LastMonthContracts)); }
		}



		public int TodaysOrders
		{
			get => _todaysOrders;
			set
			{
				_todaysOrders = value;
				OnPropertyChanged(nameof(TodaysOrders));
			}
		}

		public int NewDwellers
		{
			get => _newDwellers;
			set { _newDwellers = value; OnPropertyChanged(nameof(NewDwellers)); }
		}

		public double FilledRoomRate
		{
			get => _filledRoomRate;
			set { _filledRoomRate = value; OnPropertyChanged(nameof(FilledRoomRate)); }
		}

		public int SolvedOrders
		{
			get => _solvedOrders;
			set { _solvedOrders = value; OnPropertyChanged(nameof(SolvedOrders)); }
		}

		public int NewContracts
		{
			get => _newContracts;
			set { _newContracts = value; OnPropertyChanged(nameof(NewContracts)); }
		}

		// Lệnh (Commands)
		public ICommand ShowTodaysOrdersCommand { get; }
		public ICommand ShowNewDwellersCommand { get; }
		public ICommand ShowFilledRoomRateCommand { get; }
		public ICommand ShowSolvedOrdersCommand { get; }
		public ICommand ShowNewContractsCommand { get; }


		public ICommand InforCardClickCommand { get; set; }


		// Constructor
		public Home_View_Model()
		{
			_firebaseClient = new FirebaseClient(firebaseUrl);
			ShowTodaysOrdersCommand = new RelayCommand(async _ => await ShowTodaysOrders());
			ShowNewDwellersCommand = new RelayCommand(async _ => await ShowNewDwellers());
			ShowFilledRoomRateCommand = new RelayCommand(async _ => await ShowFilledRoomRate());
			ShowSolvedOrdersCommand = new RelayCommand(async _ => await ShowSolvedOrders());
			ShowNewContractsCommand = new RelayCommand(async _ => await ShowNewContracts());
			_NewContracts = new ObservableCollection<Contract>();
			_NewDwellers = new ObservableCollection<Dweller>();
			_NewOrder = new ObservableCollection<Order>();
			_Rooms = new ObservableCollection<Room>();

			InforCardClickCommand = new RelayCommand<string>(OnInforCardClick);

			_filterNewContracts = new ObservableCollection<Contract>();
			_filterNewDwellers = new ObservableCollection<Dweller>();
			_filterNewOrder = new ObservableCollection<Order>();
			_filterSolvedOrder = new ObservableCollection<Order>();
			_filterRooms = new ObservableCollection<Room>();
			_filterOldContracts = new ObservableCollection<Contract>();
			_filterOldDwellers = new ObservableCollection<Dweller>();
			_filterOldOrder = new ObservableCollection<Order>();
			_filterOldSolvedOrder = new ObservableCollection<Order>();

			

			listPublicOrders = new List<int>();
			listResidencedOrders = new List<int>();

			



			ChartData = new SeriesCollection
			{
				new StackedColumnSeries
				{
					Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 144, 238, 144)),
					MaxColumnWidth=15,
					Title = "Public Orders",
					Values = publicor
				},
				new StackedColumnSeries
				{
					Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 250, 235, 215)),
					MaxColumnWidth=15,
					Title = "Residence Orders",
					Values = resior
				}

			};
			MonthLabels = new List<string>
	{
		"Jan", "Feb", "Mar", "Apr", "May", "Jun",
		"Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
	};

			listPublicOrders =Enumerable.Repeat(0,12).ToList();
			listResidencedOrders = Enumerable.Repeat(0, 12).ToList();


			LoadData();
		}

		private void OnInforCardClick(string cardName)
		{
			switch (cardName)
			{
				case "btn_order_today":
					if (TodaysOrders >= YesterdayOrders)
					{
						MessageBox.Show("Todays Orders is more " + (TodaysOrders - YesterdayOrders) + " than yesterday!");
					}
					else
					{
						MessageBox.Show("Todays Orders is less " + (-TodaysOrders + YesterdayOrders) + " than yesterday!");
					}
					break;
				case "btn_new_dwellers":
					if (NewDwellers >= LastMonthDwellers)
					{
						MessageBox.Show("This month's new Dwellers is more " + (NewDwellers - LastMonthDwellers) + " than last month!");
					}
					else
					{
						MessageBox.Show("This month's new Dwellers is less " + (-NewDwellers + LastMonthDwellers) + " than last month!");
					}
					break;
				case "btn_fill_rate":
					MessageBox.Show("There are " + (_filterRooms.Count) + "/80 rooms are being rented");
					break;
				case "btn_solvedorder_today":
					MessageBox.Show("Today's Solved Orders rate: " + SolvedOrders + "/" + TodaysOrders + " and Yesterday's Solved Orders rate: " + YesterdaysolvedOrders + "/" + YesterdayOrders);
					break;
				case "btn_new_contracts":
					if (NewContracts >= LastMonthContracts)
					{
						MessageBox.Show("This month's new Contracts is more " + (NewContracts - LastMonthContracts) + " than last month!");
					}
					else
					{
						MessageBox.Show("This month's Contracts is less " + (-NewContracts + LastMonthContracts) + " than last month!");
					}
					break;
			}
		}

		private async void LoadData()
		{
			try
			{
				var today = DateTime.Now.ToString("yyyy-MM-dd");
				var thisMonth = DateTime.Now.ToString("yyyy-MM");
				var yesterday = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
				var lastMonth = DateTime.Now.AddMonths(-1).ToString("yyyy-MM");


				var firebaseContracts = await _firebaseClient
			.Child("Contracts")
			.OnceAsync<object>();
				_NewContracts.Clear();
				foreach (var item in firebaseContracts)
				{
					var contractJson = JsonConvert.SerializeObject(item.Object);
					var contract = JsonConvert.DeserializeObject<Contract>(contractJson);
					if (contract != null)
					{
						contract.Contract_Id = item.Key;
						_NewContracts.Add(contract);
					}
					else
					{
						MessageBox.Show("Contract is null");
					}
				}
				foreach (var contract in _NewContracts)
				{
					if (contract.Create_At.ToString() != null && contract.Create_At.ToString("yyyy-MM") == thisMonth)
					{
						_filterNewContracts.Add(contract);
					}
					if (contract.Create_At.ToString() != null && contract.Create_At.ToString("yyyy-MM") == lastMonth)
					{
						_filterOldContracts.Add(contract);
					}

				}


				var firebaseDwellers = await _firebaseClient
		.Child("Dwellers")
		.OnceAsync<object>();
				_NewDwellers.Clear();
				foreach (var item in firebaseDwellers)
				{
					var dwellerJson = JsonConvert.SerializeObject(item.Object);
					var dweller = JsonConvert.DeserializeObject<Dweller>(dwellerJson);
					if (dweller != null)
					{
						dweller.DwellerID = item.Key;
						_NewDwellers.Add(dweller);
					}
					else
					{
						MessageBox.Show("Dweller is null");
					}
				}
				foreach (var dweller in _NewDwellers)
				{
					if (dweller.Create_At.ToString() != null && dweller.Create_At.ToString("yyyy-MM") == thisMonth)
					{
						_filterNewDwellers.Add(dweller);
					}
					if (dweller.Create_At.ToString() != null && dweller.Create_At.ToString("yyyy-MM") == lastMonth)
					{
						_filterOldDwellers.Add(dweller);
					}
				}



				var firebaseOrders = await _firebaseClient
			.Child("Orders")
			.OnceAsync<object>();
				_NewOrder.Clear();
				foreach (var item in firebaseOrders)
				{
					var orderJson = JsonConvert.SerializeObject(item.Object);
					var order = JsonConvert.DeserializeObject<Order>(orderJson);
					if (order != null)
					{
						order.OrderID = item.Key;
						_NewOrder.Add(order);
					}
					else
					{
						MessageBox.Show("Order is null");
					}
				}


				var ordersByMonth = _NewOrder
		.GroupBy(o => new { o.Create_At.Year, o.Create_At.Month })
		.Select(group => new
		{
			Month = group.Key.Month,
			PublicServiceCount = group.Count(o => o.Type == "Public"),  // Đếm đơn hàng loại Public
			ResidenceServiceCount = group.Count(o => o.Type == "Residence")  // Đếm đơn hàng loại Residence
		})
		.ToList();

				foreach (var monthData in ordersByMonth)
				{
					int MonthIndex = monthData.Month - 1;
					listPublicOrders[MonthIndex] = monthData.PublicServiceCount;
					listResidencedOrders[MonthIndex]=monthData.ResidenceServiceCount;
				}

				for(int i=0;i<listPublicOrders.Count; i++)
				{
					if (i == listPublicOrders.Count - 1)
						publica += listPublicOrders[i].ToString();
					publica += listPublicOrders[i].ToString() + ",";
				}

				for (int i=0;i<listResidencedOrders.Count;i++)
				{
					if (i == listResidencedOrders.Count - 1)
						resida += listResidencedOrders[i].ToString();
					resida += listResidencedOrders[i].ToString() + ",";
				}
				foreach (var order in _NewOrder)
				{
					if (order.Create_At.ToString() != null && order.Create_At.ToString("yyyy-MM-dd") == today)
					{
						_filterNewOrder.Add(order);
						if (order.OrderStatus == "Solved")
						{
							_filterSolvedOrder.Add(order);
						}
					}
					if (order.Create_At.ToString() != null && order.Create_At.ToString("yyyy-MM-dd") == yesterday)
					{
						_filterOldOrder.Add(order);
						if (order.OrderStatus == "Solved")
						{
							_filterOldSolvedOrder.Add(order);
						}
					}
				}

				var firebaseRooms = await _firebaseClient
			.Child("Rooms")
			.OnceAsync<object>();
				_Rooms.Clear();
				foreach (var item in firebaseRooms)
				{
					var roomJson = JsonConvert.SerializeObject(item.Object);
					var room = JsonConvert.DeserializeObject<Room>(roomJson);
					if (room != null)
					{
						room.RoomID = item.Key;
						_Rooms.Add(room);
					}
					else
					{
						MessageBox.Show("Dweller is null");
					}
				}
				foreach (var room in _Rooms)
				{
					if (room.Status == "Rented")
					{
						_filterRooms.Add(room);
					}
				}



				TodaysOrders = _filterNewOrder.Count;
				NewDwellers = _filterNewDwellers.Count;
				FilledRoomRate = (double)_filterRooms.Count / 80 * 100;
				SolvedOrders = _filterSolvedOrder.Count;
				NewContracts = _filterNewContracts.Count;

				YesterdayOrders = _filterOldOrder.Count;
				LastMonthDwellers = _filterOldDwellers.Count;
				YesterdaysolvedOrders = _filterOldSolvedOrder.Count;
				LastMonthContracts = _filterOldContracts.Count;

				SolveToday = (double)((double)SolvedOrders / TodaysOrders * 100);
				SolveYesterday = (double)((double)YesterdaysolvedOrders / YesterdayOrders * 100);

				

				foreach (var item in listPublicOrders)
				{
					publicor.Add(item);
				}

				foreach (var item in listResidencedOrders)
				{
					resior.Add(item);
				}

			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				throw;
			}

		}

		// Hàm xử lý sự kiện
		private async Task ShowTodaysOrders()
		{
			try
			{
				TodaysOrders = await _firebaseService.GetTodayOrderCountAsync();
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				throw;
			}
		}

		private async Task ShowNewDwellers()
		{
			try
			{
				NewDwellers = await _firebaseService.GetThisMonthNewDwellerCountAsync();
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				throw;
			}
		}

		private async Task ShowFilledRoomRate()
		{
			try
			{

				FilledRoomRate = await _firebaseService.GetRateFilledRoomAsync();
				MessageBox.Show(FilledRoomRate.ToString());
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				throw;
			}
		}

		private async Task ShowSolvedOrders()
		{
			try
			{
				SolvedOrders = await _firebaseService.GetTodaysSolvedOrderAsync();
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				throw;
			}
		}

		private async Task ShowNewContracts()
		{
			try
			{
				NewContracts = await _firebaseService.GetThisMonthContractAsync();
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				throw;
			}
		}



		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}




}
