using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Requests.TrafficController;

public class CreateTrafficControllerRequest : Request
{
    [Required(ErrorMessage = "Código inválido")]
    [MaxLength(10, ErrorMessage = "O código deve conter até 10 caracteres")]
    public string CodeLocal { get; set; } = string.Empty;

    [Required(ErrorMessage = "Nome inválido")]
    [MaxLength(30, ErrorMessage = "O nome deve conter até 30 caracteres")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descrição inválida")]
    [MaxLength(100, ErrorMessage = "A descrição deve conter até 100 caracteres")]
    public string Description { get; set; } = string.Empty;
}
