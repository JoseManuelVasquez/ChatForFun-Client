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
    class ChatInteractor: IChatInteractor
    {
        /* Receiving commands */
        bool isReceiving;

        /* Listener */
        private IChatDataListener listener;
        public ChatInteractor(IChatDataListener listener)
        {
            this.listener = listener;
            isReceiving = true;
        }

        public void addFriend(string friend)
        {
            NetworkStream connection = ConnectionClient.getConnectionClient();

            /* ADD FRIEND command */
            byte[] bytes;
            bytes = Encoding.ASCII.GetBytes("ADDF ");
            connection.Write(bytes, 0, bytes.Length);

            bytes = BitConverter.GetBytes(friend.Length);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            connection.Write(bytes, 0, bytes.Length);

            bytes = Encoding.ASCII.GetBytes(friend);
            connection.Write(bytes, 0, bytes.Length);
        }

        public void deleteAccount()
        {
            NetworkStream connection = ConnectionClient.getConnectionClient();

            /* DELETE ACCOUNT command */
            byte[] bytes;
            bytes = Encoding.ASCII.GetBytes("DELA");
            connection.Write(bytes, 0, bytes.Length);

            listener.onLogout();
        }

        public void deleteFriend(string friend)
        {
            NetworkStream connection = ConnectionClient.getConnectionClient();

            /* DELETE FRIEND command */
            byte[] bytes;
            bytes = Encoding.ASCII.GetBytes("DELF ");
            connection.Write(bytes, 0, bytes.Length);

            bytes = BitConverter.GetBytes(friend.Length);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            connection.Write(bytes, 0, bytes.Length);

            bytes = Encoding.ASCII.GetBytes(friend);
            connection.Write(bytes, 0, bytes.Length);
        }

        public void logout()
        {
            NetworkStream connection = ConnectionClient.getConnectionClient();

            /* LOGOUT ACCOUNT command */
            byte[] bytes;
            bytes = Encoding.ASCII.GetBytes("LOUT");
            connection.Write(bytes, 0, bytes.Length);

            listener.onLogout();
        }

        public void receiveCommand()
        {
            NetworkStream connection = ConnectionClient.getConnectionClient();

            while (isReceiving)
            {
                /* Expecting for a command */
                byte[] buffer = new byte[100];
                int bytesRead = connection.Read(buffer, 0, 4);
                string response = "";
                for (int i = 0; i < bytesRead; i++)
                {
                    if (!buffer[i].Equals(00))
                        response += Convert.ToChar(buffer[i]);
                }

                Console.WriteLine(response);
                switch (response)
                {
                    case "LGUT":
                        isReceiving = false;
                        break;
                    case "FRDS":
                        bytesRead = connection.Read(buffer, 0, 1);
                        bytesRead = connection.Read(buffer, 0, 4);
                        byte[] byteLength = new byte[4];
                        for (int i = 0; i < bytesRead; i++)
                            byteLength[i] = buffer[bytesRead - i - 1];
                        int length = BitConverter.ToInt32(byteLength, 0);

                        List<string> friends = new List<string>();
                        List<string> statusFriends = new List<string>();
                        int lenFriend;
                        string friend;
                        string status;
                        for (int i = 0; i < length; i++)
                        {
                            bytesRead = connection.Read(buffer, 0, 4);
                            for (int j = 0; j < bytesRead; j++)
                                byteLength[j] = buffer[bytesRead - j - 1];
                            lenFriend = BitConverter.ToInt32(byteLength, 0);

                            bytesRead = connection.Read(buffer, 0, lenFriend);
                            friend = "";
                            status = "";
                            for (int j = 0; j < lenFriend - 1; j++)
                            {
                                if (!buffer[j].Equals(00))
                                    friend += Convert.ToChar(buffer[j]);
                            }
                            status += Convert.ToChar(buffer[lenFriend - 1]);

                            Console.WriteLine(friend);
                            friends.Add(friend);
                            statusFriends.Add(status);
                        }

                        listener.onReceiveFriends(friends, statusFriends);
                        break;
                    case "ADED":
                        bytesRead = connection.Read(buffer, 0, 1);
                        bytesRead = connection.Read(buffer, 0, 4);

                        byte[] byteAded = new byte[4];
                        for (int i = 0; i < bytesRead; i++)
                            byteAded[i] = buffer[bytesRead - i - 1];
                        int lengthAded = BitConverter.ToInt32(byteAded, 0);

                        string friendName = "";
                        bytesRead = connection.Read(buffer, 0, lengthAded);
                        for (int i = 0; i < lengthAded; i++)
                        {
                            if (!buffer[i].Equals(00))
                                friendName += Convert.ToChar(buffer[i]);
                        }
                        listener.onFriendAddingSuccess(friendName);
                        break;
                    case "DELD":
                        bytesRead = connection.Read(buffer, 0, 1);
                        bytesRead = connection.Read(buffer, 0, 4);

                        byte[] byteDeld = new byte[4];
                        for (int i = 0; i < bytesRead; i++)
                            byteDeld[i] = buffer[bytesRead - i - 1];
                        int lengthDeld = BitConverter.ToInt32(byteDeld, 0);

                        string friendDeld = "";
                        bytesRead = connection.Read(buffer, 0, lengthDeld);
                        for (int i = 0; i < lengthDeld; i++)
                        {
                            if (!buffer[i].Equals(00))
                                friendDeld += Convert.ToChar(buffer[i]);
                        }
                        listener.onFriendDeletingSuccess(friendDeld);
                        break;
                    case "SDED":
                        bytesRead = connection.Read(buffer, 0, 1);
                        bytesRead = connection.Read(buffer, 0, 4);

                        byte[] byteSded = new byte[4];
                        for (int i = 0; i < bytesRead; i++)
                            byteSded[i] = buffer[bytesRead - i - 1];
                        int lengthSded = BitConverter.ToInt32(byteSded, 0);

                        string friendSded = "";
                        bytesRead = connection.Read(buffer, 0, lengthSded);
                        for (int i = 0; i < lengthSded; i++)
                        {
                            if (!buffer[i].Equals(00))
                                friendSded += Convert.ToChar(buffer[i]);
                        }
                        listener.onMessageSuccess(friendSded);
                        break;
                    case "RVED":
                        bytesRead = connection.Read(buffer, 0, 1);
                        bytesRead = connection.Read(buffer, 0, 4);

                        byte[] byteRved = new byte[4];
                        for (int i = 0; i < bytesRead; i++)
                            byteRved[i] = buffer[bytesRead - i - 1];
                        int lengthRved = BitConverter.ToInt32(byteRved, 0);
                        string friendRved = "";
                        bytesRead = connection.Read(buffer, 0, lengthRved);
                        for (int i = 0; i < lengthRved; i++)
                        {
                            if (!buffer[i].Equals(00))
                                friendRved += Convert.ToChar(buffer[i]);
                        }

                        bytesRead = connection.Read(buffer, 0, 4);
                        for (int i = 0; i < bytesRead; i++)
                            byteRved[i] = buffer[bytesRead - i - 1];
                        length = BitConverter.ToInt32(byteRved, 0);
                        string message = "";
                        string date = "";
                        bytesRead = connection.Read(buffer, 0, length);
                        for (int i = 0; i < length - 17; i++)
                        {
                            if (!buffer[i].Equals(00))
                                message += Convert.ToChar(buffer[i]);
                        }

                        for (int i = length - 17; i < length; i++)
                        {
                            if (!buffer[i].Equals(00))
                                date += Convert.ToChar(buffer[i]);
                        }

                        listener.onReceiveMessage(friendRved, message, date);

                        break;
                    case "FDIN":
                        bytesRead = connection.Read(buffer, 0, 1);
                        bytesRead = connection.Read(buffer, 0, 4);

                        byte[] byteLen = new byte[4];
                        for (int i = 0; i < bytesRead; i++)
                            byteLen[i] = buffer[bytesRead - i - 1];
                        int len = BitConverter.ToInt32(byteLen, 0);
                        string friendFdin = "";
                        bytesRead = connection.Read(buffer, 0, len);
                        for (int i = 0; i < len; i++)
                        {
                            if (!buffer[i].Equals(00))
                                friendFdin += Convert.ToChar(buffer[i]);
                        }
                        Console.WriteLine(friendFdin);
                        listener.onReceiveStatus(friendFdin, "I");
                        break;
                    case "FOUT":
                        bytesRead = connection.Read(buffer, 0, 1);
                        bytesRead = connection.Read(buffer, 0, 4);

                        byte[] byteL = new byte[4];
                        for (int i = 0; i < bytesRead; i++)
                            byteL[i] = buffer[bytesRead - i - 1];
                        int l = BitConverter.ToInt32(byteL, 0);
                        string friendUser = "";
                        bytesRead = connection.Read(buffer, 0, l);
                        for (int i = 0; i < l; i++)
                        {
                            if (!buffer[i].Equals(00))
                                friendUser += Convert.ToChar(buffer[i]);
                        }
                        Console.WriteLine(friendUser);
                        listener.onReceiveStatus(friendUser, "O");
                        break;
                    case "ERRO":
                        bytesRead = connection.Read(buffer, 0, 1);
                        bytesRead = connection.Read(buffer, 0, 4);

                        byte[] bytesError = new byte[4];
                        for (int i = 0; i < bytesRead; i++)
                            bytesError[i] = buffer[bytesRead - i - 1];
                        int error = BitConverter.ToInt32(bytesError, 0);
                        Console.WriteLine(error);

                        /* More errors to add */
                        switch(error)
                        {
                            case 3: listener.onFriendAddingFail(); break;
                            case 4: listener.onFriendDeletingFail(); break;
                            case 8: listener.onMessageFail(); break;
                        }
                        break;
                }
            }
        }

        public void sendMessage(string friend, string message)
        {
            NetworkStream connection = ConnectionClient.getConnectionClient();

            /* SEND MESSAGE TO FRIEND command */
            byte[] bytes;
            bytes = Encoding.ASCII.GetBytes("SEND ");
            connection.Write(bytes, 0, bytes.Length);

            bytes = BitConverter.GetBytes(friend.Length);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            connection.Write(bytes, 0, bytes.Length);

            bytes = Encoding.ASCII.GetBytes(friend);
            connection.Write(bytes, 0, bytes.Length);

            bytes = BitConverter.GetBytes(message.Length);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            connection.Write(bytes, 0, bytes.Length);

            bytes = Encoding.ASCII.GetBytes(message);
            connection.Write(bytes, 0, bytes.Length);
        }
    }
}
