using ClientsChat.Core;
using ClientsChat.SpecialUse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
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
        public QuestionPageModel()
        {
            //Елси нет активных вопросов, то отобразить соответствующую информацию
            if (ClientChatEntities.GetContext().Question.Where(x => x.IdManager == ClientManager.Id && x.IdStatus == 1).ToList().Count == 0)
            {
                SomeText = "У вас пока нет новых вопросов...";
                return;
            }
            //Выделение памяти под вопросы
            Questions = new ObservableCollection<Question>();
            //Запись в коллекцию
            foreach (var question in ClientChatEntities.GetContext().Question.Where(x => x.IdManager == ClientManager.Id && x.IdStatus == 1).ToList())
            {
                Questions.Add(question);
            }
            //Установка метода для команды
            OpenChatPage = new RelayCommand(o => StartQuestion(SelectedQuestion.IdClient), o => SelectedQuestion != null);

        }
        //Метод для открытия вопроса и перехода в чат, для его решения
        private void StartQuestion(object sender)
        {
            Question question = SelectedQuestion;
            question.IdStatus = 2;
            ClientChatEntities.GetContext().SaveChanges();
            MessageBox.Show("Вопрос по теме: " + question.Info + " был открыт!", "Вопрос открыт!", MessageBoxButton.OK);
            FrameManager.MainFrame.Navigate(new ChatPage(ClientManager.Name));
        }
    }
}
