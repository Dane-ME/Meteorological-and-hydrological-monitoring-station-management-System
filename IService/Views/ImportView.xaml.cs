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
using IService.Views.Import;

namespace IService.Views
{
    /// <summary>
    /// Interaction logic for ImportView.xaml
    /// </summary>
    public partial class ImportView : UserControl
    {
        public ImportView()
        {
            InitializeComponent();
            meteorology.Click += (s, e) =>
            {
                UIElement uIElement = new MeteorologyView();
                MainContent.Child = uIElement;
            };
            hydrological.Click += (s, e) => 
            {
                UIElement uIElement = new HydrologicalView();
                MainContent.Child = uIElement;
            };
            rainfall.Click += (s, e) => 
            {
                UIElement uiElement = new RainfallView();
                MainContent.Child = uiElement;
            };
            list.Click += (s, e) => 
            {
                UIElement uiElement = new ImportListView();
                MainContent.Child = uiElement;
            };
        }
    }
}
