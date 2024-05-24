using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using System.Data;
using System.Drawing;
using Color = Microsoft.Maui.Graphics.Color;

namespace DoAn.ViewModels
{
    public class HomeViewModel
    {
        public int NumberOfNews { get; set; }
        public View MainContent { get; set; }

        [Obsolete]
        public HomeViewModel() 
        {
            string StationName = "Cảng xi măng Hạ Long";
            string Location = "Thống Nhất, Hoành Bồ, Hạ Long, QN";
            string Information1 = "Mực nước biển (H): (Cm)";
            string Information2 = "Độ cao sóng (HM0): (m)";
            string Information3 = "Độ dài sóng (TM02): (m)";
            string Information4 = "Độ cao sóng (Hmax): (m)";
            string Information5 = "Ac: ĐANG ĐI BẢO DƯỠNG (v)";
            NumberOfNews = 5;
            Grid grid = CreateGrid(2, NumberOfNews);
            grid.BackgroundColor = Color.FromHex("#eaecf5");
            for (int row = 0; row < NumberOfNews; row++)
            {
                for (int col = 0; col < 2; col++)
                {
                    Grid gridchild = CreateGrid(1, 3);
                    List<View> views = new List<View>();
                    views.Add(CreateLabel($"{StationName}", "#eaecf5", 20));
                    views.Add(CreateLabel($"{Location}", "#eaecf5", 15));

                    gridchild.Add(CreateBorder(CreateStackLayout(views), "578fc8", 5), 0, 0);
                    Grid gridchild2 = CreateGrid(2, 1);
                    Grid gridchild3 = CreateGrid(1, 5);
                    gridchild3.Add(CreateLabel(Information1, "#578fc8", 15), 0, 0);
                    gridchild3.Add(CreateLabel(Information2, "#578fc8", 15), 0, 1);
                    gridchild3.Add(CreateLabel(Information3, "#578fc8", 15), 0, 2);
                    gridchild3.Add(CreateLabel(Information4, "#578fc8", 15), 0, 3);
                    gridchild3.Add(CreateLabel(Information5, "#578fc8", 15), 0, 4);

                    Image wave = new Image() { Source = "wave.png", WidthRequest = 80, HeightRequest = 80};
                    gridchild2.Add(wave, 1, 0);
                    gridchild2.Add(gridchild3, 0, 0);
                    gridchild2.Margin = new Thickness(5);

                    gridchild.Add(gridchild2, 0, 1);
                    gridchild.Add(CreateLabel("Thời gian: 10h 52 phút ngày 23/05/2024", "#000000", 15), 0, 2);

                    grid.Add(CreateBorder(gridchild, "ffffff", 5), col, row);
                }
            }

            var scrollview = new ScrollView();
            scrollview.Content = grid;
            MainContent = scrollview;

        }

        [Obsolete]
        public View CreateLabel(string content, string color, int size) 
        {
            return new Label() { 
                Text = $"{content}",
                TextColor = Color.FromHex($"{color}"),
                FontSize = size,
                Margin = new Thickness(5),
            };
        }
        public View CreateStackLayout(List<View> content)
        {
            var layout = new StackLayout() 
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.Center
                
            };

            foreach (var view in content)
            {
                layout.Children.Add(view);
            }

            return layout;
        }

        public Grid CreateGrid(int column, int row )
        {
            var grid = new Grid() { BackgroundColor = Colors.Transparent};
            for (int i = 0; i < column; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            for (int i = 0; i < row; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }
            
            return grid;
        }
        [Obsolete]
        public Border CreateBorder(View content, string brushcolor, Thickness margin) => new Border()
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(15) },
            Margin = margin,
            BackgroundColor = Color.FromHex($"#{brushcolor}"),
            StrokeThickness = 0,
            Content = content
        };

    }
}
