namespace SNIESWebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("ActividadBienestar")]
    public class ActividadBienestar : Entidad
    {
        [Display(Name = "ID IES")] public string ID_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "ANO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "COD UNIDAD")] public string COD_UNIDAD { get; set; }
        [Display(Name = "UNIDAD ORGANIZACIONAL")] public string UNIDAD_ORGANIZACIONAL { get; set; }
        [Display(Name = "COD ACTIVIDAD")] public string COD_ACTIVIDAD { get; set; }
        [Display(Name = "ACTIVIDAD")] public string ACTIVIDAD { get; set; }
        [Display(Name = "COD TIPO ACTIVIDAD")] public string COD_TIPO_ACTIVIDAD { get; set; }
        [Display(Name = "TIPO ACTIVIDAD")] public string TIPO_ACTIVIDAD { get; set; }
        [Display(Name = "FECHA INICIO")] public string FECHA_INICIO { get; set; }
        [Display(Name = "FECHA FINAL")] public string FECHA_FINAL { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
}