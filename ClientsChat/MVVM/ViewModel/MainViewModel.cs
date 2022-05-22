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

namespace ClientsChat.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public ObservableCollection<UserModel> Users { get; set; }
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<Clients> Clients { get; set; }
        public ObservableCollection<Question> Questions { get; set; }
        public string Username { get; set; }

        /* Commands */
        public RelayCommand SendCommand { get; set; }
        public RelayCommand ConnectToServerCommand { get; set; }
        public RelayCommand SendMessageCommand { get; set; }
        public ICommand OpenChatPage { get; set; }
        public ICommand CloseQuestionCommand { get; set; }

        private Server _server;

        private Clients _selectedContact;

        public Clients SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                _selectedContact = value;
                OnPropertyChanged();
            }
        }

        private Question _selectedQuestion;

        public Question SelectedQuestion
        {
            get { return _selectedQuestion; }
            set
            {
                _selectedQuestion = value;
                OnPropertyChanged();
            }
        }

        public string Message { get; set; }
        
        public MainViewModel()
        {
            Users = new ObservableCollection<UserModel>();
            _server = new Server();
            _server.connectedEvent += UserConnected;
            _server.msgReceivedEvent += MessageReceived;
            _server.userDisconnectEvent += RemoveUser;

            ConnectToServerCommand = new RelayCommand(o => _server.ConnectToServer(Username), o => !string.IsNullOrEmpty(Username));
            SendMessageCommand = new RelayCommand(o => _server.SendMessageToServer(Message), o => !string.IsNullOrEmpty(Message));
            CloseQuestionCommand = new RelayCommand(o => CloseQuestion(), o => SelectedQuestion != null);
            

            Messages = new ObservableCollection<MessageModel>();
            Clients = new ObservableCollection<Clients>();
            Questions = new ObservableCollection<Question>();


            SetAndUpdateQuestions();

            if (Questions.Count != 0)
            {
                SelectedQuestion = Questions.First();

            }
        }

        private void SetAndUpdateQuestions()
        {
            Questions.Clear();
            var listQuestions = ClientChatEntities.GetContext().Question.Where(x => x.IdManager == ClientManager.Id && x.IdStatus == 2).ToList();
            for (int i = 0; i < listQuestions.Count(); i++)
            {
                Questions.Add(listQuestions.ElementAt(i));
            }
        }
        private void CloseQuestion()
        {
            Question question = SelectedQuestion;
            question.IdStatus = 3;
            Managers manager = ClientChatEntities.GetContext().Managers.Where(x => x.Id == ClientManager.Id).First();
            manager.CountQuestions++;
            ClientChatEntities.GetContext().SaveChanges();
            MessageBox.Show("Вопрос по теме: " + question.Info + " был закрыт!", "Вопрос открыт!", MessageBoxButton.OK);
            SetAndUpdateQuestions();
            
        }

        private void RemoveUser()
        {
            var uid = _server.PacketReader.ReadMessage();
            var user = Users.Where(x => x.ID == uid).FirstOrDefault();
            Application.Current.Dispatcher.Invoke(() => Users.Remove(user));
        }

        private void MessageReceived()
        {
            var msg = _server.PacketReader.ReadMessage();
            Application.Current.Dispatcher.Invoke(() => Messages.Add(new MessageModel
            {
                Message = msg,
                Time = DateTime.Now.ToString("dddd, dd MMMM yyyy h:mm tt"),
                IsNativeOrigin = true
            }));
        }

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
