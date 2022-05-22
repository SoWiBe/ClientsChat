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
    class QuestionPageModel : ObservableObject
    {
        public ObservableCollection<Question> Questions { get; set; }
        public string Title { get; set; }
        public string SomeText { get; set; }
        public ICommand OpenChatPage { get; set; }

        private Question _selectedQuestion;
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
            if (ClientChatEntities.GetContext().Question.Where(x => x.IdManager == ClientManager.Id && x.IdStatus == 1).ToList().Count == 0)
            {
                SomeText = "У вас пока нет новых вопросов...";
                return;
            }

            Questions = new ObservableCollection<Question>();
            foreach (var question in ClientChatEntities.GetContext().Question.Where(x => x.IdManager == ClientManager.Id && x.IdStatus == 1).ToList())
            {
                Questions.Add(question);
            }

            OpenChatPage = new RelayCommand(o => StartQuestion(SelectedQuestion.IdClient), o => SelectedQuestion != null);

        }

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
