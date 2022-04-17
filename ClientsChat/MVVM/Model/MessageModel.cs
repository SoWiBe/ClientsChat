using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientsChat.MVVM.Model
{
    class MessageModel
    {
        public string Message { get; set; }
        public string Time { get; set; }
        public bool IsNativeOrigin { get; set; }
        public StaticResourceExtension ChatItem { get; set; }
    }
}
