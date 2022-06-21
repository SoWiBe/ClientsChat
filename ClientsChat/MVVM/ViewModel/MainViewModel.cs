using ClientsChat.Core;
using ClientsChat.MVVM.Model;
using ClientsChat.Net;
using ClientsChat.SpecialUse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Net;
using System.Text.Json;
using System.Collections.Specialized;

namespace ClientsChat.MVVM.ViewModel
{
    //Модель, которая описывает окно чата
    class MainViewModel : ObservableObject
    {
        //Объявления всех коллекций
        public ObservableCollection<UserModel> Users { get; set; }
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<Client> Clients { get; set; }
        public ObservableCollection<Question> Questions { get; set; }
        //Имя для входа в чат
        public string Username { get; set; }

        /* Commands */

        //Команда на отправку
        public RelayCommand SendCommand { get; set; }
        //Команда на соединение с сервером
        public RelayCommand ConnectToServerCommand { get; set; }
        //Команда на отправку сообщения
        public RelayCommand SendMessageCommand { get; set; }
        //Команда на открытие чата
        public ICommand OpenChatPage { get; set; }
        //Команда на закрытие вопроса
        public ICommand CloseQuestionCommand { get; set; }

        //Сервер
        private Server _server;
        //Выбранный контакт
        private Client _selectedContact;
        //Свойство, для получения или установления выбранного контакта
        public Client SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                _selectedContact = value;
                OnPropertyChanged();
            }
        }
        //Выбранный вопрос
        private Question _selectedQuestion;
        //Свойство, для получения или установления выбранного вопроса
        public Question SelectedQuestion
        {
            get { return _selectedQuestion; }
            set
            {
                _selectedQuestion = value;
                OnPropertyChanged();
            }
        }
        //Поле сообщения
        public string Message { get; set; }
        //Булевое поле для дальнейшей проверки на наличие сообщений
        public bool IsHasContent { get; set; }

        private string urlQuestions = "http://dimasbarbadoss-001-site1.itempurl.com/api/directions";
        private string urlClients = "http://dimasbarbadoss-001-site1.itempurl.com/api/clients";
        
        public MainViewModel()
        {
            Users = new ObservableCollection<UserModel>();
            _server = new Server();

            //Привязка событий
            _server.connectedEvent += UserConnected;
            _server.msgReceivedEvent += MessageReceived;
            _server.userDisconnectEvent += RemoveUser;

            //Установка комманд
            ConnectToServerCommand = new RelayCommand(o => PostMessage(Message), o => !string.IsNullOrEmpty(Username));
            SendMessageCommand = new RelayCommand(o => MessageReceived(), o => !string.IsNullOrEmpty(Message));
            CloseQuestionCommand = new RelayCommand(o => CloseQuestion(), o => SelectedQuestion != null);
            
            //Выделение памяти под коллекции
            Messages = new ObservableCollection<MessageModel>();
            Clients = new ObservableCollection<Client>();
            Questions = new ObservableCollection<Question>();

            //Вывод вопросов с определенным статусом
            SetAndUpdateQuestions();
            IsHasContent = true;
            if (Questions.Count() == 0)
            {
                IsHasContent = false;
            }
            //Установка первого вопроса, как выбранного
            if (Questions.Count != 0)
            {
                SelectedQuestion = Questions.First();
            }


        }

        private JsonElement ParseResponse(string response)
        {
            JsonDocument doc = JsonDocument.Parse(response);
            JsonElement root = doc.RootElement;
            return root;
        }
        private void SetAndUpdateQuestions()
        {
            string response = "";
            Questions.Clear();

            using (var webClient = new WebClient())
            {
                webClient.Encoding = System.Text.Encoding.UTF8;
                webClient.QueryString.Get("id");
                response = webClient.DownloadString(urlQuestions);
            }

            JsonElement questionsJson = ParseResponse(response);
            for (int i = 0; i < questionsJson.GetArrayLength(); i++)
            {
                Questions.Add(new Question
                { 
                    Id = questionsJson[i].GetProperty("id").ToString(),
                    Name = questionsJson[i].GetProperty("name").ToString(),
                });
            }
        }

        //private void SetAndUpdateClients()
        //{
        //    string response = "";
        //    Clients.Clear();

        //    using (var webClient = new WebClient())
        //    {
        //        webClient.Encoding = System.Text.Encoding.UTF8;
        //        webClient.QueryString.Get("id");
        //        response = webClient.DownloadString(urlQuestions);
        //    }

        //    JsonElement questionsJson = ParseResponse(response);
        //    for (int i = 0; i < questionsJson.GetArrayLength(); i++)
        //    {
        //        Clients.Add(new Question
        //        {
        //            Id = questionsJson[i].GetProperty("id").ToString(),
        //            Name = questionsJson[i].GetProperty("name").ToString(),
        //        });
        //    }
        //}

        private void PostMessage(string message)
        {

        }
        //Закрытие вопроса
        private void CloseQuestion()
        {

            //using (var webClient = new WebClient())
            //{
            //    // Создаём коллекцию параметров
            //    var pars = new NameValueCollection();

            //    // Добавляем необходимые параметры в виде пар ключ, значение
            //    pars.Add("image", ((Image)sender).Source.ToString());
            //    var response = webClient.UploadValues(url, pars);
            //}
            //Question question = SelectedQuestion;
            //question.IdStatus = 3;
            //Managers manager = ClientChatEntities.GetContext().Managers.Where(x => x.Id == ClientManager.Id).First();
            //manager.CountQuestions++;
            //ClientChatEntities.GetContext().SaveChanges();
            //MessageBox.Show("Вопрос по теме: " + question.Info + " был закрыт!", "Вопрос открыт!", MessageBoxButton.OK);
            //SetAndUpdateQuestions();
        }
        //Отсоединение пользователя с чата
        private void RemoveUser()
        {
            var uid = _server.PacketReader.ReadMessage();
            var user = Users.Where(x => x.ID == uid).FirstOrDefault();
            Application.Current.Dispatcher.Invoke(() => Users.Remove(user));
        }
        private void MessageReceived()
        {
            Messages.Add(new MessageModel
            {
                Message = Message,
                Time = DateTime.Now.ToString("dddd, dd MMMM yyyy h:mm tt"),
                IsNativeOrigin = true
            });
            Message = "";
        }
        //Присоединение пользователсся к чату
        private void UserConnected()
        {
            var user = new UserModel
            {
                Username = _server.PacketReader.ReadMessage(),
                ID = _server.PacketReader.ReadMessage()
            };

            if (!Users.Any(x => x.ID == user.ID))
            {
                Application.Current.Dispatcher.Invoke(() => Users.Add(user));
            }
        }
    }
}
