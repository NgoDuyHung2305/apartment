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
    internal class Block_Dweller_View_Model: Base_View_Model
    {
        private readonly FirebaseService _firebaseService;
        public Block Block;
        public ICommand AddDwellerCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand UpdateDwellerCommand { get; set; }


        public AddDweller AddDweller;
        private ObservableCollection<Dweller> _dwellerList { get; set; }
        public ObservableCollection<Dweller> Dwellers{ get; set; }
        public ObservableCollection<Dweller> DwellerList
        {
            get => _dwellerList;
            set
            {
                _dwellerList = value;
                OnPropertyChanged(nameof(DwellerList));
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
        private Dweller _selectedDweller { get; set; }
        public Dweller SelectedDweller
        {
            get => _selectedDweller;
            set
            {
                _selectedDweller = value;
                OnPropertyChanged(nameof(SelectedDweller));
            }
        }
        public UpdateDweller UpdateDweller;

        public Block_Dweller_View_Model() {
        }
        public Block_Dweller_View_Model(Block block)
        {
            _firebaseService = new FirebaseService();
            DwellerList = new ObservableCollection<Dweller>();
            Dwellers = new ObservableCollection<Dweller>();
            Block = new Block(block);
            GetAsync(DwellerList, Block);
            GetAsync(Dwellers, Block);
            AddDwellerCommand = new RelayCommand(async _ => await AddDwellerClick());
            SearchCommand = new RelayCommand<string>(OnSearch);
            UpdateDwellerCommand = new RelayCommand<Dweller>(async _ => await UpdateDwellerClick());
        }
        public async Task AddDwellerClick()
        {
            AddDweller = new AddDweller();
            AddDweller.ShowDialog();
            RefreshDwellerList(Dwellers);
        }
        public async Task UpdateDwellerClick()
        {
            if (SelectedDweller != null)
            {
                UpdateDweller = new UpdateDweller
                {
                    DataContext = new Update_Dweller_View_Model(SelectedDweller)
                };
                UpdateDweller.ShowDialog();
            }
            RefreshDwellerList(Dwellers);
        }
        private async void GetAsync(ObservableCollection<Dweller> dwellerList, Block block)
        {

            var Rooms = await _firebaseService.GetDataAsync<Model.Room>("Rooms", "RoomID");
            var BlockRooms = Rooms.Where(room => room.BlockID == block.BlockID);
            var Dwellers = await _firebaseService.GetDataAsync<Dweller>("Dwellers", "DwellerID");
            var BlockDwellers = Dwellers.Where(dweller => BlockRooms.Any(room => room.RoomID == dweller.RoomID));
            App.Current.Dispatcher.Invoke(() =>
            {
                foreach (var dweller in BlockDwellers)
                {
                    dwellerList.Add(dweller);
                }
            });
        }
        private async Task RefreshDwellerList(ObservableCollection<Dweller> dwellerList)
        {
            dwellerList.Clear();
            await Task.Run(() => GetAsync(dwellerList,Block));
        }
        private void OnSearch(string obj)
		{
			if (string.IsNullOrWhiteSpace(SearchText))
			{
				Dwellers.Clear();
				foreach (var contract in DwellerList)
				{
                    Dwellers.Add(contract);
				}
			}
			else
			{
				var lowerSearchText=RemoveVietnameseDiacritics(SearchText.ToLower());
				var filterDweller = DwellerList.Where(c =>
				(c.DwellerID != null && RemoveVietnameseDiacritics(c.DwellerID.ToLower()).Contains(lowerSearchText)) ||
				(c.DwellerName != null && RemoveVietnameseDiacritics(c.DwellerName.ToLower()).Contains(lowerSearchText)) ||
				(c.RoomID != null && RemoveVietnameseDiacritics(c.RoomID.ToLower()).Contains(lowerSearchText)) ||
				(c.DwellerPhone.ToString() != null && RemoveVietnameseDiacritics(c.DwellerPhone.ToString().ToLower()).Contains(lowerSearchText))||
				(c.DwellerEmail != null && RemoveVietnameseDiacritics(c.DwellerEmail.ToLower())==lowerSearchText)).ToList();

                Dwellers.Clear();
				foreach (var dweller in filterDweller)
				{
                    Dwellers.Add(dweller);
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
