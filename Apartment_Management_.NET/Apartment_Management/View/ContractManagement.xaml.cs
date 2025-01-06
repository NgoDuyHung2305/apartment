using Apartment_Management.ViewModel;
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
	/// Interaction logic for ContractManagement.xaml
	/// </summary>
	public partial class ContractManagement : UserControl
	{
		public ContractManagement()
		{
			InitializeComponent();
			DataContext = new Contract_View_Model();
		}
	}
}
