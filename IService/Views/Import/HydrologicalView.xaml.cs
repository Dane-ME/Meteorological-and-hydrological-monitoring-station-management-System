using IService.Models;
using IService.ViewModels.Import;
using System.Windows.Controls;

namespace IService.Views.Import
{
    /// <summary>
    /// Interaction logic for HydrologicalView.xaml
    /// </summary>
    public partial class HydrologicalView : UserControl
    {
        public HydrologicalView()
        {
            InitializeComponent();
            DataContext = new HydrologicalViewModel();
        }
    }
}
