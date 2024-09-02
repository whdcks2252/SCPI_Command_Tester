using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCP_Lib
{
    public class SG_TCP : IDisposable
    {
        private TcpClient client;
        private NetworkStream stream;
        private readonly string ip;
        private readonly int port;

        public static SG_TCP InitInstance(string _ip, int _port)
        {
            return new SG_TCP(_ip, _port);
        }

        public SG_TCP(string _ip, int _port)
        {
            ip = _ip;
            port = _port;
            client = new TcpClient();

        }

        public async Task<bool> TCPConnect(CancellationToken token)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            await client.ConnectAsync(endPoint, token);
            stream = client.GetStream();

            return IsConnect();

        }

        public async Task SendMessage(string message, CancellationToken token)
        {
            if (!IsConnect()) throw new InvalidOperationException("Not connected to a server.");
            if (stream == null) throw new InvalidOperationException("Not stream.");

            byte[] bytes = Encoding.ASCII.GetBytes(message + "\r\n");
            await stream.WriteAsync(bytes, 0, bytes.Length,token);

        }

        public async Task<string> ReceiveLineAsync(CancellationToken token)
        {
            if (!IsConnect()) throw new InvalidOperationException("Not connected to a server.");
            if (stream == null) throw new InvalidOperationException("Not stream.");

            byte[] buffer = new byte[1024];
            int bytes = await stream.ReadAsync(buffer, 0, buffer.Length, token);

            if (bytes <= 0) return "";

            return Encoding.UTF8.GetString(buffer, 0, bytes);

        }


        public bool IsConnect()
        {
            if (client == null)
                return false;
            else
                return client.Connected;
        }

        public void Dispose()
        {

            if (client != null)
            {
                client.Dispose();
            }

            if (stream != null)
            {
                stream.Dispose();
            }
        }

        public string GetIP()
        {
            return ip;
        }

        public int GetPort()
        {
            return port;
        }


    }
}
