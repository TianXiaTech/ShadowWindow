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

namespace ShadowWindow
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

          
        }

        private void cbx_IsEnabled_Checked(object sender, RoutedEventArgs e)
        {
            WinAPI.DropShadow(this, new Margins() { cxLeftWidth = 5, cxRightWidth = 5, cyBottomHeight = 5, cyTopHeight = 5 });
        }

        private void cbx_IsEnabled_Unchecked(object sender, RoutedEventArgs e)
        {
            WinAPI.DropShadow(this, new Margins() { cxLeftWidth = 0, cxRightWidth = 0, cyBottomHeight = 0, cyTopHeight = 0 });
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
