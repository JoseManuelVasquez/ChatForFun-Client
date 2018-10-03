using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ChatForFun_Client.Interface;
using ChatForFun_Client.Protocol;

namespace ChatForFun_Client.Interactor
{
    class SignUpInteractor: ISignUpInteractor
    {
        /* Listener */
        private ISignUpDataListener listener;

        public SignUpInteractor(ISignUpDataListener listener)
        {
            this.listener = listener;
        }

        public void registerUser(string user, string password)
        {
            NetworkStream connection = ConnectionClient.getConnectionClient();

            /* REGISTER command */
            byte[] bytes;
            bytes = Encoding.ASCII.GetBytes("RGST ");
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
            for (int i = 0; i < bytesRead; i++)
            {
                if (!buffer[i].Equals(00))
                    response += Convert.ToChar(buffer[i]);
            }

            Console.WriteLine(response);
            if (response.ToUpper().Equals("RGTD"))
                listener.onRegisterSuccess();
            else if (response.ToUpper().Equals("ERRO"))
            {
                bytesRead = connection.Read(buffer, 0, 5);
                listener.onRegisterFail();
            }
        }
    }
}
