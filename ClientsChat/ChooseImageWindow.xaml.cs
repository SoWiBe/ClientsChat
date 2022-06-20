using ClientsChat.SpecialUse;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
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
        private string url = "http://dimasbarbadoss-001-site1.itempurl.com/api/managers";
        private string response = "";
        public ChooseImageWindow()
        {
            InitializeComponent();

        }

        private void imgOne_MouseDown(object sender, MouseButtonEventArgs e)
        {
            url += "/" + ClientManager.Id;

            using (var webClient = new WebClient())
            {
                // Создаём коллекцию параметров
                var pars = new NameValueCollection();

                // Добавляем необходимые параметры в виде пар ключ, значение
                pars.Add("image", ((Image)sender).Source.ToString());
                var response = webClient.UploadValues(url, pars);
            }
            ClientManager.Icon = ((Image)sender).Source.ToString();

            FrameManager.MainFrame.Navigate(new ProfilePage());
            FrameManager.LeftPanel.Navigate(new LeftPanelPage());
        }
    }
}
