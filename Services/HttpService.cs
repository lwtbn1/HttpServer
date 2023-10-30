using HttpServer.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Services
{
     
    public class HttpService : Singleton<HttpService>
    {
        private HttpListener listener;

        public void Start()
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:30000/");
            listener.Start();
            listener.BeginGetContext(GetContextCallback, listener);
        }

        private void GetContextCallback(IAsyncResult ar)
        {
            var _listener = ar.AsyncState as HttpListener;
            if (_listener.IsListening)
            {
                var context = _listener.EndGetContext(ar);
                var request = context.Request;
                var queryStr = request.QueryString;

                if (request.HttpMethod == "POST")
                {
                    var stream = context.Request.InputStream;
                    var reader = new StreamReader(stream, Encoding.UTF8);
                    var content = reader.ReadToEnd();
                    LogicMain.Ins.DoLogic(content, context);
                }
                else
                {
                    Console.WriteLine("只能接收POST请求");
                }
                Console.WriteLine(queryStr.GetValues("p")[0]);
                listener.BeginGetContext(GetContextCallback, listener);
            }
        }

        public void Respons(string data, HttpListenerContext context)
        {
            HttpListenerResponse response = context.Response;
            response.StatusCode = (int)HttpStatusCode.OK;
            response.ContentType = "application/json;charset=UTF-8";
            response.ContentEncoding = Encoding.UTF8;
            response.AppendHeader("Content-Type", "application/json;charset=UTF-8");

            using (StreamWriter writer = new StreamWriter(response.OutputStream, Encoding.UTF8))
            {
                writer.Write(data);
                writer.Close();
                response.Close();
            }

        }

    }
}
