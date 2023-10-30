using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Base
{
    public class Singleton<T> where T : new()
    {
        private static object locker = new object();
        private static T ins;
        public static T Ins
        {

            get
            {
                lock (locker)
                {
                    if (ins == null)
                    {
                        ins = new T();
                    }
                    return ins;
                }
                
            }
        }

    }
}
