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
using IService.ViewModels.Import;

namespace IService.Views.Import
{
    /// <summary>
    /// Interaction logic for MeteorologyView.xaml
    /// </summary>
    public partial class MeteorologyView : UserControl
    {
        public MeteorologyView()
        {
            InitializeComponent();
            DataContext = new MeteorologicalViewModel();
        }
    }
}
