using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsChat
{
    public partial class Clients
    {
        public string ImageNew
        {
            get
            {
                if (Image == null)
                    return "E:/C#/ClientsChat/ClientsChat/Icons/group3.png";
                return Image;
            }
            set { }
        }
    }
}
