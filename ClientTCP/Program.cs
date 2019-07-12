using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientTCP
{
    class Program
    {
        static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 8080;

            var udpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("ВВЕДИТЕ СООБЩЕНИЕ: ");
            var messages = Console.ReadLine();

            var data = Encoding.UTF8.GetBytes(messages);
            udpSocket.Connect(udpEndPoint);

            udpSocket.Send(data);

            var buffer = new byte[256];
            var size = 0;
            var answer = new StringBuilder();

            do
            {
                size = udpSocket.Receive(buffer);
                answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
            }
            while (udpSocket.Available > 0);

            Console.WriteLine(answer.ToString());
            udpSocket.Shutdown(SocketShutdown.Both);
            udpSocket.Close();
        }
    }
}
