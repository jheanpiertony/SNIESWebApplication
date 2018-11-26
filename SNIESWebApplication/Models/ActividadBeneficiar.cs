namespace SNIESWebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("ActividadBeneficiar")]
    public class ActividadBeneficiar : Entidad
    {
        [Display(Name = "ID IES")] public string ID_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "ANO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "COD UNIDAD")] public string COD_UNIDAD { get; set; }
        [Display(Name = "UNIDAD ORGANIZACIONAL")] public string UNIDAD_ORGANIZACIONAL { get; set; }
        [Display(Name = "COD ACTIVIDAD")] public string COD_ACTIVIDAD { get; set; }
        [Display(Name = "ACTIVIDAD")] public string ACTIVIDAD { get; set; }
        [Display(Name = "COD TIPO BENEFICIARIO")] public string COD_TIPO_BENEFICIARIO { get; set; }
        [Display(Name = "TIPO BENEFICIARIO")] public string TIPO_BENEFICIARIO { get; set; }
        [Display(Name = "CANTIDAD BENEFICIARIO")] public string CANTIDAD_BENEFICIARIO { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }


    }
}