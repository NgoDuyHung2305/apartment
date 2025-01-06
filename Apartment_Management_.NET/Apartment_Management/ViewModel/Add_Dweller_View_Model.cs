using Apartment_Management.Model;
using Apartment_Management.Service;
using Firebase.Database;
using Firebase.Database.Query;
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
    internal class Add_Dweller_View_Model: Base_View_Model
    {
        private readonly FirebaseService _firebaseService;
        private ObservableCollection<Model.Block> _blockList;
        private ObservableCollection<Room> _roomList;
        private Model.Block _selectedBlock;
        private Room _selectedRoom;
        private string _dwellerName;
        private string _dwellerSex;
        private string _hometown;
        private string _phone;
        private string _email;
        private DateTime? _birthDate;
        private DateTime? _startDate;
        public ObservableCollection<Model.Block> BlockList
        {
            get => _blockList;
            set
            {
                _blockList = value;
                OnPropertyChanged(nameof(BlockList));
            }
        }

        public ObservableCollection<Room> RoomList
        {
            get => _roomList;
            set
            {
                _roomList = value;
                OnPropertyChanged(nameof(RoomList));
            }
        }

        public Model.Block SelectedBlock
        {
            get => _selectedBlock;
            set
            {
                _selectedBlock = value;
                OnPropertyChanged(nameof(SelectedBlock));
                LoadRoomsForBlock(value); // Khi thay đổi block, load danh sách phòng của block đó
            }
        }

        public Room SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                _selectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }
        public string DwellerName
        {
            get => _dwellerName;
            set
            {
                _dwellerName = value;
                OnPropertyChanged(nameof(DwellerName));
            }
        }
        public string DwellerSex
        {
            get => _dwellerSex;
            set
            {
                _dwellerSex = value;
                OnPropertyChanged(nameof(DwellerSex));
            }
        }

        public string Hometown
        {
            get => _hometown;
            set
            {
                _hometown = value;
                OnPropertyChanged(nameof(Hometown));
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public DateTime? BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                OnPropertyChanged(nameof(BirthDate));
            }
        }

        public DateTime? StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public Add_Dweller_View_Model() {
            _firebaseService = new FirebaseService();
            SaveCommand = new RelayCommand<Window>(async (window) => await SaveDweller(window));
            CancelCommand = new RelayCommand<Window>(window => CancelClick(window));
            LoadBlocks();
        }
         private async Task SaveDweller(Window window)
            {
            Dweller  newDweller=null;
                if (SelectedBlock == null || SelectedRoom == null)
                {
                MessageBox.Show("Please select a Block and Room.");
                return;
            }
            try
            {
                newDweller = new Dweller
                {
                    DwellerName = DwellerName,
                    DwellerSex = DwellerSex,
                    DwellerPhone = Phone,
                    DwellerHomeTown = Hometown,
                    DwellerEmail = Email,
                    RoomID = SelectedRoom.RoomID,
                    DwellerBirth = BirthDate.Value,
                    Dweller_Date_Start = StartDate.Value
                };
            }
            catch (Exception e)
            {
            }
            try
            {
                await _firebaseService.AddDataAsync<Dweller>("Dwellers", newDweller);
                await _firebaseService.UpdateFieldAsync("Rooms", SelectedRoom.RoomID, "room_status", "Rented");
                MessageBox.Show("Thêm cư dân thành công");
                window?.Close();
            }
            catch (Exception e)
            {
            }
            window?.Close();
        }

        public void CancelClick(Window window)
        {
            window?.Close();
        }
        private async void LoadBlocks()
        {
            var Blocks = await _firebaseService.GetDataAsync<Model.Block>("Blocks", "BlockID");
            BlockList = new ObservableCollection<Model.Block>(Blocks);
        }

        private async void LoadRoomsForBlock(Model.Block block)
        {
            var Rooms = await _firebaseService.GetDataAsync<Model.Room>("Rooms", "RoomID");
            var BlockRooms = Rooms.Where(room => room.BlockID == block.BlockID);
            RoomList = new ObservableCollection<Room>(BlockRooms);
        }
    }
}
