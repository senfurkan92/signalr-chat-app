using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Models
{
    public class ResultModel
    {
        public bool IsSuccess { get; set; }

        public Exception? Exception { get; set; }
    }

    public class ResultModel<T> : ResultModel
    {
        public T? Data { get; set; }
    }
}
