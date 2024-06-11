using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Requests.TrafficController
{
    public class GetTrafficControllerByIdRequest : Request
    {
        public long Id { get; set; }
    }
}
