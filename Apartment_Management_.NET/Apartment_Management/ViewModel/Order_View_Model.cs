using Apartment_Management.Helper;
using Apartment_Management.Model;
using Apartment_Management.Service;
using Apartment_Management.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Apartment_Management.ViewModel
{
	internal class Order_View_Model:Base_View_Model
	{
        private readonly FirebaseService _firebaseService;
        public ObservableCollection<Order> UnsolvedOrdersList { get; set; }
        public ObservableCollection<Order> UnsolvedOrders { get; set; }

        public ObservableCollection<Order> SolvedOrdersList { get; set; }
        public ObservableCollection<Order> SolvedOrders { get; set; }
        private Order _selectedOrder;
        private MainWindowViewModel _mainViewModel;
        public Account Account;


        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {

                    _selectedOrder = value;
                    OnPropertyChanged(nameof(SelectedOrder));
                
            }
        }

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

        public ICommand SearchCommand { get; set; }
        public ICommand UpdateOrderCommand { get; set; }
        public ICommand AccountCommand { get; set; }
        public Order_View_Model() { }

        public Order_View_Model(MainWindowViewModel mainWindow)
        {
            _firebaseService = new FirebaseService();
            _mainViewModel = mainWindow;
            UnsolvedOrdersList = new ObservableCollection<Order>();
            UnsolvedOrders = new ObservableCollection<Order>();
            SolvedOrdersList = new ObservableCollection<Order>();
            SolvedOrders = new ObservableCollection<Order>();
            GetOrderAsync(UnsolvedOrdersList, SolvedOrdersList);
            GetOrderAsync(UnsolvedOrders, SolvedOrders);
            SearchCommand = new RelayCommand<string>(OnSearch);
            UpdateOrderCommand = new RelayCommand<Window>(async _ => await UpdateOrder());
            AccountCommand = new RelayCommand(async _ => await AccountClick());

        }
        private async void GetOrderAsync(ObservableCollection<Order> unsolvedOrdersList,ObservableCollection<Order> solvedOrdersList)
        {
            unsolvedOrdersList.Clear();
            solvedOrdersList.Clear();
            var Orders = await _firebaseService.GetDataAsync<Order>("Orders", "OrderID");
            var UnsolveRequests = Orders.Where(request => request.OrderStatus == "Unsolved");
            foreach (var order in UnsolveRequests)
            {
                unsolvedOrdersList.Add(order);
            }
            var SolveRequests = Orders.Where(request => request.OrderStatus == "Solved");
            foreach (var order in SolveRequests)
            {
                solvedOrdersList.Add(order);
            }
        }
        private async Task UpdateOrder()
        {
            if (SelectedOrder != null)
            {
                var result = MessageBox.Show(
            "Order dã hoàn thành?",
            "Update Status",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Người dùng chọn Yes
                    await _firebaseService.UpdateFieldAsync("Orders", SelectedOrder.OrderID, "ord_status", "Solved");
                    MessageBox.Show("Cập nhật thành công.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    GetOrderAsync(UnsolvedOrdersList, SolvedOrdersList);
                    GetOrderAsync(UnsolvedOrders, SolvedOrders);
                }
                else
                {
                    // Người dùng chọn No
                }
            }

        }
        public async Task AccountClick()
        {
            Account = new Account()
            {
                DataContext = new Account_View_Model()
            };
            _mainViewModel.CurrentView = Account;
        }

            private void OnSearch(string obj)
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                UnsolvedOrders.Clear();
                SolvedOrders.Clear();
                foreach (var contract in UnsolvedOrdersList)
                {
                    UnsolvedOrders.Add(contract);
                }
                foreach (var contract in SolvedOrdersList)
                {
                    SolvedOrders.Add(contract);
                }
            }
            else
            {
                var lowerSearchText = RemoveVietnameseDiacritics(SearchText.ToLower());
                var filterUnsolveOrders = UnsolvedOrdersList.Where(c =>
                (c.RoomID != null && RemoveVietnameseDiacritics(c.RoomID.ToLower()).Contains(lowerSearchText)) ||
                (c.OrderDescription != null && RemoveVietnameseDiacritics(c.OrderDescription.ToLower()).Contains(lowerSearchText)) ||
                (c.RoomID != null && RemoveVietnameseDiacritics(c.RoomID.ToLower()).Contains(lowerSearchText))).ToList();
                var filterSolveOrders = SolvedOrdersList.Where(c =>
               (c.RoomID != null && RemoveVietnameseDiacritics(c.RoomID.ToLower()).Contains(lowerSearchText)) ||
               (c.OrderDescription != null && RemoveVietnameseDiacritics(c.OrderDescription.ToLower()).Contains(lowerSearchText)) ||
               (c.RoomID != null && RemoveVietnameseDiacritics(c.RoomID.ToLower()).Contains(lowerSearchText))).ToList();

                UnsolvedOrders.Clear();
                foreach (var order in filterUnsolveOrders)
                {
                    UnsolvedOrders.Add(order);
                }
                SolvedOrders.Clear();
                foreach (var order in filterSolveOrders)
                {
                    SolvedOrders.Add(order);
                }
            }


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
