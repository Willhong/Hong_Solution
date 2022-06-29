using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Hong_Solution.Communication
{
    public class SocketConnect
    {
        public Socket clientSock;
        public String sServerIP;
        public String nServerPort;
        public SocketConnect()
        {
            clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ConnectToServer();
        }

        public void ConnectToServer()
        {
            clientSock.Connect("127.0.0.1", 9999);

            AsyncObject obj = new AsyncObject(2048);
            obj.WorkingSocket = clientSock;
            //textBox1.Text = "Connected";
            clientSock.BeginReceive(obj.Buffer, 0, obj.BufferSize, 0, DataReceived, obj);
        }

        public void DataReceived(IAsyncResult ar)
        {
            string strMsg;

            AsyncObject obj = (AsyncObject)ar.AsyncState;
            int received;
            try
            {
                received = obj.WorkingSocket.EndReceive(ar);

                // 받은 데이터가 없으면(연결끊어짐) 끝낸다.
                if (received <= 0)
                {
                    obj.WorkingSocket.Close();
                    return;
                }
                strMsg = Encoding.UTF8.GetString(obj.Buffer).TrimEnd('\0');
                //Invoke(new Action(delegate ()
                //{
                //    textBox2.Text = "Received:" + strMsg;

                //}));
                obj.ClearBuffer();
                obj.WorkingSocket.BeginReceive(obj.Buffer, 0, obj.BufferSize, 0, DataReceived, obj);
            }
            catch (Exception ex)
            {
                new Task(ReconnectThread).Start();
            }
        }

        public void ReconnectThread()
        {
            while (true)
            {
                if (clientSock.Connected == false)
                {
                    clientSock = null;
                    try
                    {
                        clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        ConnectToServer();
                        if (clientSock.Connected == true)
                        {
                            return;
                        }
                    }
                    catch (SocketException ex)
                    {

                    }
                }
            }
        }
    }
}
