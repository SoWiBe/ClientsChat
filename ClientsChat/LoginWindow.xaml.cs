
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

        public LoginWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            ClientManager.Login = Login;
            ClientManager.Password = Password;
            Users user;
            if(string.IsNullOrWhiteSpace(Login))
            {
                MessageBox.Show("Введите логин!");
                CustomMessageBox.OpenBox("Введите логин!");
                return;
            }
            else if (string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Введите пароль!");
                return;
            }
            else if(ClientChatEntities.GetContext().Users.Where(x => x.Login == Login && x.Password == Password).Count() == 0)
            {
                MessageBox.Show("Такого пользователя нет!");
                return;
            }
            else
            {
                user = ClientChatEntities.GetContext().Users.Where(x => x.Login == Login && x.Password == Password).First();

                FrameManager.BorderMenu.Visibility = Visibility.Visible;
                FrameManager.MainFrame.Navigate(new ChatPage(ClientChatEntities.GetContext().Managers.Where(x => x.IdUser == user.Id).First().FIO));
                FrameManager.RegistrFrame.Navigate(null);
            }



            //new MainWindow(user).Show();
            //this.Close();
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FrameManager.RegistrFrame.Navigate(new SignUpPage());
        }
    }
}
