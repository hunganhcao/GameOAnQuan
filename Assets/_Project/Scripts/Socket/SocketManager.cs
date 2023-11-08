using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace WorldForKid.ConnectSocket
{
    public class SocketManager : Singleton<SocketManager>
    {
        [SerializeField] private EventName e_ShowPopupMessage;
        private TcpClient _clientSocket;
        private NetworkStream _stream;
        private Thread _clientThread;
        private string ipAddress;
        private int port;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
        private void Update()
        {
            if (_stream != null && _stream.DataAvailable)
            {
                HandleReceivedDataFromServer();
            }
        }

        private void HandleReceivedDataFromServer()
        {
            byte[] buffer = new byte[8096];
            int byteCount = _stream.Read(buffer, 0, buffer.Length);
            string dataReceived = Encoding.ASCII.GetString(buffer, 0, byteCount);
            var msg = new BaseMessage().FromJson(dataReceived);

            ExcuteMessage.Excute(this, msg);
            
            
        }
        public void ConnectToServer(string ipAddress, int port)
        {
            this.ipAddress = ipAddress;
            this.port = port;
            _clientSocket = new TcpClient();
            _clientSocket.Connect(ipAddress, port);
            _stream = _clientSocket.GetStream();
            Debug.Log("Connected to server.");

        }

        public void SendAndReceiveData()
        {
            if (_stream != null)
            {
                if (_stream.CanWrite)
                {
                    string dataToSend = ipAddress;
                    byte[] data = Encoding.ASCII.GetBytes(dataToSend);
                    _stream.Write(data, 0, data.Length);
                    _stream.Flush();
                }
            }
        }
        public void SendToServer(BaseMessage msg)
        {
            Debug.Log("Send to Socket: " + msg.code + "|" + msg.message);
            if (_stream != null)
            {
                if (_stream.CanWrite)
                {
                    string dataToSend = msg.ToJson();
                    byte[] data = Encoding.ASCII.GetBytes(dataToSend);
                    _stream.Write(data, 0, data.Length);
                    _stream.Flush();
                }
            }
        }
        public void SendToServer(Code code, object data)
        {
            var msg = new BaseMessage(code, data);
            SendToServer(msg);
        }

        private void OnDestroy()
        {
            if (_stream != null)
                _stream.Close();
            if (_clientSocket != null)
                _clientSocket.Close();
            if (_clientThread != null)
                _clientThread.Abort();
        }
    }
}
