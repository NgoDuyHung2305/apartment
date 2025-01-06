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
    /// Interaction logic for BlockCard.xaml
    /// </summary>
    public partial class BlockCard : UserControl
    {
      
        public BlockCard()
        {
            InitializeComponent();
        }
        public string BlockName
        {
            get { return (string)GetValue(BlockNameProperty); }
            set { SetValue(BlockNameProperty, value); }
        }

        public static readonly DependencyProperty BlockNameProperty =
        DependencyProperty.Register("BlockName", typeof(string), typeof(BlockCard));

        public int? BlockRoomAvailable
        {
            get { return (int?)GetValue(BlockRoomAvailableProperty); }
            set { SetValue(BlockRoomAvailableProperty, value); }
        }

        public static readonly DependencyProperty BlockRoomAvailableProperty =
        DependencyProperty.Register("BlockRoomAvailable", typeof(int?), typeof(BlockCard));
        public int? BlockRoom
        {
            get { return (int?)GetValue(BlockRoomProperty); }
            set { SetValue(BlockRoomProperty, value); }
        }
        public static readonly DependencyProperty BlockRoomProperty =
        DependencyProperty.Register("BlockRoom", typeof(int?), typeof(BlockCard));
    }

}
