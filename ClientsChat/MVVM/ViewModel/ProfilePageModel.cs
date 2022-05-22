using ClientsChat.Core;
using ClientsChat.SpecialUse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsChat.MVVM.ViewModel
{
    class ProfilePageModel : ObservableObject
    {
        public string ManagerName { get; set; }
        public string ManagerIcon { get; set; }
        public string Experience { get; set; }
        public string CountCompleteQuestions { get; set; }
        public string Grade { get; set; }

        public ProfilePageModel()
        {
            Managers manager = ClientChatEntities.GetContext().Managers.Where(x => x.Id == ClientManager.Id).First();
            ManagerName = manager.FIO;

            if(ClientChatEntities.GetContext().Managers.Where(x => x.Id == ClientManager.Id).First().Image != null)
            {
                ManagerIcon = ClientChatEntities.GetContext().Managers.Where(x => x.Id == ClientManager.Id).First().Image;
            }

            Experience = ClientChatEntities.GetContext().Managers.Where(x => x.Id == ClientManager.Id).First().Experiance;
            CountCompleteQuestions = ClientChatEntities.GetContext().Managers.Where(x => x.Id == ClientManager.Id).First().CountQuestions.ToString();
            Grade = ClientChatEntities.GetContext().Managers.Where(x => x.Id == ClientManager.Id).First().Grade.ToString();
        }
    }
}
