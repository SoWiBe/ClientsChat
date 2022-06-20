using ClientsChat.Core;
using ClientsChat.MVVM.Model;
using ClientsChat.SpecialUse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ClientsChat.MVVM.ViewModel
{
    //Модель для отображения всех активных вопросов, со всем статусами
    class QuestionPageModel : ObservableObject
    {
        //Коллекция всех вопросов
        public ObservableCollection<Question> Questions { get; set; }
        //Название страницы
        public string SomeText { get; set; }
        //Команда на открытие чата с выбранным вопросом
        public ICommand OpenChatPage { get; set; }
        //Выбранный вопрос
        private Question _selectedQuestion;
        //Свойство на получение или установку выбранного вопроса
        public Question SelectedQuestion
        {
            get
            {
                return _selectedQuestion;
            }
            set
            {
                _selectedQuestion = value;
                OnPropertyChanged();
            }
        }
        private string url = "http://dimasbarbadoss-001-site1.itempurl.com/api/directions";
        private string response = "";
        public QuestionPageModel()
        {
            using (var webClient = new WebClient())
            {
                webClient.Encoding = System.Text.Encoding.UTF8;
                webClient.QueryString.Get("id");
                response = webClient.DownloadString(url);
            }

            JsonElement questionsJson = ParseResponse(response);
            Questions = new ObservableCollection<Question>();
            //Заполнение коллекции данными
            for (int i = 0; i < questionsJson.GetArrayLength(); i++)
            {
                Questions.Add(new Question
                {
                    Id = questionsJson[i].GetProperty("id").ToString(),
                    Name = questionsJson[i].GetProperty("name").ToString(),
                });
            }
            //Установка метода для команды
            OpenChatPage = new RelayCommand(o => StartQuestion(SelectedQuestion.Name), o => SelectedQuestion != null);

        }
        //Метод для открытия вопроса и перехода в чат, для его решения
        private void StartQuestion(object sender)
        {
            //Question question = SelectedQuestion;
            //question.IdStatus = 2;
            //ClientChatEntities.GetContext().SaveChanges();
            //MessageBox.Show("Вопрос по теме: " + question.Info + " был открыт!", "Вопрос открыт!", MessageBoxButton.OK);
            FrameManager.MainFrame.Navigate(new ChatPage(ClientManager.Name));
        }

        private JsonElement ParseResponse(string response)
        {
            JsonDocument doc = JsonDocument.Parse(response);
            JsonElement root = doc.RootElement;
            return root;
        }

    }
}
