using System.Windows;
using DTS.Framework.Designer.ViewModels;

namespace DTS.Framework.Designer
{
    public partial class MainWindow : Window
    {
        public MainViewModel MainViewModel { get; set; }

        public MainWindow(MainViewModel mainViewModel)
        {
            DataContext = MainViewModel = mainViewModel;

            InitializeComponent();
        }
    }
}
