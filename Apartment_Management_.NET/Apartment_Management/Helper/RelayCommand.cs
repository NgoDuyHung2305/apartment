using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Apartment_Management.Helper
{
	public class RelayCommand:ICommand
	{
		private readonly Action<object> _execute;//Hàm thực thi
		private readonly Predicate<object> _canExecute;//Hàm kiểm tra điều kiện thực thi

        private readonly Action _execute2;
        private readonly Func<bool> _canExecute2;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute=null)
		{
			_execute = execute?? throw new ArgumentNullException(nameof(execute));
			_canExecute = canExecute;
		}


		public bool CanExecute(object parameter)
		{
			return _canExecute == null || _canExecute(parameter);
		}

		public void Execute(object parameter)
		{
			_execute(parameter);
		}

		public event EventHandler CanExecuteChanged
		{
			add 
			{ 
				CommandManager.RequerySuggested += value; 
			}
			remove
			{
				CommandManager.RequerySuggested -= value;
			}
		}
        public event EventHandler CanExecute2Changed;

        public bool CanExecute2(object parameter)
        {
            return _canExecute2 == null || _canExecute2();
        }

        public void Execute2(object parameter)
        {
            _execute2();
        }

        public void RaiseCanExecute2Changed()
        {
            CanExecute2Changed?.Invoke(this, EventArgs.Empty);
        }
    
}

	public class RelayCommand<T> : ICommand
	{
		private readonly Action<T> _execute;
		private readonly Predicate<T> _canExecute;

		public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
		{
			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter) => _canExecute == null || _canExecute((T)parameter);

		public void Execute(object parameter) => _execute((T)parameter);

		public event EventHandler CanExecuteChanged
		{
			add => CommandManager.RequerySuggested += value;
			remove => CommandManager.RequerySuggested -= value;
		}
	}
}
