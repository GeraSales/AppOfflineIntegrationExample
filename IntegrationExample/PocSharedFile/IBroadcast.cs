using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocSharedFile
{
    public interface IBroadcast
    {
        void SendRequest(DataReceiverModel data);       
    }
}
