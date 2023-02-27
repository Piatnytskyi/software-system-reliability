using Reliability.Desktop.ViewModels;
using System.Windows;

namespace Reliability.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ReliabilityViewModel();
        }
    }
}
