using ClientsChat.SpecialUse;
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

namespace ClientsChat
{
    /// <summary>
    /// Логика взаимодействия для LeftPanelPage.xaml
    /// </summary>
    public partial class LeftPanelPage : Page
    {
        public LeftPanelPage()
        {
            InitializeComponent();
        }

        private void stackExit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FrameManager.RegistrFrame.Navigate(new LoginWindow());
            FrameManager.MainFrame.Navigate(null);
        }

        private void borderQuestion_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new QuestionPage());
        }

        private void borderChat_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new ChatPage(ClientManager.Name));
        }

        private void stackProfile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new ProfilePage());
        }

        private void stackNotifications_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new NotificationsPage());
        }
    }
}
