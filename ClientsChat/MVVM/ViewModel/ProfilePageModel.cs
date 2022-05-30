using ClientsChat.Core;
using ClientsChat.SpecialUse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsChat.MVVM.ViewModel
{
    //Модель для окна с профилем
    class ProfilePageModel : ObservableObject
    {
        //Поле для имени менеджера
        public string ManagerName { get; set; }
        //Поле для аватара менеджера
        public string ManagerIcon { get; set; }
        //Поле для отображения опыта
        public string Experience { get; set; }
        //Поле для числа выполненных вопросов
        public string CountCompleteQuestions { get; set; }
        //Поле для должности
        public string Grade { get; set; }

        public ProfilePageModel()
        {
            //Получение менеджера для работы с изменением данных
            Managers manager = ClientChatEntities.GetContext().Managers.Where(x => x.Id == ClientManager.Id).First();
            //Запись ФИО менеджера
            ManagerName = manager.FIO;
            //Если есть аватар, то устанавливаем его
            if(ClientChatEntities.GetContext().Managers.Where(x => x.Id == ClientManager.Id).First().Image != null)
            {
                ManagerIcon = ClientChatEntities.GetContext().Managers.Where(x => x.Id == ClientManager.Id).First().Image;
            }
            //Установка остальных значений
            Experience = ClientChatEntities.GetContext().Managers.Where(x => x.Id == ClientManager.Id).First().Experiance;
            CountCompleteQuestions = ClientChatEntities.GetContext().Managers.Where(x => x.Id == ClientManager.Id).First().CountQuestions.ToString();
            Grade = ClientChatEntities.GetContext().Managers.Where(x => x.Id == ClientManager.Id).First().Grade.ToString();
        }
    }
}
