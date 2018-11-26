namespace SNIESWebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("EducacionContinua")]
    public class EducacionContinua : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string AÑO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CÓDIGO CURSO")] public string CODIGO_CURSO { get; set; }
        [Display(Name = "NOMBRE CURSO")] public string NOMBRE_CURSO { get; set; }
        [Display(Name = "NÚMERO HORAS")] public string NUMERO_HORAS { get; set; }
        [Display(Name = "ID TIPO CURSO EXTENSIÓN")] public string ID_TIPO_CURSO_EXTENSION { get; set; }
        [Display(Name = "TIPO CURSO EXTENSIÓN")] public string TIPO_CURSO_EXTENSION { get; set; }
        [Display(Name = "VALOR CURSO")] public string VALOR_CURSO { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }

    [Table("DocenteEducacionContinua")]
    public class DocenteEducacionContinua : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string AÑO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CÓDIGO CURSO")] public string CODIGO_CURSO { get; set; }
        [Display(Name = "NOMBRE CURSO")] public string NOMBRE_CURSO { get; set; }
        [Display(Name = "ID TIPO DOCUMENTO")] public string ID_TIPO_DOCUMENTO { get; set; }
        [Display(Name = "NÚMERO DOCUMENTO")] public string NUMERO_DOCUMENTO { get; set; }
        [Display(Name = "PRIMER NOMBRE")] public string PRIMER_NOMBRE { get; set; }
        [Display(Name = "SEGUNDO NOMBRE")] public string SEGUNDO_NOMBRE { get; set; }
        [Display(Name = "PRIMER APELLIDO")] public string PRIMER_APELLIDO { get; set; }
        [Display(Name = "SEGUNDO APELLIDO")] public string SEGUNDO_APELLIDO { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }

    [Table("BeneficioEducacionContinua")]
    public class BeneficioEducacionContinua : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string AÑO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CÓDIGO CURSO")] public string CODIGO_CURSO { get; set; }
        [Display(Name = "NOMBRE CURSO")] public string NOMBRE_CURSO { get; set; }
        [Display(Name = "ID TIPO BENEFICIARIO")] public string ID_TIPO_BENEFICIARIO { get; set; }
        [Display(Name = "TIPO BENEFICIARIO")] public string TIPO_BENEFICIARIO { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }

    }
}