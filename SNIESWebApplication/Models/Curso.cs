namespace SNIESWebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("Curso")]
    public class Curso : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "CODIGO CURSO")] public string CODIGO_CURSO { get; set; }
        [Display(Name = "NOMBRE CURSO")] public string NOMBRE_CURSO { get; set; }
        [Display(Name = "COD CINE")] public string COD_CINE { get; set; }
        [Display(Name = "CINE CAMPO DETALLADO")] public string CINE_CAMPO_DETALLADO { get; set; }
        [Display(Name = "EXTENSIÓN")] public string EXTENSION { get; set; }
    }
}