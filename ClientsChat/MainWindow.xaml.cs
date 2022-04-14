using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ClientsChat
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public bool StatusHome { get; set; }
        public bool StatusChat { get; set; }
        public bool StatusBell { get; set; }
        public bool StatusSettings { get; set; }

        private bool[] _isMenuItemsClicks = new bool[] { false, false, false, false};
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuHomeClick(object sender, RoutedEventArgs e)
        {
            ResetClicks(0);
            ChangeClicks(1, StatusHome);
            OnPropertyChanged("StatusHome");
        }
        private void MenuChatClick(object sender, RoutedEventArgs e)
        {
            ResetClicks(1);
            ChangeClicks(1,  StatusChat);
            OnPropertyChanged("StatusChat");
        }
        private void MenuBellClick(object sender, RoutedEventArgs e)
        {
            ResetClicks(2);
            ChangeClicks(2, StatusBell);
            OnPropertyChanged("StatusBell");
        }
        private void MenuSettingsClick(object sender, RoutedEventArgs e)
        {
            ResetClicks(3);
            ChangeClicks(3,  StatusSettings);
            OnPropertyChanged("StatusSettings");
        }

        private void ChangeClicks(int position, bool status)
        {
            _isMenuItemsClicks[position] = !_isMenuItemsClicks[position];
            StatusChat = _isMenuItemsClicks[position];
        }

        public void ResetClicks(int position)
        {
            for (int i = 0; i < 4; i++)
            {
                if (i != position)
                {

                }
            }     
        }

        //Realization click to menu items
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        protected void OnPropertyChanged(string property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        //Move window by left button on the mouse
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else
                Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
