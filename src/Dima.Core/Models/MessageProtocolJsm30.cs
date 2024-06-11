using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Models
{
    public class MessageProtocolJsm30
    {
        public ulong Id { get; set; }

        public string CommandName { get; set; } = string.Empty;
        public string CodeLocal { get; set; } = string.Empty;
        public byte Command { get; set; }
        public byte[] param { get; set; } = Array.Empty<byte>();

        public string ClientIpV4 { get; set; } = string.Empty;
        public int ClientPort { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public long TrafficControllerId { get; set; }
        public TrafficController TrafficController { get; set; } = new TrafficController();

        public string UserId { get; set; } = string.Empty;
        public int TenantId { get; set; }
    }
}
