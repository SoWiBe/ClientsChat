using ClientsChat.Core;
using ClientsChat.SpecialUse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

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

        private string url = "http://dimasbarbadoss-001-site1.itempurl.com/api/managers/" + ClientManager.Id;
        private string response = "";
        public ProfilePageModel()
        {
            using (var webClient = new WebClient())
            {
                webClient.Encoding = System.Text.Encoding.UTF8;
                webClient.QueryString.Get("id");
                response = webClient.DownloadString(url);
            }


            //Получение менеджера для работы с изменением данных
            JsonElement manager = ParseResponse(response);
            //Запись ФИО менеджера
            ManagerName = manager.GetProperty("fio").ToString();
            //Если есть аватар, то устанавливаем его
            if (manager.GetProperty("image").ToString() != null)
            {
                MessageBox.Show(manager.GetProperty("image").ToString());
                ManagerIcon = manager.GetProperty("image").ToString();
            }
            else
            {
                ManagerIcon = "E:/C#/ClientsChat/ClientsChat/Icons/Ellipse 64.png";
            }
            //Установка остальных значений
            Experience = manager.GetProperty("experiance").ToString();
            CountCompleteQuestions = manager.GetProperty("countQuestions").ToString();
            Grade = manager.GetProperty("grade").ToString();
        }

        private JsonElement ParseResponse(string response)
        {
            JsonDocument doc = JsonDocument.Parse(response);
            JsonElement root = doc.RootElement;
            return root;
        }
    }
}
