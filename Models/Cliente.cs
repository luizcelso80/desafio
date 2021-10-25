using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace concilig.Models
{
    public class Cliente
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Cpf")]
        public string cpf { get; set; }

        [Display(Name = "Nome")]
        public string nome { get; set; }
        public ICollection<Contrato> Contratos {get; set;}
    }
}