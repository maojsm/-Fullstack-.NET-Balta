using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Models
{
    public class TrafficController
    {
        public long Id { get; set; }

        public string CodeLocal { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int TenantId { get; set; }

        // Relação 1 para muitos com a tabela TcHardwareInRealtime
        public ICollection<TcHardwareInRealtime> TcHardwareInRealtimes { get; set; } = [];

    }
}
