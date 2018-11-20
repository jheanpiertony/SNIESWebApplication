namespace SNIESWebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("Matriculado")]
    public class Matriculado : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "ID TIPO DOCUMENTO")] public string ID_TIPO_DOCUMENTO { get; set; }
        [Display(Name = "TIPO DOCUMENTO")] public string TIPO_DOCUMENTO { get; set; }
        [Display(Name = "NUMERO DOCUMENTO")] public string NUMERO_DOCUMENTO { get; set; }
        [Display(Name = "CODIGO ESTUDIANTE")] public string CODIGO_ESTUDIANTE { get; set; }
        [Display(Name = "SEXO BIOLOGICO")] public string SEXO_BIOLOGICO { get; set; }
        [Display(Name = "PRIMER NOMBRE")] public string PRIMER_NOMBRE { get; set; }
        [Display(Name = "SEGUNDO NOMBRE")] public string SEGUNDO_NOMBRE { get; set; }
        [Display(Name = "PRIMER APELLIDO")] public string PRIMER_APELLIDO { get; set; }
        [Display(Name = "SEGUNDO APELLIDO")] public string SEGUNDO_APELLIDO { get; set; }
        [Display(Name = "PROGRAMA CONSECUTIVO")] public string PROGRAMA_CONSECUTIVO { get; set; }
        [Display(Name = "PROGRAMA")] public string PROGRAMA { get; set; }
        [Display(Name = "COD DANE")] public string COD_DANE { get; set; }
        [Display(Name = "DEPARTAMENTO")] public string DEPARTAMENTO { get; set; }
        [Display(Name = "MUNICIPIO")] public string MUNICIPIO { get; set; }
        [Display(Name = "FECHA NACIMIENTO")] public string FECHA_NACIMIENTO { get; set; }
        [Display(Name = "ID PAIS")] public string ID_PAIS { get; set; }
        [Display(Name = "PAIS")] public string PAIS { get; set; }
        [Display(Name = "COD DANE NACIMIENTO")] public string COD_DANE_NACIMIENTO { get; set; }
        [Display(Name = "DEPARTAMENTO NACIMIENTO")] public string DEPARTAMENTO_NACIMIENTO { get; set; }
        [Display(Name = "MUNICIPIO NACIMIENTO")] public string MUNICIPIO_NACIMIENTO { get; set; }
        [Display(Name = "ID ZONA RESIDENCIA")] public string ID_ZONA_RESIDENCIA { get; set; }
        [Display(Name = "ZONA RESIDENCIA")] public string ZONA_RESIDENCIA { get; set; }
        [Display(Name = "NUMERO MATERIAS INSCRITAS")] public string NUMERO_MATERIAS_INSCRITAS { get; set; }
        [Display(Name = "NUMERO MATERIAS APROBADAS")] public string NUMERO_MATERIAS_APROBADAS { get; set; }
        [Display(Name = "ES REINTEGRO ESTD ANTES DE 1998")] public string ES_REINTEGRO_ESTD_ANTES_DE1998 { get; set; }
        [Display(Name = "AÑO PRIMER CURSO")] public string ANO_PRIMER_CURSO { get; set; }
        [Display(Name = "SEMESTRE PRIMER CURSO")] public string SEMESTRE_PRIMER_CURSO { get; set; }
        [Display(Name = "FUENTE")] public string FUENTE { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
}