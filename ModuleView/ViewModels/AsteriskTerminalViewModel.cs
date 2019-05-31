using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace ModuleView.ViewModels
{
    public class AsteriskTerminalViewModel : BindableBase
    {
        private TcpClient _client;
        public AsteriskTerminalViewModel()
        {             

        }

        public bool IsConnected
        {
            get { return _client.Connected; }
        }
    }
}
