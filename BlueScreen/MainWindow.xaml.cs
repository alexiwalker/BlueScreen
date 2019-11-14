using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BlueScreen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Wrapper.Background=Brushes.LightSkyBlue;
            
            for(int i = 0;i<1000;i++){
                TextBlock t = new TextBlock();
                t.Width = 25;
                t.Height = 25;
                t.Background = Brushes.Green;
                t.Margin = new Thickness(10,10,10,10);
                Wrapper.Children.Add(t);
            }

        }
    }
}