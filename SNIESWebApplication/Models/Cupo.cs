namespace SNIESWebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("Cupo")]
    public class Cupo : Entidad
    {
        [Display(Name = "CODIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "PRO CONSECUTIVO")] public string PRO_CONSECUTIVO { get; set; }
        [Display(Name = "PROGRAM NOMBRE")] public string PROGRAM_NOMBRE { get; set; }
        [Display(Name = "CÓDIGO MUNICIPIO")] public string CODIGO_MUNICIPIO { get; set; }
        [Display(Name = "NOMBRE MUNICIPIO")] public string NOMBRE_MUNICIPIO { get; set; }
        [Display(Name = "CUPOS NUEVOS PROYECTADOS")] public string CUPOS_NUEVOS_PROYECTADOS { get; set; }
        [Display(Name = "CUPOS TOTALES PROYECTADOS")] public string CUPOS_TOTALES_PROYECTADOS { get; set; }
        [Display(Name = "MATRÍCULA TOTAL ESPERADA")] public string MATRICULA_TOTAL_ESPERADA { get; set; }
        [Display(Name = "FUENTE")] public string FUENTE { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
}