using ClientsChat.Core;
using ClientsChat.SpecialUse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsChat.MVVM.ViewModel
{
    class NotificationsModel : ObservableObject
    {
        public ObservableCollection<Question> Questions { get; set; }
        public string Title { get; set; }
        public string SomeText { get; set; }
        public NotificationsModel()
        {
            if (ClientChatEntities.GetContext().Question.Where(x => x.IdManager == ClientManager.Id).ToList().Count == 0)
            {
                SomeText = "Уведомлений нет...";
                return;
            }

            Questions = new ObservableCollection<Question>();
            foreach (var question in ClientChatEntities.GetContext().Question.Where(x => x.IdManager == ClientManager.Id).ToList())
            {
                Questions.Add(question);
            }
        }
    }
}
