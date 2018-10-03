using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ChatForFun_Client.Interface;
using ChatForFun_Client.Protocol;
using ChatForFun_Client.View;

namespace ChatForFun_Client.Interactor
{
    class LoginInteractor : ILoginInteractor
    {
        private ILoginOnDataListener listener;

        public LoginInteractor(ILoginOnDataListener listener)
        {
            this.listener = listener;
        }

        public void authenticateUser(string user, string password)
        {
            NetworkStream connection = ConnectionClient.getConnectionClient();

            /* LOGIN command */
            byte[] bytes;
            bytes = Encoding.ASCII.GetBytes("LOIN ");
            connection.Write(bytes, 0, bytes.Length);

            bytes = BitConverter.GetBytes(user.Length);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            connection.Write(bytes, 0, bytes.Length);

            bytes = Encoding.ASCII.GetBytes(user);
            connection.Write(bytes, 0, bytes.Length);

            bytes = BitConverter.GetBytes(password.Length);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            connection.Write(bytes, 0, bytes.Length);

            bytes = Encoding.ASCII.GetBytes(password);
            connection.Write(bytes, 0, bytes.Length);

            byte[] buffer = new byte[100];
            int bytesRead = connection.Read(buffer, 0, 4);
            string response = "";
            for (int i=0; i < bytesRead; i++)
            {
                if (!buffer[i].Equals(00))
                    response += Convert.ToChar(buffer[i]);
            }

            Console.WriteLine(response);
            if (response.ToUpper().Equals("LGGD"))
            {
                listener.onLoginSuccess();
            }
            else if (response.ToUpper().Equals("ERRO"))
            {
                bytesRead = connection.Read(buffer, 0, 5);
                listener.onLoginFail();
            }
        }
    }
}
