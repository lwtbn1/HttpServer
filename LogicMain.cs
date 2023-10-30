using HttpServer.Logics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LitJson;
using HttpServer.Base;
using System.Net;
using HttpServer.Datas;

namespace HttpServer
{
    public class LogicMain : Singleton<LogicMain>   
    {
        private object locker = new object();
        private Dictionary<int, ILogic> logics = new Dictionary<int, ILogic>();

        public void Regist()
        {
            Regist(CMDDefine.CMD_001_CREATE_ACCOUNT, new Logic001CreateAccount());
        }

        void Regist(int cmd, ILogic logic)
        {
            if(logics.ContainsKey(cmd))
                logics.Remove(cmd);
            logics.Add(cmd, logic);
        }

        public void DoLogic(string recvContent, HttpListenerContext httpListenerContext)
        {
            try
            {
                var recvData = JsonMapper.ToObject<RecvData>(recvContent);
                if(logics.TryGetValue(recvData.cmd, out ILogic logic))
                {
                    logic.Handle(recvData.data, httpListenerContext);
                }

            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
