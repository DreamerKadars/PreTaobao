using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
namespace PreTaobao.Pages
{
    public class Send
    {
        public static string Fip = "39.98.126.110";
        private const int port=3306;
        public Send()
        {

        }
        public static void send(string file, string ip)
        {

            Socket ConnectSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ServerIPAddress = IPAddress.Parse(ip);
            IPEndPoint ServerIPEndPoint = new IPEndPoint(ServerIPAddress, port);

            // ConnectSocket.
            ConnectSocket.Connect(ServerIPEndPoint);
            String path = file;
            FileStream reader = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
            long send = 0L, length = reader.Length;
            string fileName = Path.GetFileName(path);
            string sendStr = "namelength," + fileName + "," + length.ToString();
            ConnectSocket.Send(Encoding.Default.GetBytes(sendStr));

            int BufferSize = 1024;
            byte[] buffer = new byte[32];
            ConnectSocket.Receive(buffer);
            string mes = Encoding.Default.GetString(buffer);
            if (mes.Contains("OK"))
            {
                Console.WriteLine("Sending file:" + fileName + ".Plz wait...");
                byte[] fileBuffer = new byte[BufferSize];
                int read, sent;
                while ((read = reader.Read(fileBuffer, 0, BufferSize)) != 0)
                {
                    sent = 0;
                    while ((sent += ConnectSocket.Send(fileBuffer, sent, read, SocketFlags.None)) < read)
                    {
                        send += (long)sent;
                    }
                }
                Console.WriteLine("Send finish.\n");
            }


        }

    }
}
