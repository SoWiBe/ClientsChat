
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
        private PersonModel User;
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
            }
            else if (string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Введите пароль!");
            }
            else if(ClientChatEntities.GetContext().Users.Where(x => x.Login == Login && x.Password == Password) == null)
            {
                MessageBox.Show("Такого юзера нет!");
            }
            else
            {
                MessageBox.Show(ClientChatEntities.GetContext().Users.Where(x => x.Login == Login && x.Password == Password).ToString());
            }
                
            //user = ClientChatEntities.GetContext().Users.Where(x => x.Login == Login && x.Password == Password).First();
           
            //new MainWindow(user).Show();
            //this.Close();
        }
    }
}
