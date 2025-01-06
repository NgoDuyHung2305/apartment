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
	/// Interaction logic for AddContract.xaml
	/// </summary>
	public partial class AddContract : Window
	{
		public AddContract()
		{
			InitializeComponent();
			DataContext= new ViewModel.AddContract_View_Model();
		}
	}
}
