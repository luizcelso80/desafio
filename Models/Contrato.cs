using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace concilig.Models
{
    public class Contrato
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Referência")]
        public string referencia { get; set; }
        [Display(Name = "Valor")]
        [DataType(DataType.Currency)]
        public double valor { get; set; }

        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de vencimento")]
        public DateTime dataVencimento { get; set; }
        public Cliente Cliente { get; set; }
        public Produto Produto { get; set; }
    }
}