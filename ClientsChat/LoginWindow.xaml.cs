
using System;
using System.Collections.Generic;
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
using ClientsChat.MVVM.Model;
using ClientsChat.SpecialUse;


namespace ClientsChat
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Page
    {
        public string Login { get; set; }
        public string Password { get; set; }


        private List<string> clients = new List<string>();
        private string response = "";
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = this;

            string url = "http://dimasbarbadoss-001-site1.itempurl.com/api/managers";

            using (var webClient = new WebClient())
            {
                webClient.Encoding = System.Text.Encoding.UTF8;
                webClient.QueryString.Get("id");
                response = webClient.DownloadString(url);
            }
            
            
        }
        private JsonElement ParseResponse(string response)
        {
            JsonDocument doc = JsonDocument.Parse(response);
            JsonElement root = doc.RootElement;
            return root;
        }
        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            ClientManager.Login = Login;
            ClientManager.Password = passwordBox.Password;

            if (string.IsNullOrWhiteSpace(Login))
            {
                MessageBox.Show("Введите логин!");
                return;
            }
            else if (string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                MessageBox.Show("Введите пароль!");
                return;
            }
            else
            {

                JsonElement clientsJson = ParseResponse(response);
                JsonElement client;
                for (int i = 0; i < clientsJson.GetArrayLength(); i++)
                {
                    if (Login.Equals(clientsJson[i].GetProperty("login").ToString()) && passwordBox.Password.Equals(clientsJson[i].GetProperty("password").ToString()))
                    {
                        client = clientsJson[i];
                        ClientManager.Name = client.GetProperty("fio").ToString();
                        ClientManager.Id = Convert.ToInt32(client.GetProperty("id").ToString());
                        ClientManager.Icon = client.GetProperty("image").ToString();
                        FrameManager.LeftPanel.Navigate(new LeftPanelPage());
                        FrameManager.MainFrame.Navigate(new ChatPage(ClientManager.Name));
                        FrameManager.RegistrFrame.Navigate(null);
                        return;
                    }
                }
                MessageBox.Show("Такой пользователя нет!", "Не найден", MessageBoxButton.OK);
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FrameManager.RegistrFrame.Navigate(new SignUpPage());
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password.Length == 0)
                txtPasswordOne.Visibility = Visibility.Visible;
            else
                txtPasswordOne.Visibility = Visibility.Hidden;
        }
    }
}
