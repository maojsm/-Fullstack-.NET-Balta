using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Dtos;

public class TcHardwareInRealtimeDto
{
    public long Id { get; set; }
    public long TrafficControllerId { get; set; }
    public TrafficControllerDto TrafficController { get; set; } = null!;
    public double VoltageAcIn { get; set; }
    public double Voltage9vIn { get; set; }
    public bool DoorOpen { get; set; }
    public bool FlashingYellowOn { get; set; }
    public bool CpuConnected { get; set; }
    public bool TcpConnected { get; set; }
    public string ClientIpV4 { get; set; } = string.Empty;
    public int ClientPort { get; set; }
    public DateTime CreatedAt { get; set; }
}

