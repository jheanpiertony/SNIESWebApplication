namespace SNIESWebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("Consultaria")]
    public class Consultaria : Entidad
    {
        [Display(Name = "ID IES")] public string ID_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string AÑO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "COD CONSULTORIA")] public string COD_CONSULTORIA { get; set; }
        [Display(Name = "DESC CONSULTORIA")] public string DESC_CONSULTORIA { get; set; }
        [Display(Name = "COD CINE")] public string COD_CINE { get; set; }
        [Display(Name = "CINE CAMPO DETALLADO")] public string CINE_CAMPO_DETALLADO { get; set; }
        [Display(Name = "NOMBRE INSTITUCIÓN")] public string NOMBRE_INSTITUCION { get; set; }
        [Display(Name = "COD SECTOR")] public string COD_SECTOR { get; set; }
        [Display(Name = "SECTOR")] public string SECTOR { get; set; }
        [Display(Name = "VALOR")] public string VALOR { get; set; }
        [Display(Name = "FECHA INICIO")] public string FECHA_INICIO { get; set; }
        [Display(Name = " FECHA FINAL")] public string _FECHA_FINAL { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }

    [Table("RecHumanoConsultoria")]
    public class RecHumanoConsultoria : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string AÑO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CODIGO CONSULTORIA")] public string CODIGO_CONSULTORIA { get; set; }
        [Display(Name = "DESCRIPCIÓN CONSULTORIA")] public string DESCRIPCION_CONSULTORIA { get; set; }
        [Display(Name = "ID TIPO DOCUMENTO")] public string ID_TIPO_DOCUMENTO { get; set; }
        [Display(Name = "NUMERO DOCUMENTO")] public string NUMERO_DOCUMENTO { get; set; }
        [Display(Name = "PRIMER NOMBRE")] public string PRIMER_NOMBRE { get; set; }
        [Display(Name = "SEGUNDO NOMBRE")] public string SEGUNDO_NOMBRE { get; set; }
        [Display(Name = "PRIMER APELLIDO")] public string PRIMER_APELLIDO { get; set; }
        [Display(Name = "SEGUNDO APELLIDO")] public string SEGUNDO_APELLIDO { get; set; }
        [Display(Name = "NIVEL ESTUDIO")] public string NIVEL_ESTUDIO { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
}