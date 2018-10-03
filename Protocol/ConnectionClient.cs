using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatForFun_Client.Protocol
{
    /// <summary>
    /// Connection from client to server
    /// </summary>
    public class ConnectionClient
    {
        /* Connection attribute */
        private volatile static NetworkStream clientConnection;

        private ConnectionClient() {}

        /// <summary>
        /// Singleton instance of our connection socket
        /// </summary>
        /// <returns>Socket</returns>
        public static NetworkStream getConnectionClient()
        {
            if(clientConnection == null)
            {
                TcpClient tcpConnection = new TcpClient("localhost", 27500);
                clientConnection = tcpConnection.GetStream();
            }

            return clientConnection;
        }

        /// <summary>
        /// Destructor of our singleton socket
        /// </summary>
        public static void destroyInstance()
        {
            clientConnection = null;
        }
    }
}
