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
    internal class Block_View_Model : Base_View_Model
    {
        private readonly FirebaseService _firebaseService;

        private MainWindowViewModel _mainViewModel;
        public ICommand BlockDwellerCommand { get; set; }
        public ICommand BlockRoomCommand { get; set; }
        public ICommand BlockOrderCommand { get; set; }
        public ICommand BlockReceiptCommand { get; set; }

        public BlockDwellerManagement BlockDwellerManagement { get; set; }
        public RoomManagement RoomManagement { get; set; }
        public BlockReceiptManagement BlockReceiptManagement { get; set; }
        public BlockOrderManagement BlockOrderManagement { get; set; }
        public Block Block { get; set; }
        public ObservableCollection<Model.Room> EmptyRoomList { get;set; }
        public ObservableCollection<Order> UnsolveOrderList { get; set; }
        public Block_View_Model(MainWindowViewModel mainViewModel, Block block)
        {
            _firebaseService = new FirebaseService();
            Block = new Block(block);
            EmptyRoomList = new ObservableCollection<Model.Room>();
            UnsolveOrderList = new ObservableCollection<Order>();
            GetAsync(EmptyRoomList, UnsolveOrderList, Block);
            _mainViewModel = mainViewModel;
            BlockDwellerCommand = new RelayCommand(async _ => await BlockDwekkerClick());
            BlockRoomCommand = new RelayCommand(async _ => await BlockRoomClick());
            BlockOrderCommand = new RelayCommand(async _ => await BlockOrderClick());
            BlockReceiptCommand = new RelayCommand(async _ => await BlockReceiptClick());
        }
        public Block_View_Model() { }
        private async Task BlockDwekkerClick()
        {
            BlockDwellerManagement = new BlockDwellerManagement
            {
                DataContext = new Block_Dweller_View_Model(Block)
            };

            // Chuyển sang BlockView
            _mainViewModel.CurrentView = BlockDwellerManagement;
        }
        private async Task BlockRoomClick()
        {
            RoomManagement = new RoomManagement
            {
                DataContext = new Room_Management_View_Model(_mainViewModel, Block)
            };
            _mainViewModel.CurrentView = RoomManagement;
        }
        private async Task BlockReceiptClick()
        {
            BlockReceiptManagement = new BlockReceiptManagement
            {
                DataContext = new Block_Receipt_View_Model(_mainViewModel, Block)
            };
            _mainViewModel.CurrentView = BlockReceiptManagement;
        }
        private async Task BlockOrderClick()
        {
            BlockOrderManagement = new BlockOrderManagement
            {
                DataContext = new Block_Order_View_Model(_mainViewModel, Block)
            };
            _mainViewModel.CurrentView = BlockOrderManagement;
        }
        private async void GetAsync(ObservableCollection<Model.Room> emptyRoomList, ObservableCollection<Order> unsolvedOrdersList, Block block)
        {
            var Rooms = await _firebaseService.GetDataAsync<Model.Room>("Rooms", "RoomID");
            var BlockRooms = Rooms.Where(room => room.BlockID == block.BlockID);
            var EmptyRooms= BlockRooms.Where(room=> room.Status=="Empty");
            foreach (var room in EmptyRooms)
            {
                emptyRoomList.Add(room);
            }
            var Orders = await _firebaseService.GetDataAsync<Order>("Orders", "OrderID");
            var UnsolveOrder = Orders.Where(order => BlockRooms.Any(room=>room.RoomID==order.RoomID)&&order.OrderStatus== "Unsolved");  
            foreach (var order in UnsolveOrder)
            {
                unsolvedOrdersList.Add(order);
            }
        }
    }
}

