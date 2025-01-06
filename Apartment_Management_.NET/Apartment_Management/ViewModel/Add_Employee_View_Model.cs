using Apartment_Management.Model;
using Apartment_Management.Service;
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
    internal class Add_Employee_View_Model:Base_View_Model
    {
        private readonly FirebaseService _firebaseService;
        private string _employeeName;
        private string _employeeSex;
        private string _employeeRole;
        private string _employeePassword;
        private string _status;
        private string _phone;
        private string _email;
        private DateTime? _birthDate;
        private DateTime? _startDate;

        public string EmployeeName
        {
            get => _employeeName;
            set
            {
                _employeeName = value;
                OnPropertyChanged(nameof(EmployeeName));
            }
        }
        public string EmployeeSex
        {
            get => _employeeSex;
            set
            {
                _employeeSex = value;
                OnPropertyChanged(nameof(EmployeeSex));
            }
        }
        public string EmployeeRole
        {
            get => _employeeRole;
            set
            {
                _employeeRole = value;
                OnPropertyChanged(nameof(EmployeeRole));
            }
        }
        public string EmployeePassword
        {
            get => _employeePassword;
            set
            {
                _employeePassword = value;
                OnPropertyChanged(nameof(EmployeePassword));
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
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
        public Add_Employee_View_Model()
        {
            _firebaseService = new FirebaseService();
            SaveCommand = new RelayCommand<Window>(async (window) => await SaveEmployee(window));
            CancelCommand = new RelayCommand<Window>(window => CancelClick(window));
        }
        private async Task SaveEmployee(Window window)
        {
            Employee newEmployee = null;

            try
            {
                newEmployee = new Employee
                {
                    EmployeeName = EmployeeName,
                    EmployeeSex = EmployeeSex,
                    EmployeeRole = EmployeeRole,
                    EmployeePassword = EmployeePassword,
                    EmployeePhone = Phone,
                    EmployeeEmail = Email,
                    EmployeeBirth = BirthDate.Value,
                    Date_Start = StartDate.Value
                };
            }
            catch (Exception e)
            {
            }
            try
            {
                await _firebaseService.AddDataAsync<Employee>("Employees", newEmployee);
                MessageBox.Show("Thêm nhân viên thành công");
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
    }
}

