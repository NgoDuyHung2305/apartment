using Apartment_Management.Helper;
using Apartment_Management.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Apartment_Management.ViewModel
{
    internal class MainWindowViewModel : Base_View_Model
	{
		private object _currentView;

		// Thuộc tính để binding với ContentControl
		public object CurrentView
		{
			get => _currentView;
			set
			{
				if (_currentView != value)
				{
					_currentView = value;
					OnPropertyChanged(nameof(CurrentView));
				}
			}
		}
        private bool _isHomeActive;
        public bool IsHomeActive
        {
            get => _isHomeActive;
			set {
                _isHomeActive = value;
			OnPropertyChanged(nameof(IsHomeActive));
			}
		}

        private bool _isBlockManagementActive;
        public bool IsBlockManagementActive
        {
            get => _isBlockManagementActive;
            set
            {
                _isBlockManagementActive = value;
                OnPropertyChanged(nameof(IsBlockManagementActive));
            }
        }

        private bool _isDwellerManagementActive;
        public bool IsDwellerManagementActive
        {
            get => _isDwellerManagementActive;
            set
            {
                _isDwellerManagementActive = value;
                OnPropertyChanged(nameof(IsDwellerManagementActive));
            }
        }

        private bool _isEmployeeManagementActive;
        public bool IsEmployeeManagementActive
        {
            get => _isEmployeeManagementActive;
            set
            {
                _isEmployeeManagementActive = value;
                OnPropertyChanged(nameof(IsEmployeeManagementActive));
            }
        }

        private bool _isOrderManagementActive;
        public bool IsOrderManagementActive
        {
            get => _isOrderManagementActive;
            set
            {
                _isOrderManagementActive = value;
                OnPropertyChanged(nameof(IsOrderManagementActive));
            }
        }

        private bool _isReceiptManagementActive;
        public bool IsReceiptManagementActive
        {
            get => _isReceiptManagementActive;
            set
            {
                _isReceiptManagementActive = value;
                OnPropertyChanged(nameof(IsReceiptManagementActive));
            }
        }

        private bool _isContractManagementActive;
        public bool IsContractManagementActive
        {
            get => _isContractManagementActive;
            set
            {
                _isContractManagementActive = value;
                OnPropertyChanged(nameof(IsContractManagementActive));
            }
        }

        private bool _isFinanceManagementActive;
        public bool IsFinanceManagementActive
        {
            get => _isFinanceManagementActive;
            set
            {
                _isFinanceManagementActive = value;
                OnPropertyChanged(nameof(IsFinanceManagementActive));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
		public MainWindowViewModel()
		{
			CurrentView = new Home();
			ShowContractViewCommand = new RelayCommand(ShowContractView);
			ShowDwellerViewCommand = new RelayCommand(ShowDwellerView);
			ShowEmployeeViewCommand = new RelayCommand(ShowEmployeeView);
			ShowHomeViewCommand = new RelayCommand(ShowHomeView);
			ShowOrderViewCommand = new RelayCommand(ShowOrderView);
			ShowReceiptViewCommand = new RelayCommand(ShowReceiptView);
			ShowBlockViewCommand = new RelayCommand(ShowBlockView);
            ShowFinanceViewCommand = new RelayCommand(ShowFinanceView);
            QuitCommand = new RelayCommand(Quit);
		}

		private void Quit(object obj)
		{
			// Hiển thị hộp thoại xác nhận
			MessageBoxResult result = MessageBox.Show(
				"Do you want to quit the application?",
				"Confirm to quit",
				MessageBoxButton.YesNo,
				MessageBoxImage.Question
			);

			// Nếu chọn Yes, thoát ứng dụng
			if (result == MessageBoxResult.Yes)
			{
				Application.Current.Shutdown();
			}
		}

		private void ShowReceiptView(object obj)
		{
            ResetActiveStates();
            CurrentView = new ReceiptManagement() { DataContext = new Receipt_View_Model(this) };
            IsReceiptManagementActive = true;
		}

		private void ShowOrderView(object obj)
		{
            ResetActiveStates();
            CurrentView = new OrderManagement(){ DataContext = new Order_View_Model(this) };
            IsOrderManagementActive = true;
		}

		private void ShowHomeView(object obj)
        {
            ResetActiveStates();
            CurrentView = new Home();
            IsHomeActive = true;
		}

		private void ShowEmployeeView(object obj)
        {
            ResetActiveStates();
            CurrentView = new EmployeeManagement() { DataContext=new Employee_View_Model(this) };
            IsEmployeeManagementActive = true;
		}

		private void ShowDwellerView(object obj)
        {
            ResetActiveStates();
            CurrentView = new DwellerManagement() { DataContext =new Dweller_View_Model(this)};
            IsDwellerManagementActive = true;
		}

		private void ShowBlockView(object obj)
		{
            ResetActiveStates();
            CurrentView = new BlockManagement()
			{
				DataContext = new Block_Management_View_Model(this)
			};
            IsBlockManagementActive = true; 
		}

		private void ShowContractView(object obj)
        {
            ResetActiveStates();
            CurrentView = new ContractManagement(){ DataContext = new Contract_View_Model(this)};
            IsContractManagementActive = true;
		}
        private void ShowFinanceView(object obj)
        {
            ResetActiveStates();
            CurrentView = new FinanceManagement();
            IsFinanceManagementActive = true;
        }

        public ICommand ShowContractViewCommand { get; }
		public ICommand ShowDwellerViewCommand { get; }
		public ICommand ShowEmployeeViewCommand { get; }
		public ICommand ShowHomeViewCommand { get; }
		public ICommand ShowOrderViewCommand { get; }
		public ICommand ShowReceiptViewCommand { get; }
		public ICommand ShowBlockViewCommand { get; }
        public ICommand ShowFinanceViewCommand { get; }
        public ICommand QuitCommand { get; }
        private void ResetActiveStates()
        {
            IsHomeActive = false;
            IsBlockManagementActive = false;
            IsDwellerManagementActive = false;
            IsEmployeeManagementActive = false;
            IsOrderManagementActive = false;
            IsReceiptManagementActive = false;
            IsContractManagementActive = false;
            IsFinanceManagementActive = false;
        }

    }
}
