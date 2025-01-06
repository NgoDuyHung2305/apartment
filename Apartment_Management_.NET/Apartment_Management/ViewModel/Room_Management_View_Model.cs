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
    internal class Room_Management_View_Model:Base_View_Model
    {
        private readonly FirebaseService _firebaseService;
        public ObservableCollection<Room> RoomList { get; set; }
        public RoomView RoomView { get; set; }
        public Block Block { get; set; }
        private Room _selectedRoom;
        private MainWindowViewModel _mainViewModel;
        public ICommand RoomCommand { get; set; }



        public Room SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                _selectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }
        public Room_Management_View_Model() { }
        public Room_Management_View_Model(MainWindowViewModel mainWindow, Block block)
        {
            _firebaseService = new FirebaseService();
            _mainViewModel = mainWindow;
            RoomList = new ObservableCollection<Room>();
            RoomCommand = new Helper.RelayCommand(async _ => await RoomClick());
            Block = new Block(block);
            GetAsync(RoomList, Block);
        }

        private async void GetAsync(ObservableCollection<Room> roomList, Block block)
        {
            var Rooms = await _firebaseService.GetDataAsync<Model.Room>("Rooms", "RoomID");
            var BlockRooms = Rooms.Where(room => room.BlockID == block.BlockID);
            foreach (var room in BlockRooms)
            {
                roomList.Add(room);
            }
        }
        private async Task RoomClick()
        {
            if (SelectedRoom != null)
            {
                RoomView = new RoomView
                {
                    DataContext = new Room_View_Model(_mainViewModel, SelectedRoom)
                };
                _mainViewModel.CurrentView = RoomView;
            }
        }
    }
}
