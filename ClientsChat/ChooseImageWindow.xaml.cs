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
using System.Windows.Shapes;

namespace ClientsChat
{
    /// <summary>
    /// Логика взаимодействия для ChooseImageWindow.xaml
    /// </summary>
    public partial class ChooseImageWindow : Window
    {
        public ChooseImageWindow()
        {
            InitializeComponent();
        }

        private void imgOne_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Managers manager = ClientChatEntities.GetContext().Managers.Where(x => x.Id == ClientManager.Id).First();
            manager.Image = ((Image)sender).Source.ToString();
            ClientManager.Icon = manager.Image;
            MessageBox.Show(manager.FIO.ToString());
            //ClientChatEntities.GetContext().Managers.Add(manager);
            ClientChatEntities.GetContext().SaveChanges();

            FrameManager.MainFrame.Navigate(new ProfilePage());
            FrameManager.LeftPanel.Navigate(new LeftPanelPage());
        }
    }
}
