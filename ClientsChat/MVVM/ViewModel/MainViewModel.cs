using ClientsChat.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsChat.MVVM.ViewModel
{
    class MainViewModel
    {
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<ContactModel> Contacts { get; set; }

        public MainViewModel()
        {
            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();

            Messages.Add(new MessageModel
            {
                Username = "Aleksey",
                UsernameColor = "Black",
                ImageSource = "",
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
                    ImageSource = "",
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
                    ImageSource = "",
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
                    ImageSource = "",
                    Message = "Test message for Sergek",
                    IsNativeOrigin = true,
                    FirstMessage = false
                });
            }

            Messages.Add(new MessageModel
            {
                Username = "Slava",
                UsernameColor = "Red",
                ImageSource = "",
                Message = "Test message for Slava",
                IsNativeOrigin = false,
                FirstMessage = true
            });

            for (int i = 0; i < 5; i++)
            {
                Contacts.Add(new ContactModel
                {
                    Username = $"Slava {i}",
                    ImageSourse = "E:/C#/ClientsChat/ClientsChat/Icons/photo.png",
                    Messages = Messages
                });
            }
        }
    }
}
