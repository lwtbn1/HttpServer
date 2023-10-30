using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Datas
{
    [Serializable]
    public class RecvData
    {
        public int cmd;
        public string data;
    }
}
