using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Logics
{
    public interface ILogic
    {
        void Handle(string data, HttpListenerContext httpListenerContext);
    }
}
