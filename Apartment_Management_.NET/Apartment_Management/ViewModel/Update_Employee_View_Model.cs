using Apartment_Management.Model;
using Apartment_Management.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Apartment_Management.ViewModel
{
    internal class Update_Employee_View_Model:Base_View_Model
    {
        private readonly FirebaseService _firebaseService;
        public Employee Employee;
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
        public ICommand DeleteCommand { get; set; }

        public ICommand CancelCommand { get; set; }
        public Update_Employee_View_Model()
        {
            
        }
        public Update_Employee_View_Model(Employee employee)
        {
            _firebaseService = new FirebaseService();
            Employee = new Employee(employee);
            SaveCommand = new RelayCommand<Window>(async (window) => await SaveEmployee(window));
            DeleteCommand = new RelayCommand<Window>(async (window) => await DeleteEmployee(window));
            CancelCommand = new RelayCommand<Window>(window => CancelClick(window));
            LoadEmployee(Employee);
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
                await _firebaseService.UpdateDataAsync<Employee>("Employees",Employee.EmployeeID , newEmployee);
                MessageBox.Show("Cập nhật nhân viên thành công");
                window?.Close();
            }
            catch (Exception e)
            {
            }
            window?.Close();
        }
        private async Task DeleteEmployee(Window window)
        {
            var result = MessageBox.Show(
        "Bạn muốn xóa nhân viên này?",
        "Confirm Delete",
        MessageBoxButton.YesNo,
        MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Người dùng chọn Yes
                await _firebaseService.DeleteDataAsync("Employees", Employee.EmployeeID);
                MessageBox.Show("Xóa nhân viên thành công.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                window?.Close();
            }
            else
            {
                // Người dùng chọn No
            }

        }
        private async void LoadEmployee(Employee employee)
        {

            EmployeeName = employee.EmployeeName;
            EmployeeRole = employee.EmployeeRole;
            EmployeePassword = employee.EmployeePassword;
            EmployeeSex = employee.EmployeeSex;
            Phone = employee.EmployeePhone;
            Email = employee.EmployeeEmail;
            BirthDate = employee.EmployeeBirth;
            StartDate = employee.Date_Start;
        }
        public void CancelClick(Window window)
        {
            window?.Close();
        }
    }
}

