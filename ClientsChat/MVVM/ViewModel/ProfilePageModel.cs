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
        public string Experience { get; set; }
        public string CountCompleteQuestions { get; set; }
        public string Grade { get; set; }

        public ProfilePageModel()
        {
            Managers manager = ClientChatEntities.GetContext().Managers.Where(x => x.Id == ClientManager.Id).First();
            ManagerName = manager.FIO;
            Experience = "8 лет";
            CountCompleteQuestions = "228";
            Grade = "Профессионал";
        }
    }
}
