using Apartment_Management.View;
using Apartment_Management.ViewModel;
using LiveCharts.Wpf.Charts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Apartment_Management
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			//MainContent.Content = new View.Home();
			//Home_View.IsActive = true;
			//Block_Managemen.IsActive = false;
			//Dweller_Managemen.IsActive = false;
			//Employee_Management.IsActive = false;
			//Order_Management.IsActive = false;
			//Receipt_Management.IsActive = false;
			//Contract_Management.IsActive = false;
			//Finance_Management.IsActive = false;
		}

		public void NavigateToAccount()
		{
			MainContent.Content = new View.Account();
		}

		private void Border_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				this.DragMove();

			}
		}

		private void Home_View_Click(object sender, RoutedEventArgs e)
		{
			MainContent.Content = new View.Home();
			Home_View.IsActive = true;
            Block_Managemen.IsActive = false;
			Dweller_Managemen.IsActive = false;
			Employee_Management.IsActive = false;
			Order_Management.IsActive = false;
			Receipt_Management.IsActive = false;
			Contract_Management.IsActive = false;
			//Finance_Management.IsActive = false;

		}

		private void Block_Managemen_Click(object sender, RoutedEventArgs e)
		{
			MainContent.Content = new View.BlockManagement()
			{
				DataContext = new Block_Management_View_Model(new MainWindowViewModel())
			};
			Home_View.IsActive = false;
			Block_Managemen.IsActive = true;
			Dweller_Managemen.IsActive = false;
			Employee_Management.IsActive = false;
			Order_Management.IsActive = false;
			Receipt_Management.IsActive = false;
			Contract_Management.IsActive = false;
			//Finance_Management.IsActive = false;
		}

		private void Dweller_Managemen_Click(object sender, RoutedEventArgs e)
		{
			MainContent.Content = new View.DwellerManagement();
			Home_View.IsActive = false;
            Block_Managemen.IsActive = false;
			Dweller_Managemen.IsActive = true;
			Employee_Management.IsActive = false;
			Order_Management.IsActive = false;
			Receipt_Management.IsActive = false;
			Contract_Management.IsActive = false;
			//Finance_Management.IsActive = false;
		}

		private void Employee_Management_Click(object sender, RoutedEventArgs e)
		{
			MainContent.Content = new View.EmployeeManagement();
			Home_View.IsActive = false;
            Block_Managemen.IsActive = false;
			Dweller_Managemen.IsActive = false;
			Employee_Management.IsActive = true;
			Order_Management.IsActive = false;
			Receipt_Management.IsActive = false;
			Contract_Management.IsActive = false;
			//Finance_Management.IsActive = false;
		}

		private void Order_Management_Click(object sender, RoutedEventArgs e)
		{
			MainContent.Content = new View.OrderManagement();
			Home_View.IsActive = false;
            Block_Managemen.IsActive = false;
			Dweller_Managemen.IsActive = false;
			Employee_Management.IsActive = false;
			Order_Management.IsActive = true;
			Receipt_Management.IsActive = false;
			Contract_Management.IsActive = false;
			//Finance_Management.IsActive = false;
		}

		private void Receipt_Management_Click(object sender, RoutedEventArgs e)
		{
			MainContent.Content = new View.ReceiptManagement();
			Home_View.IsActive = false;
            Block_Managemen.IsActive = false;
			Dweller_Managemen.IsActive = false;
			Employee_Management.IsActive = false;
			Order_Management.IsActive = false;
			Receipt_Management.IsActive = true;
			Contract_Management.IsActive = false;
			//Finance_Management.IsActive = false;
		}

		private void Contract_Management_Click(object sender, RoutedEventArgs e)
		{
			MainContent.Content = new View.ContractManagement();
			Home_View.IsActive = false;
            Block_Managemen.IsActive = false;
			Dweller_Managemen.IsActive = false;
			Employee_Management.IsActive = false;
			Order_Management.IsActive = false;
			Receipt_Management.IsActive = false;
			Contract_Management.IsActive = true;
			//Finance_Management.IsActive = false;
		}

		private void Finance_Management_Click(object sender, RoutedEventArgs e)
		{
			MainContent.Content = new View.FinanceManagement();
			Home_View.IsActive = false;
            Block_Managemen.IsActive = false;
			Dweller_Managemen.IsActive = false;
			Employee_Management.IsActive = false;
			Order_Management.IsActive = false;
			Receipt_Management.IsActive = false;
			Contract_Management.IsActive = false;
			//Finance_Management.IsActive = true;
		}

		private void menubutton_Click_1(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result = MessageBox.Show(
	   "Do you want to quit the application?",
	   "Confirm to quit",
	   MessageBoxButton.YesNo,
	   MessageBoxImage.Question
   );

			// Nếu chọn "Yes", đóng ứng dụng
			if (result == MessageBoxResult.Yes)
			{
				Application.Current.Shutdown();
			}
		}

		//private void menubutton_Click(object sender, RoutedEventArgs e)
		//{
		//	var button = sender as System.Windows.Controls.Button;
		//	if (button != null && button.Tag != null)
		//	{
		//		string viewName = button.Tag.ToString();
		//		switch (viewName)
		//		{
		//			case "Home":
		//				MainContent.Content = new View.Home();
		//				break;
		//			case "RoomManagementView":
		//				MainContent.Content = new View.RoomManagement();
		//				break;
		//			case "DwellerManagement":
		//				MainContent.Content = new View.DwellerManagement();
		//				break;
		//			case "EmployeeManagement":
		//				MainContent.Content = new View.EmployeeManagement();
		//				break;
		//			case "OrderManagement":
		//				MainContent.Content = new View.OrderManagement();
		//				break;
		//			case "ReceiptManagement":
		//				MainContent.Content = new View.ReceiptManagement();
		//				break;
		//			case "ContractManagement":
		//				MainContent.Content = new View.ContractManagement();
		//				break;
		//			case "FinanceManagement":
		//				MainContent.Content = new View.FinanceManagement();
		//				break;
		//			default:
		//				MainContent.Content = new View.ContractManagement();
		//				break;
		//		}
		//	}
		//}

	}
}
