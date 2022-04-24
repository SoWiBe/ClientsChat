﻿using ClientsChat.Core;
using ClientsChat.MVVM.Model;
using ClientsChat.Net;
using ClientsChat.SpecialUse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientsChat.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        
        public ObservableCollection<UserModel> Users { get; set; }
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<ContactModel> Contacts { get; set; }
        public string Username { get; set; }

        /* Commands */
        public RelayCommand SendCommand { get; set; }
        public RelayCommand ConnectToServerCommand { get; set; }
        public RelayCommand SendMessageCommand { get; set; }

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

        public string Message { get; set; }
        
        public MainViewModel()
        {
            Users = new ObservableCollection<UserModel>();
            _server = new Server();
            _server.connectedEvent += UserConnected;
            _server.msgReceivedEvent += MessageReceived;
            _server.userDisconnectEvent += RemoveUser;

            ConnectToServerCommand = new RelayCommand(o => _server.ConnectToServer(Username), o => !string.IsNullOrEmpty(Username));
            SendMessageCommand = new RelayCommand(o => _server.SendMessageToServer(Message), o => !string.IsNullOrEmpty(Message));

            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();

            Contacts.Add(new ContactModel
            {
                Username = "Aleksey",
                ImageSourse = "E:/C#/ClientsChat/ClientsChat/Icons/group2.png"
            });
            Contacts.Add(new ContactModel
            {
                Username = "Stas",
                ImageSourse = "E:/C#/ClientsChat/ClientsChat/Icons/group3.png"
            });
            Contacts.Add(new ContactModel
            {
                Username = "Slava",
                ImageSourse = "E:/C#/ClientsChat/ClientsChat/Icons/group1.png"
            });
            Contacts.Add(new ContactModel
            {
                Username = "Maria",
                ImageSourse = "E:/C#/ClientsChat/ClientsChat/Icons/photo.png"
            });
            Contacts.Add(new ContactModel
            {
                Username = "Mixa",
                ImageSourse = "E:/C#/ClientsChat/ClientsChat/Icons/group2.png"
            });
            Contacts.Add(new ContactModel
            {
                Username = "Aleksey",
                ImageSourse = "E:/C#/ClientsChat/ClientsChat/Icons/group2.png"
            });
            Contacts.Add(new ContactModel
            {
                Username = "Stas",
                ImageSourse = "E:/C#/ClientsChat/ClientsChat/Icons/group3.png"
            });
            Contacts.Add(new ContactModel
            {
                Username = "Slava",
                ImageSourse = "E:/C#/ClientsChat/ClientsChat/Icons/group1.png"
            });
            Contacts.Add(new ContactModel
            {
                Username = "Maria",
                ImageSourse = "E:/C#/ClientsChat/ClientsChat/Icons/photo.png"
            });
            Contacts.Add(new ContactModel
            {
                Username = "Mixa",
                ImageSourse = "E:/C#/ClientsChat/ClientsChat/Icons/group2.png"
            });
        }

        private void RemoveUser()
        {
            var uid = _server.PacketReader.ReadMessage();
            var user = Users.Where(x => x.ID == uid).FirstOrDefault();
            Application.Current.Dispatcher.Invoke(() => Users.Remove(user));
        }

        private void MessageReceived()
        {
            var msg = _server.PacketReader.ReadMessage();
            Application.Current.Dispatcher.Invoke(() => Messages.Add(new MessageModel
            {
                Message = msg,
                Time = DateTime.Now.ToString("dddd, dd MMMM yyyy h:mm tt"),
                IsNativeOrigin = true
            }));
        }

        private void UserConnected()
        {
            var user = new UserModel
            {
                Username = _server.PacketReader.ReadMessage(),
                ID = _server.PacketReader.ReadMessage()
            };

            if (!Users.Any(x => x.ID == user.ID))
            {
                Application.Current.Dispatcher.Invoke(() => Users.Add(user));
            }
        }
    }
}