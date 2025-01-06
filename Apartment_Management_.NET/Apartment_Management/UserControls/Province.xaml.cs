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
    /// Interaction logic for Province.xaml
    /// </summary>
    public partial class Province : UserControl
    {
        public Province()
        {
            InitializeComponent();
        }

        public string ProvinceName
        {
            get { return (string)GetValue(ProvinceNameProperty); }
            set { SetValue(ProvinceNameProperty, value); }
        }

        public static readonly DependencyProperty ProvinceNameProperty
            = DependencyProperty.Register("ProvinceName", typeof(string), typeof(Province));


        public string Population
        {
            get { return (string)GetValue(PopulationProperty); }
            set { SetValue(PopulationProperty, value); }
        }

        public static readonly DependencyProperty PopulationProperty
            = DependencyProperty.Register("Population", typeof(string), typeof(Province));

        public bool IsLevelUp
        {
            get { return (bool)GetValue(IsLevelUpProperty); }
            set { SetValue(IsLevelUpProperty, value); }
        }

        public static readonly DependencyProperty IsLevelUpProperty
            = DependencyProperty.Register("IsLevelUp", typeof(bool), typeof(Province));


        public string Rank
        {
            get { return (string)GetValue(RankProperty); }
            set { SetValue(RankProperty, value); }
        }

        public static readonly DependencyProperty RankProperty
            = DependencyProperty.Register("Rank", typeof(string), typeof(Province));
    }
}
