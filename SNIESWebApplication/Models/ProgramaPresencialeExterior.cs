namespace SNIESWebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("ProgramaPresencialeExterior")]
    public class ProgramaPresencialeExterior : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "ANO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CÓDIGO PROGRAMA")] public string CODIGO_PROGRAMA { get; set; }
        [Display(Name = "PROGRAMA")] public string PROGRAMA { get; set; }
        [Display(Name = "CINE CAMPO DETALLADO")] public string CINE_CAMPO_DETALLADO { get; set; }
        [Display(Name = "PAÍS")] public string PAIS { get; set; }
        [Display(Name = "MODALIDAD")] public string MODALIDAD { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
}