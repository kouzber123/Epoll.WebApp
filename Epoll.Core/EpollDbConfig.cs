using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epoll.Core
{
    public class EpollDbConfig
    {
        //same format as in launchsettings.json
        public string Database_Name { get; set; }
        public string Epoll_Collection_Name { get; set; }
        public string Connection_String { get; set; }
    }
}
