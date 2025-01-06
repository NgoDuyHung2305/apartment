using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Apartment_Management.Helper;
using System.Windows.Input;
using Apartment_Management.Model;
using Firebase.Database;
using Newtonsoft.Json;
using System.Windows;
using Apartment_Management.View;


namespace Apartment_Management.ViewModel
{
	internal class Receipt_View_Model:Base_View_Model
	{
		private const string firebaseUrl = "https://apartment-management-2h-default-rtdb.firebaseio.com/";
		private readonly FirebaseClient _firebaseClient;

		public ObservableCollection<Receipt> Receipts { get; set; }
		public ObservableCollection<Receipt> PaidReceipts { get; set; }
		public ObservableCollection<Receipt> UnpaidReceipts { get; set; }
        private MainWindowViewModel _mainViewModel;
        public Account Account;


        public Receipt_View_Model() { }
        public Receipt_View_Model(MainWindowViewModel mainWindow)
		{
			_firebaseClient = new FirebaseClient(firebaseUrl);
			_mainViewModel = mainWindow;
			Receipts = new ObservableCollection<Receipt>();
			UnpaidReceipts = new ObservableCollection<Receipt>();
			PaidReceipts = new ObservableCollection<Receipt>();
			LoadContractAsync();
			NotificationCommand = new RelayCommand(OnNotification);
			AccountCommand = new RelayCommand(OnAccount);
			SearchCommand = new RelayCommand<string>(OnSearch);
		}



		private async void LoadContractAsync()
		{
			try
			{
				var firebaseReceipts = await _firebaseClient
			.Child("Bills")
			.OnceAsync<object>();
				Receipts.Clear();
				foreach (var item in firebaseReceipts)
				{
					var receiptJson = JsonConvert.SerializeObject(item.Object);
					var receipt = JsonConvert.DeserializeObject<Receipt>(receiptJson);

					if (receipt != null)
					{
						receipt.ReceiptID = item.Key;
						Receipts.Add(receipt);
					}
					else
						MessageBox.Show("Receipt is null");
				}

				foreach (var receipt in Receipts)
				{
					if (receipt.Status == "Paid")
						PaidReceipts.Add(receipt);
					if (receipt.Status == "Unpaid")
						UnpaidReceipts.Add(receipt);
				}
				//Contracts.Clear();
				//foreach (var item in firebaseContracts)
				//{
				//	var contract = item.Object;
				//	contract.Contract_Id = item.Key;
				//	Contracts.Add(contract);
				//}

				//MessageBox.Show("Load Contract Success");
				//foreach (var contract in Contracts)
				//{
				//	if(contract.Room_Id == null)
				//	{
				//		MessageBox.Show("Room_Id is null");
				//	}
				//	else 
				//		MessageBox.Show("Room_Id is not null");
				//}
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message);
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public ICommand NotificationCommand { get; set; }
		public ICommand AccountCommand { get; set; }
		public ICommand SearchCommand { get; set; }


		private string _searchText;
		public string SearchText
		{
			get => _searchText;
			set
			{
				if (_searchText != value)
				{
					_searchText = value;
					OnPropertyChanged();
				}
			}
		}
		private void OnSearch(string obj)
		{
			MessageBox.Show($"tim kiem tu khoa: {_searchText}");
		}


		private void OnAccount(object obj)
		{
            Account = new Account()
            {
                DataContext = new Account_View_Model()
            };
            _mainViewModel.CurrentView = Account;
        }

		private void OnNotification(object obj)
		{
			MessageBox.Show("noti click");
		}


		public string RemoveVietnameseDiacritics(string text)
		{
			string[] vietnameseChars = new string[]
			{
			"áàảãạâấầẩẫậăắằẳẵặ",
			"éèẻẽẹêếềểễệ",
			"íìỉĩị",
			"óòỏõọôốồổỗộơớờởỡợ",
			"úùủũụưứừửữự",
			"ýỳỷỹỵ",
			"đ",
			"ÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶ",
			"ÉÈẺẼẸÊẾỀỂỄỆ",
			"ÍÌỈĨỊ",
			"ÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢ",
			"ÚÙỦŨỤƯỨỪỬỮỰ",
			"ÝỲỶỸỴ",
			"Đ"
			};

			char[] replaceChars = new char[]
			{
			'a', 'e', 'i', 'o', 'u', 'y', 'd',
			'A', 'E', 'I', 'O', 'U', 'Y', 'D'
			};

			for (int i = 0; i < vietnameseChars.Length; i++)
			{
				foreach (char c in vietnameseChars[i])
				{
					text = text.Replace(c, replaceChars[i]);
				}
			}

			return text;
		}
	}
}
