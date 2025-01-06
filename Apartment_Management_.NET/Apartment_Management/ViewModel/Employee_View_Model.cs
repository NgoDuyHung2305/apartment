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
	internal class Employee_View_Model:Base_View_Model
	{
        private readonly FirebaseService _firebaseService;
        private ObservableCollection<Employee> _employeesList { get; set; }
        public ObservableCollection<Employee> EmployeeList
        {
            get => _employeesList;
            set
            {
                _employeesList = value;
                OnPropertyChanged(nameof(EmployeeList));
            }
        }
        public ObservableCollection<Employee> Employees { get; set; }
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
        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                
                    _selectedEmployee = value;
                    OnPropertyChanged(nameof(SelectedEmployee));
                
            }
        }
        public ICommand AddEmployeeCommand { get; set; }
        public ICommand UpdateEmployeeCommand { get; set; }

        public ICommand SearchCommand { get; set; }
        public ICommand AccountCommand { get; set; }



        //public AddEmployee AddEmployee;
        public AddEmployee AddEmployee;
        public UpdateEmployee UpdateEmployee;
        public Account Account;
        private MainWindowViewModel _mainViewModel;

        public Employee_View_Model() { }

        public Employee_View_Model(MainWindowViewModel mainWindow)
        {
            _firebaseService = new FirebaseService();
            _mainViewModel = mainWindow;
            EmployeeList = new ObservableCollection<Employee>();
            Employees = new ObservableCollection<Employee>();
            GetEmployeeAsync(EmployeeList);
            GetEmployeeAsync(Employees);
            AddEmployeeCommand = new RelayCommand(async _ => await AddEmployeeClick());
            UpdateEmployeeCommand = new RelayCommand<Dweller>(async _ => await UpdateEmployeeClick());
            SearchCommand = new RelayCommand<string>(OnSearch);
            AccountCommand = new RelayCommand(async _ => await AccountClick());
        }
        public async Task AddEmployeeClick()
        {
            AddEmployee = new AddEmployee();
            AddEmployee.ShowDialog();
            RefreshEmployeeList(Employees);
        }
        public async Task UpdateEmployeeClick()
        {
            if (SelectedEmployee != null)
            {
                UpdateEmployee = new UpdateEmployee
                {
                    DataContext = new Update_Employee_View_Model(SelectedEmployee)
                };
                UpdateEmployee.ShowDialog();
            }
            RefreshEmployeeList(Employees);
        }
        private async void GetEmployeeAsync(ObservableCollection<Employee> employeeList)
        {
            var employees = await _firebaseService.GetDataAsync<Employee>("Employees", "EmployeeID");
            App.Current.Dispatcher.Invoke(() =>
            {
                employeeList.Clear();
                foreach (var employee in employees)
                {
                    employeeList.Add(employee);
                }
            });
            
        }
        private async Task RefreshEmployeeList(ObservableCollection<Employee> employeeList)
        {
            employeeList.Clear();
            await Task.Run(() => GetEmployeeAsync(employeeList));
        }
        private void OnSearch(string obj)
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Employees.Clear();
                foreach (var contract in EmployeeList)
                {
                    Employees.Add(contract);
                }
            }
            else
            {
                var lowerSearchText = RemoveVietnameseDiacritics(SearchText.ToLower());
                var filterContracts = EmployeeList.Where(c =>
                (c.EmployeeName != null && RemoveVietnameseDiacritics(c.EmployeeName.ToLower()).Contains(lowerSearchText)) ||
                (c.EmployeeRole != null && RemoveVietnameseDiacritics(c.EmployeeRole.ToLower()).Contains(lowerSearchText)) ||
                (c.EmployeePhone.ToString() != null && RemoveVietnameseDiacritics(c.EmployeePhone.ToString().ToLower()).Contains(lowerSearchText)) ||
                (c.EmployeeEmail != null && RemoveVietnameseDiacritics(c.EmployeeEmail.ToLower()) == lowerSearchText)).ToList();

                Employees.Clear();
                foreach (var contract in filterContracts)
                {
                    Employees.Add(contract);
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

