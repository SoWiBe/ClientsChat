using ClientsChat.SpecialUse;
using System;
using System.Windows;
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
            if (ClientManager.Icon != "")
            {
                MessageBox.Show(ClientManager.Icon);
                ManagerIcon = ClientManager.Icon;
            }
            else
            {
                ManagerIcon = "E:/C#/ClientsChat/ClientsChat/Icons/Ellipse 41.png";
            }
        }
    }
}
