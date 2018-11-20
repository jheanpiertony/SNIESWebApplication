namespace SNIESWebApplication.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ApoyoEstudiante")]
    public class ApoyoEstudiante : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "ID TIPO DOCUMENTO")] public string ID_TIPO_DOCUMENTO { get; set; }
        [Display(Name = "TIPO DOCUMENTO")] public string TIPO_DOCUMENTO { get; set; }
        [Display(Name = "NUMERO DOCUMENTO")] public string NUMERO_DOCUMENTO { get; set; }
        [Display(Name = "PRIMER NOMBRE")] public string PRIMER_NOMBRE { get; set; }
        [Display(Name = "SEGUNDO NOMBRE")] public string SEGUNDO_NOMBRE { get; set; }
        [Display(Name = "PRIMER APELLIDO")] public string PRIMER_APELLIDO { get; set; }
        [Display(Name = "SEGUNDO APELLIDO")] public string SEGUNDO_APELLIDO { get; set; }
        [Display(Name = "PROGRAMA CONSECUTIVO")] public string PROGRAMA_CONSECUTIVO { get; set; }
        [Display(Name = "PROGRAMA NOMBRE")] public string PROGRAMA_NOMBRE { get; set; }
        [Display(Name = "ID MUNICIPIO")] public string ID_MUNICIPIO { get; set; }
        [Display(Name = "DEPARTAMENTO")] public string DEPARTAMENTO { get; set; }
        [Display(Name = "MUNICIPIO")] public string MUNICIPIO { get; set; }
        [Display(Name = "RECIBIO APOYO FINANCIERO")] public string RECIBIO_APOYO_FINANCIERO { get; set; }
        [Display(Name = "RECIBIO APOYO ACADEMICO")] public string RECIBIO_APOYO_ACADEMICO { get; set; }
        [Display(Name = "RECIBIO OTROS APOYOS")] public string RECIBIO_OTROS_APOYOS { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
}