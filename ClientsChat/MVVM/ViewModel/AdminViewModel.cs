using ClientsChat.Core;
using ClientsChat.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace ClientsChat.MVVM.ViewModel
{
    class AdminViewModel : ObservableObject
    {
        public ObservableCollection<Manager> Managers { get; set; }
        public string Username { get; set; }

        private string urlQuestions = "http://dimasbarbadoss-001-site1.itempurl.com/api/managers/";
        //Команда на отправку
        public RelayCommand DeleteCommand { get; set; }

        private Manager _selectedManager;
        
        public Manager SelectedManager
        {
            get { return _selectedManager; }
            set
            {
                _selectedManager = value;
                OnPropertyChanged();
            }
        }

        public AdminViewModel()
        {
            Managers = new ObservableCollection<Manager>();

            DeleteCommand = new RelayCommand(o => DeleteManager());

            SetAndUpdateManagers();
        }

        private void SetAndUpdateManagers()
        {
            string response = "";
            Managers.Clear();

            using (var webClient = new WebClient())
            {
                webClient.Encoding = System.Text.Encoding.UTF8;
                webClient.QueryString.Get("id");
                response = webClient.DownloadString(urlQuestions);
            }

            JsonElement questionsJson = ParseResponse(response);
            for (int i = 0; i < questionsJson.GetArrayLength(); i++)
            {
                if (questionsJson[i].GetProperty("login").ToString().Equals("55maximus55"))
                    return;
                Managers.Add(new Manager
                {
                    Id = questionsJson[i].GetProperty("id").ToString(),
                    fio = questionsJson[i].GetProperty("fio").ToString(),
                    grade = questionsJson[i].GetProperty("grade").ToString(),
                    experiance = questionsJson[i].GetProperty("experiance").ToString(),
                    countQuestions = questionsJson[i].GetProperty("countQuestions").ToString()
                });
            }
        }

        private JsonElement ParseResponse(string response)
        {
            JsonDocument doc = JsonDocument.Parse(response);
            JsonElement root = doc.RootElement;
            return root;
        }

        private void DeleteManager()
        {
            using (var client = new WebClient())
            {
                client.UploadValues(urlQuestions + SelectedManager.Id, "DELETE", new NameValueCollection());

            }
            MessageBox.Show("Успешно удален " + SelectedManager.fio);
            SetAndUpdateManagers();
        }
    }
}
