namespace SNIESWebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("ActividadRecHumano")]
    public class ActividadRecHumano : Entidad
    {
        [Display(Name = "ID IES")] public string ID_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "ANO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "COD UNIDAD")] public string COD_UNIDAD { get; set; }
        [Display(Name = "UNIDAD ORGANIZACIONAL")] public string UNIDAD_ORGANIZACIONAL { get; set; }
        [Display(Name = "COD ACTIVIDAD")] public string COD_ACTIVIDAD { get; set; }
        [Display(Name = "ACTIVIDAD")] public string ACTIVIDAD { get; set; }
        [Display(Name = "TIPO DOCUMENTO")] public string TIPO_DOCUMENTO { get; set; }
        [Display(Name = "NÚMERO DOCUMENTO")] public string NUMERO_DOCUMENTO { get; set; }
        [Display(Name = "PRIMER NOMBRE")] public string PRIMER_NOMBRE { get; set; }
        [Display(Name = "SEGUNDO NOMBRE")] public string SEGUNDO_NOMBRE { get; set; }
        [Display(Name = "PRIMER APELLIDO")] public string PRIMER_APELLIDO { get; set; }
        [Display(Name = "SEGUNDO APELLIDO")] public string SEGUNDO_APELLIDO { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
}