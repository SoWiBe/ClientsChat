using ClientsChat.Core;
using ClientsChat.MVVM.Model;
using ClientsChat.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsChat.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<ContactModel> Contacts { get; set; }

        public string Username { get; set; }

        /* Commands */
        public RelayCommand SendCommand { get; set; }
        public RelayCommand ConnectToServerCommand { get; set; }

        private Server _server;

       
        private ContactModel _selectedContact;

        public ContactModel SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                _selectedContact = value;
                OnPropertyChanged();
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel()
        {
            _server = new Server();
            ConnectToServerCommand = new RelayCommand(o => _server.ConnectToServer(Username), o => !string.IsNullOrEmpty(Username));

            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();

            SendCommand = new RelayCommand(o =>
            {
                Messages.Add(new MessageModel
                {
                    Message = Message,
                    FirstMessage = false
                });

                Message = "";
            });

            Messages.Add(new MessageModel
            {
                Username = "Aleksey",
                UsernameColor = "Black",
                ImageSource = "E:/C#/ClientsChat/ClientsChat/Icons/photo.png",
                Message = "Test message for Aleksey",
                IsNativeOrigin = false,
                FirstMessage = true
            });

            for (int i = 0; i < 2; i++)
            {
                Messages.Add(new MessageModel
                {
                    Username = "Artem",
                    UsernameColor = "Orange",
                    ImageSource = "E:/C#/ClientsChat/ClientsChat/Icons/photo.png",
                    Message = "Test message for Artem",
                    IsNativeOrigin = false,
                    FirstMessage = false
                });
            }

            for (int i = 0; i < 2; i++)
            {
                Messages.Add(new MessageModel
                {
                    Username = "Stas",
                    UsernameColor = "Blue",
                    ImageSource = "E:/C#/ClientsChat/ClientsChat/Icons/photo.png",
                    Message = "Test message for Stas",
                    IsNativeOrigin = true,
                    FirstMessage = false
                });
            }

            for (int i = 0; i < 2; i++)
            {
                Messages.Add(new MessageModel
                {
                    Username = "Sergek",
                    UsernameColor = "Green",
                    ImageSource = "E:/C#/ClientsChat/ClientsChat/Icons/photo.png",
                    Message = "Test message for Sergek",
                    IsNativeOrigin = true,
                    FirstMessage = false
                });
            }

            Messages.Add(new MessageModel
            {
                Username = "Slava",
                UsernameColor = "Red",
                ImageSource = "E:/C#/ClientsChat/ClientsChat/Icons/photo.png",
                Message = "Test message for Slava",
                IsNativeOrigin = false,
                FirstMessage = true
            });

            for (int i = 0; i < 5; i++)
            {
                Contacts.Add(new ContactModel
                {
                    Username = $"Slava {i}",
                    ImageSourse = "/Icons/photo.png",
                    Messages = Messages
                });
            }
        }
    }
}
