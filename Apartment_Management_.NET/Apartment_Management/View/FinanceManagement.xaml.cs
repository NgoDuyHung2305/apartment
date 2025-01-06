using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Apartment_Management.View
{
	/// <summary>
	/// Interaction logic for FinanceManagement.xaml
	/// </summary>
	public partial class FinanceManagement : UserControl
	{
		public FinanceManagement()
		{
			InitializeComponent();
		}

		private void btn_noti_Click(object sender, RoutedEventArgs e)
		{

        }

		private void btn_acc_Click(object sender, RoutedEventArgs e)
		{
			(Window.GetWindow(this) as MainWindow)?.NavigateToAccount();
		}

		private void btn_newreceipt_today_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btn_unpaid_receipt_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
