using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace desafioConcilig.Models
{
    public class Importacao
    {
        [Display(Name = "Nova Importação")]
        public string importacao {get; set;}
    }
}