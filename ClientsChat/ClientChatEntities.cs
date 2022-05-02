using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsChat.SpecialUse
{
    public partial class ClientChatEntities
    {
        private static ClientChatEntities _context;
        public static ClientChatEntities GetContext()
        {
            if (_context == null)
                _context = new ClientChatEntities();
            return _context;
        }
    }
}
