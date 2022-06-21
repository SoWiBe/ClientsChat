using ClientsChat.Core;
using ClientsChat.MVVM.Model;
using ClientsChat.SpecialUse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientsChat.MVVM.ViewModel
{
    //Модель для отображения уведомления
    class NotificationsModel : ObservableObject
    {
        //Коллекция для хранения вопросов
        private string url = "http://dimasbarbadoss-001-site1.itempurl.com/api/directions";
        private string response = "";
        public ObservableCollection<Question> Questions { get; set; }
        //Поле для названия страницы
        public string SomeText { get; set; }
        public NotificationsModel()
        {
            using (var webClient = new WebClient())
            {
                webClient.Encoding = System.Text.Encoding.UTF8;
                webClient.QueryString.Get("id");
                response = webClient.DownloadString(url);
            }

            JsonElement questionsJson = ParseResponse(response);
            Questions = new ObservableCollection<Question>();
            //Заполнение коллекции данными
            for (int i = 0; i < questionsJson.GetArrayLength(); i++)
            {
                Questions.Add(new Question
                {
                    Id = questionsJson[i].GetProperty("id").ToString(),
                    Name = questionsJson[i].GetProperty("name").ToString(),
                });
            }
        }
        private JsonElement ParseResponse(string response)
        {
            JsonDocument doc = JsonDocument.Parse(response);
            JsonElement root = doc.RootElement;
            return root;
        }
    }
}
