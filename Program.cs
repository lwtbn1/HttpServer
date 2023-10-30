using HttpServer.Datas;
using HttpServer.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace HttpServer
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            DataLogic.Ins.Load();
            LogicMain.Ins.Regist();
            HttpService.Ins.Start();

            Thread mainThrea = new Thread(() => { 
                while (true)
                {
                    Thread.Sleep(1000);
                }
            });
            mainThrea.Start();
            
        }

        

    }
}
