namespace SNIESWebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("Periodo")]
    public class Periodo : Entidad
    {
        [Display(Name = "Periodo")]
        public string FechaPeriodo { get; set; }
    }
}