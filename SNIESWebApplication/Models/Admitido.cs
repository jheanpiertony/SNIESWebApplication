namespace SNIESWebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("Admitido")]
    public class Admitido : Entidad
    {
        [Display(Name = "ID IES")] public string ID_IES { get; set; }
        [Display(Name = "NOMBRE COMPLETO")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "COD DANE")] public string COD_DANE { get; set; }
        [Display(Name = "DEPARTAMENTO")] public string DEPARTAMENTO { get; set; }
        [Display(Name = "MUNICIPIO")] public string MUNICIPIO { get; set; }
        [Display(Name = "PRO CONSECUTIVO")] public string PRO_CONSECUTIVO { get; set; }
        [Display(Name = "PROGRAMA")] public string PROGRAMA { get; set; }
        [Display(Name = "TIPO DOCUMENTO")] public string TIPO_DOCUMENTO { get; set; }
        [Display(Name = "NÚMERO DOCUMENTO")] public string NUMERO_DOCUMENTO { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
}