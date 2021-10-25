using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace concilig.Models
{
    public class Produto
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Descrição")]
        public string descricao { get; set; }

        public ICollection<Contrato> Contratos {get; set;}
    }
}