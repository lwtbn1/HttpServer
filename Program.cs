using System;
using System.Collections.Generic;
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
        static HttpListener listener;
        static void Main(string[] args)
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:30000/");
            listener.Start();

            listener.BeginGetContext(GetContextCallback, listener);


            Thread mainThrea = new Thread(() => { 
                while (true)
                {
                    Thread.Sleep(1000);
                }
            });
            mainThrea.Start();
            
        }

        static void GetContextCallback(IAsyncResult ar)
        {
            var _listener = ar.AsyncState as HttpListener;
            if(_listener.IsListening)
            {
                var context = _listener.EndGetContext(ar);
                var request = context.Request;
                var queryStr = request.QueryString;
                Console.WriteLine(queryStr.GetValues("p")[0]);
                listener.BeginGetContext(GetContextCallback, listener);
            }
        }

    }
}
