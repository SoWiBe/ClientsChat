using ClientsChat.Core;
using ClientsChat.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsChat.MVVM.ViewModel
{
    class ChooseImagesModel : ObservableObject
    {
        public ObservableCollection<ProfileImage> Images = new ObservableCollection<ProfileImage>();
        public ChooseImagesModel()
        {
            for (int i = 0; i < 3; i++)
            {
                Images.Add(new ProfileImage
                {
                    Icon = "E:/C#/ClientsChat/ClientsChat/Icons/group2.png",
                    IconTwo = "E:/C#/ClientsChat/ClientsChat/Icons/group2.png",
                    IconThree = "E:/C#/ClientsChat/ClientsChat/Icons/group2.png"
                });
            }
            
        }
    }
}
