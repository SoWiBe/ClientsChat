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
    /// Логика взаимодействия для SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string PasswordRepeat { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }

        public SignUpPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(ValidateInfo())
            {
                Users user = new Users();
                user.Login = Login;
                user.Password = Password;

                Managers manager = new Managers();
                manager.FIO = SecondName + " " + FirstName + " " + LastName;
                manager.IdUser = user.Id;

                ClientChatEntities.GetContext().Users.Add(user);
                ClientChatEntities.GetContext().Managers.Add(manager);
                ClientChatEntities.GetContext().SaveChanges();

                FrameManager.RegistrFrame.Navigate(new LoginWindow());
            }
        }

        private bool ValidateInfo()
        {
            if(string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(SecondName)
                || string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(PasswordRepeat))
            {
                MessageBox.Show("Не все поля заполнены!");
                return false;
            }
            else if(PasswordRepeat != Password)
            {
                MessageBox.Show("Пароли не совпадают!");
                return false;
            }
            MessageBox.Show(Login);
            return true;
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FrameManager.RegistrFrame.Navigate(new LoginWindow());
        }
    }
}
