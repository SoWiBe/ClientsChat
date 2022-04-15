using ChatServer.Net.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    class Client
    {
        public string Username { get; set; }
        public Guid UID { get; set; }
        public TcpClient ClientSocket { get; set; }

        PacketReader _packerReader;
        public Client(TcpClient client)
        {
            ClientSocket = client;
            UID = Guid.NewGuid();

            _packerReader = new PacketReader(ClientSocket.GetStream());

            var opcode = _packerReader.ReadByte();
            Username = _packerReader.ReadMessage();

            Console.WriteLine($"[{DateTime.Now}]: Client has connected with the username: { Username }");
        }
    }
}
