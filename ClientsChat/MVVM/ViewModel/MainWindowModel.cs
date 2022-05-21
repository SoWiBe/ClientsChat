using ClientsChat.SpecialUse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsChat.MVVM.ViewModel
{
    class MainWindowModel
    {
        public string ManagerIcon { get; set; }
        public MainWindowModel()
        {
            if (ClientManager.Icon != null)
                ManagerIcon = ClientManager.Icon;
        }
    }
}
