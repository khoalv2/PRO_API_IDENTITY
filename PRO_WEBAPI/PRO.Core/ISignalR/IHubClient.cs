using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRO.Core.ISignalR
{
    public interface IHubClient
    {
        Task BroadcastMessage();
    }
}
