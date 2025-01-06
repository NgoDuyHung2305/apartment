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

namespace Apartment_Management.UserControls
{
    /// <summary>
    /// Interaction logic for RoomCard.xaml
    /// </summary>
    public partial class RoomCard : UserControl
    {
        public RoomCard()
        {
            InitializeComponent();
        }
        public string RoomName
        {
            get { return (string)GetValue(RoomNameProperty); }
            set { SetValue(RoomNameProperty, value); }
        }
        public static readonly DependencyProperty RoomNameProperty =
        DependencyProperty.Register("RoomName", typeof(string), typeof(RoomCard));
        public string RoomStatus
        {
            get { return (string)GetValue(RoomStatusProperty); }
            set { SetValue(RoomStatusProperty, value); }
        }
        public static readonly DependencyProperty RoomStatusProperty =
        DependencyProperty.Register("RoomStatus", typeof(string), typeof(RoomCard));
        public double RoomPrice
        {
            get { return (double)GetValue(RoomPriceProperty); }
            set { SetValue(RoomPriceProperty, value); }
        }
        public static readonly DependencyProperty RoomPriceProperty =
        DependencyProperty.Register("RoomPrice", typeof(double), typeof(RoomCard));
    }
}
