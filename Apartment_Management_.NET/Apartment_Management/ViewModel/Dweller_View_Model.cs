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
    internal class Dweller_View_Model:Base_View_Model
	{
        private readonly FirebaseService _firebaseService;
        private ObservableCollection<Dweller> _dwellersList { get; set; }
        private MainWindowViewModel _mainViewModel;

        public ObservableCollection<Dweller> DwellerList
        {
            get => _dwellersList;
            set
            {
                _dwellersList = value;
                OnPropertyChanged(nameof(DwellerList));
            }
        }
        public ObservableCollection<Dweller> Dwellers { get; set; }

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
                _selectedDweller= value;
                OnPropertyChanged(nameof(SelectedDweller));
            }
        }
        public ICommand AddDwellerCommand { get; set; }
        public ICommand UpdateDwellerCommand { get; set; }
        public ICommand AccountCommand { get; set; }

        public ICommand SearchCommand { get; set; }

        public AddDweller AddDweller;

        public UpdateDweller UpdateDweller;
        public Account Account;
        public Dweller_View_Model() { }

        public Dweller_View_Model(MainWindowViewModel mainWindow)
        {
            _firebaseService = new FirebaseService();
            _mainViewModel = mainWindow;
            DwellerList = new ObservableCollection<Dweller>();
            Dwellers = new ObservableCollection<Dweller>();
            GetDwellerAsync(DwellerList);
            GetDwellerAsync(Dwellers);
            AddDwellerCommand = new RelayCommand(async _ => await AddDwellerClick());
            UpdateDwellerCommand = new RelayCommand<Dweller>(async _ => await UpdateDwellerClick());
            SearchCommand = new RelayCommand<string>(OnSearch);
            AccountCommand = new RelayCommand(async _ => await AccountClick());
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
        public async Task AccountClick()
        {
            Account = new Account()
            {
                DataContext = new Account_View_Model()
            };
            _mainViewModel.CurrentView= Account;
        }
        private async void GetDwellerAsync(ObservableCollection<Dweller> dwellerList)
        {
            var dwellers = await _firebaseService.GetDataAsync<Dweller>("Dwellers", "DwellerID");

            App.Current.Dispatcher.Invoke(() =>
            {
                dwellerList.Clear(); 
                foreach (var dweller in dwellers)
                {
                    dwellerList.Add(dweller);
                }

            });
        }
        private async Task RefreshDwellerList(ObservableCollection<Dweller> dwellerList)
        {
            dwellerList.Clear();
            await Task.Run(() => GetDwellerAsync(dwellerList));
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
                var lowerSearchText = RemoveVietnameseDiacritics(SearchText.ToLower());
                var filterContracts = DwellerList.Where(c =>
                (c.DwellerID != null && RemoveVietnameseDiacritics(c.DwellerID.ToLower()).Contains(lowerSearchText)) ||
                (c.DwellerName != null && RemoveVietnameseDiacritics(c.DwellerName.ToLower()).Contains(lowerSearchText)) ||
                (c.RoomID != null && RemoveVietnameseDiacritics(c.RoomID.ToLower()).Contains(lowerSearchText)) ||
                (c.DwellerPhone.ToString() != null && RemoveVietnameseDiacritics(c.DwellerPhone.ToString().ToLower()).Contains(lowerSearchText)) ||
                (c.DwellerEmail != null && RemoveVietnameseDiacritics(c.DwellerEmail.ToLower()) == lowerSearchText)).ToList();

                Dwellers.Clear();
                foreach (var contract in filterContracts)
                {
                    Dwellers.Add(contract);
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
