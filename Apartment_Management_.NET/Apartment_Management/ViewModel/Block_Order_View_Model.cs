using Apartment_Management.Model;
using Apartment_Management.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace Apartment_Management.ViewModel
{
    internal class Block_Order_View_Model:Base_View_Model
    {
        private readonly FirebaseService _firebaseService;
        public Model.Block Block;
        public ObservableCollection<Order> UnsolvedOrdersList { get; set; }
        public ObservableCollection<Order> UnsolvedOrders { get; set; }

        public ObservableCollection<Order> SolvedOrdersList { get; set; }
        public ObservableCollection<Order> SolvedOrders { get; set; }

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

        public Block_Order_View_Model() {        }
        public Block_Order_View_Model(MainWindowViewModel mainWindow, Model.Block block)
        {
            _firebaseService = new FirebaseService();
            UnsolvedOrdersList = new ObservableCollection<Order>();
            UnsolvedOrders = new ObservableCollection<Order>();
            SolvedOrdersList = new ObservableCollection<Order>();
            SolvedOrders = new ObservableCollection<Order>();
            Block = new Model.Block(block);
            GetOrderAsync(UnsolvedOrdersList, SolvedOrdersList, Block);
            GetOrderAsync(UnsolvedOrders, SolvedOrders, Block);
            SearchCommand = new RelayCommand<string>(OnSearch);
        }
        private async void GetOrderAsync(ObservableCollection<Order> unsolvedOrdersList, ObservableCollection<Order> solvedOrdersList,Model.Block block)
        {
            var Rooms = await _firebaseService.GetDataAsync<Model.Room>("Rooms", "RoomID");
            var BlockRooms = Rooms.Where(room => room.BlockID == block.BlockID);
            var Orders = await _firebaseService.GetDataAsync<Order>("Orders");
            var UnsolveRequests = Orders.Where(order => BlockRooms.Any(room => room.RoomID == order.RoomID)&&order.OrderStatus== "Unsolved");
            foreach (var order in UnsolveRequests)
            {
                unsolvedOrdersList.Add(order);
            }
            var SolveRequests = Orders.Where(order => BlockRooms.Any(room => room.RoomID == order.RoomID) && order.OrderStatus == "Solved");
            foreach (var order in SolveRequests)
            {
                solvedOrdersList.Add(order);
            }
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
