using MahApps.Metro.IconPacks;
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
    /// Interaction logic for BlockEntityCard.xaml
    /// </summary>
    public partial class BlockEntityCard : UserControl
    {
      
        public BlockEntityCard()
        {
            InitializeComponent();
        }
        public PackIconMaterialKind Icon
        {
            get { return (PackIconMaterialKind)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("Icon", typeof(PackIconMaterialKind), typeof(BlockEntityCard));
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(BlockEntityCard));

        public int? Number1
        {
            get { return (int?)GetValue(Number1Property); }
            set { SetValue(Number1Property, value); }
        }

        public static readonly DependencyProperty Number1Property =
        DependencyProperty.Register("Number1", typeof(int?), typeof(BlockEntityCard));
        public int? Number2
        {
            get { return (int?)GetValue(Number2Property); }
            set { SetValue(Number2Property, value); }
        }
        public string Space
        {
            get { return (string)GetValue(SpaceProperty); }
            set { SetValue(SpaceProperty, value); }
        }

        public static readonly DependencyProperty SpaceProperty =
        DependencyProperty.Register("Space", typeof(string), typeof(BlockEntityCard));

        public static readonly DependencyProperty Number2Property =
        DependencyProperty.Register("Number2", typeof(int?), typeof(BlockEntityCard));
    }
    
}
