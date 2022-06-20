using ClientsChat.SpecialUse;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
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

        private string url = "http://dimasbarbadoss-001-site1.itempurl.com/api/managers/add";
        public SignUpPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(ValidateInfo())
            {
                using (var webClient = new WebClient())
                {
                    // Создаём коллекцию параметров
                    var pars = new NameValueCollection();

                    // Добавляем необходимые параметры в виде пар ключ, значение
                    pars["fio"] = SecondName + " " + FirstName + " " + LastName;
                    pars["image"] = "";
                    pars["grade"] = "";
                    pars["login"] = Login;
                    pars["password"] = Password;
                    pars["experiance"] = "";


                    // Посылаем параметры на сервер
                    // Может быть ответ в виде массива байт
                    var response = webClient.UploadValues(url, "POST", pars);
                }

                FrameManager.RegistrFrame.Navigate(new LoginWindow());
            }
        }

        private bool ValidateInfo()
        {
            if(string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(passwordBox.Password) || string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(SecondName)
                || string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(passwordBoxRepeat.Password))
            {
                MessageBox.Show("Не все поля заполнены!");
                return false;
            }
            else if(passwordBox.Password != passwordBoxRepeat.Password)
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

        private void passwordBoxRepeat_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passwordBoxRepeat.Password.Length == 0)
                txtPassword.Visibility = Visibility.Visible;
            else
                txtPassword.Visibility = Visibility.Hidden;
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
