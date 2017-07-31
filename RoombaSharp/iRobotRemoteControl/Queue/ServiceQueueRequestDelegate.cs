using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Queues
{
    /// <summary>
    /// Process request delegate.
    /// </summary>
    /// <param name="data"></param>
    public delegate void ServiceQueRequestDelegate(Object data);
}
