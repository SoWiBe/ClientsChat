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
    //Модель для отображения уведомления
    class NotificationsModel : ObservableObject
    {
        //Коллекция для хранения вопросов
        public ObservableCollection<Question> Questions { get; set; }
        //Поле для названия страницы
        public string SomeText { get; set; }
        public NotificationsModel()
        {
            //проверка на кол-во уведомлений у менеджера
            if (ClientChatEntities.GetContext().Question.Where(x => x.IdManager == ClientManager.Id).ToList().Count == 0)
            {
                SomeText = "Уведомлений нет...";
                return;
            }
            //Выделение памяти под уведомления
            Questions = new ObservableCollection<Question>();
            //Заполнение коллекции данными
            foreach (var question in ClientChatEntities.GetContext().Question.Where(x => x.IdManager == ClientManager.Id).ToList())
            {
                Questions.Add(question);
            }
        }
    }
}
