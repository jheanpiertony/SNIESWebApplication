namespace SNIESWebApplication.Models
{
    using SNIESWebApplication.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("ActividadCultural")]
    public class ActividadCultural : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CÓD UNIDAD ORGANIZACIONAL")] public string COD_UNIDAD_ORGANIZACIONAL { get; set; }
        [Display(Name = "UNIDAD ORGANIZACIONAL")] public string UNIDAD_ORGANIZACIONAL { get; set; }
        [Display(Name = "CÓDIGO ACTIVIDAD")] public string CODIGO_ACTIVIDAD { get; set; }
        [Display(Name = "ACTIVIDAD")] public string ACTIVIDAD { get; set; }
        [Display(Name = "CÓD TIPO ACTIVIDAD")] public string COD_TIPO_ACTIVIDAD { get; set; }
        [Display(Name = "TIPO ACTIVIDAD")] public string TIPO_ACTIVIDAD { get; set; }
        [Display(Name = "FECHA INICIO")] public string FECHA_INICIO { get; set; }
        [Display(Name = "FECHA FINAL")] public string FECHA_FINAL { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }

    [Table("RecHumanoCultural")]
    public class RecHumanoCultural : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CÓD UNIDAD ORGANIZACIONAL")] public string COD_UNIDAD_ORGANIZACIONAL { get; set; }
        [Display(Name = "UNIDAD ORGANIZACIONAL")] public string UNIDAD_ORGANIZACIONAL { get; set; }
        [Display(Name = "CÓDIGO ACTIVIDAD")] public string CODIGO_ACTIVIDAD { get; set; }
        [Display(Name = "ACTIVIDAD")] public string ACTIVIDAD { get; set; }
        [Display(Name = "CÓD TIPO ACTIVIDAD")] public string COD_TIPO_ACTIVIDAD { get; set; }
        [Display(Name = "TIPO ACTIVIDAD")] public string TIPO_ACTIVIDAD { get; set; }
        [Display(Name = "FECHA INICIO")] public string FECHA_INICIO { get; set; }
        [Display(Name = "FECHA FINAL")] public string FECHA_FINAL { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
}