using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestBll
{
    public class ReturnedResult<T>
    {
        public string Message { get;internal set; }
        public byte Result { get; internal set; }

        public T Data { get; internal set; }
    }
}
