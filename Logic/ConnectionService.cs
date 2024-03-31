using Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class ConnectionService : IConnectionService
    {
        public Action<string> ConnectionLogger { get; set; }

        public bool Connected
        {
            get
            {
                if (WebSocketClient.CurrentConnection != null)
                {
                    return WebSocketClient.CurrentConnection.IsConnected;
                }

                return false;
            }
        }

        public async Task<bool> Connect(Uri peerUri)
        {
            try
            {
                ConnectionLogger?.Invoke($"Establishing connection to {peerUri.OriginalString}");

                await WebSocketClient.Connect(peerUri, ConnectionLogger);

                return await Task.FromResult(true);
            }
            catch (WebSocketException e)
            {
                Debug.WriteLine($"Caught web socket exception {e.Message}");
                ConnectionLogger?.Invoke(e.Message);
                return await Task.FromResult(false);
            }

        }

        public async Task Disconnect()
        {
            await WebSocketClient.Disconnect();
        }
    }
}
