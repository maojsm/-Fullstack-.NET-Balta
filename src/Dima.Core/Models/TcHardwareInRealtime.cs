using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dima.Core.Models
{
    /// <summary>
    /// Informações do Modulo de Comunicação com ESP32
    /// </summary>
    public class TcHardwareInRealtime
    {
        public long Id { get; set; }

        public long TrafficControllerId { get; set; }

        //[JsonIgnore]
        public TrafficController TrafficController { get; set; } = null!;

        public double VoltageAcIn { get; set; }

        public double Voltage9vIn { get; set; }

        public bool DoorOpen { get; set; }

        public bool FlashingYellowOn { get; set; }

        public bool CpuConnected { get; set; }

        // Calculado pela API de acordo com a data de criação do registro (CreatedAt)
        [NotMapped]
        public bool TcpConnected { get; set; }

        public string ClientIpV4 { get; set; } = string.Empty;
        public int ClientPort { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
