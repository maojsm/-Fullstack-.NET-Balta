using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Requests.TcHardwareInRealtime
{
    public class CreateTcHardwareInRealtimeRequest
    {
        [Required(ErrorMessage = "Controlador de tráfego inválido")]
        [DefaultValue(1)]
        public long TrafficControllerId { get; set; }

        [Required(ErrorMessage = "Tensão AC inválida")]
        [DefaultValue(219.3)]
        public double VoltageAcIn { get; set; }

        [Required(ErrorMessage = "Tensão 9v inválida")]
        [DefaultValue(9.2)]
        public double Voltage9vIn { get; set; }

        [Required(ErrorMessage = "Porta aberta inválida")]
        [DefaultValue(false)]
        public bool DoorOpen { get; set; }

        [Required(ErrorMessage = "Luz amarela piscando inválida")]
        [DefaultValue(false)]
        public bool FlashingYellowOn { get; set; }

        [Required(ErrorMessage = "Conexão com a CPU inválida")]
        [DefaultValue(true)]
        public bool CpuConnected { get; set; }
    }
}
