using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epoll.Core
{
    //Interface defines list  > go to EpollServices
    public interface IEpollServices
    {
        List<EpollModel> GetEpolls();
        EpollModel GetEpoll(string id);
        EpollModel AddEpoll(EpollModel epollModel);
        EpollModel UpdateEpoll(EpollModel epollModel);
        void  DeleteEpoll(string id);
    }
}
