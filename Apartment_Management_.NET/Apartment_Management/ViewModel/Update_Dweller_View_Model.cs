using Apartment_Management.Model;
using Apartment_Management.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Windows.Input;
using System.Windows;
using System.Windows.Documents;

namespace Apartment_Management.ViewModel
{
    internal class Update_Dweller_View_Model:Base_View_Model
    {
    private readonly FirebaseService _firebaseService;
        public Dweller Dweller;
    private string _dwellerName;
    private string _dwellerSex;
    private string _hometown;
    private string _phone;
    private string _email;
    private DateTime? _birthDate;
    private DateTime? _startDate;
        private Model.Block _dwellerBlock;
        private Room _dwellerRoom;

        public Model.Block DwellerBlock
        {
            get => _dwellerBlock;
            set
            {
                _dwellerBlock = value;
                OnPropertyChanged(nameof(DwellerBlock));
            }
        }

        public Room DwellerRoom
        {
            get => _dwellerRoom;
            set
            {
                _dwellerRoom = value;
                OnPropertyChanged(nameof(DwellerRoom));
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
        public ICommand DeleteCommand { get; set; }

        public ICommand CancelCommand { get; set; }
        public Update_Dweller_View_Model() { }
    public Update_Dweller_View_Model(Dweller dweller)
    {
        _firebaseService = new FirebaseService();
            Dweller=new Dweller(dweller);
        SaveCommand = new RelayCommand<Window>(async (window) => await SaveDweller(window));
            DeleteCommand = new RelayCommand<Window>(async (window) => await DeleteDweller(window));
            CancelCommand = new RelayCommand<Window>(window => CancelClick(window));
        LoadDweller(dweller);
            LoadRoomDetail();
    }

        private async Task SaveDweller(Window window)
    {
        Dweller newDweller = null;
        try
        {
            newDweller = new Dweller
            {
                DwellerName = DwellerName,
                DwellerSex = DwellerSex,
                DwellerPhone = Phone,
                DwellerHomeTown = Hometown,
                DwellerEmail = Email,
                RoomID = DwellerRoom.RoomID,
                DwellerBirth = BirthDate.Value,
                Dweller_Date_Start = StartDate.Value
            };
        }
        catch (Exception e)
        {
        }
        try
        {
            await _firebaseService.UpdateDataAsync<Dweller>("Dwellers",Dweller.DwellerID, newDweller);
            MessageBox.Show("Cập nhật cư dân thành công");
            window?.Close();
        }
        catch (Exception e)
        {
        }
        window?.Close();
    }
        private async Task DeleteDweller(Window window)
        {
            var result = MessageBox.Show(
        "Bạn muốn xóa cư dân này?",  
        "Confirm Delete",                               
        MessageBoxButton.YesNo,                         
        MessageBoxImage.Question);                      

            if (result == MessageBoxResult.Yes)
            {
                // Người dùng chọn Yes
                await _firebaseService.DeleteDataAsync("Dwellers", Dweller.DwellerID);
                MessageBox.Show("Xóa cư dân thành công.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                window?.Close();
            }
            else
            {
                // Người dùng chọn No
            }

        }
    public void CancelClick(Window window)
    {
        window?.Close();
    }
    private async Task<Model.Block> LoadBlock(Room room)
    {
            var blocks = await _firebaseService.GetDataAsync<Model.Block>("Blocks","BlockID");

            var block = blocks.FirstOrDefault(b => b.BlockID == room.BlockID);

            return block;
    }

    private async Task<Room> LoadRoom()
    {
        var rooms = await _firebaseService.GetDataAsync<Model.Room>("Rooms", "RoomID");
        var room =rooms.FirstOrDefault(r => r.RoomID == Dweller.RoomID);
        return room;
    }
        private async void LoadDweller(Dweller dweller)
        {
           
            DwellerName = dweller.DwellerName;
            DwellerSex = dweller.DwellerSex;
            Phone = dweller.DwellerPhone;
            Hometown = dweller.DwellerHomeTown;
            Email = dweller.DwellerEmail;
            BirthDate = dweller.DwellerBirth;
            StartDate = dweller.Dweller_Date_Start;
        }
        private async void LoadRoomDetail()
        {
            DwellerRoom = await LoadRoom();
            DwellerBlock = await LoadBlock(DwellerRoom);

        }
    }
}
