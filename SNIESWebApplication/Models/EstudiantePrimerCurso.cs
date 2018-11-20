namespace SNIESWebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("EstudiantePrimerCurso")]
    public class EstudiantePrimerCurso : Entidad
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
        [Display(Name = "NOMBRE PROGRAMA")] public string NOMBRE_PROGRAMA { get; set; }
        [Display(Name = "ID MUNICIPIO")] public string ID_MUNICIPIO { get; set; }
        [Display(Name = "DEPARTAMENTO")] public string DEPARTAMENTO { get; set; }
        [Display(Name = "MUNICIPIO")] public string MUNICIPIO { get; set; }
        [Display(Name = "ID TIPO VINCULACIÓN")] public string ID_TIPO_VINCULACION { get; set; }
        [Display(Name = "TIPO VINCULACIÓN")] public string TIPO_VINCULACION { get; set; }
        [Display(Name = "ID GRUPO ÉTNICO")] public string ID_GRUPO_ETNICO { get; set; }
        [Display(Name = "GRUPO ÉTNICO")] public string GRUPO_ETNICO { get; set; }
        [Display(Name = "ID PUEBLO INDIGENA")] public string ID_PUEBLO_INDIGENA { get; set; }
        [Display(Name = "PUEBLO INDIGENA")] public string PUEBLO_INDIGENA { get; set; }
        [Display(Name = "ID COMINIDAD NEGRA")] public string ID_COMINIDAD_NEGRA { get; set; }
        [Display(Name = "COMUNIDAD NEGRA")] public string COMUNIDAD_NEGRA { get; set; }
        [Display(Name = "PERSONA CON DISCAPACIDAD")] public string PERSONA_CON_DISCAPACIDAD { get; set; }
        [Display(Name = "ID TIPO DISCAPACIDAD")] public string ID_TIPO_DISCAPACIDAD { get; set; }
        [Display(Name = "ID CAPACIDAD EXCEP")] public string ID_CAPACIDAD_EXCEP { get; set; }
        [Display(Name = "CAPACIDAD EXCEPCIONAL")] public string CAPACIDAD_EXCEPCIONAL { get; set; }
        [Display(Name = "COD PRUEBA SABER 11")] public string COD_PRUEBA_SABER_11 { get; set; }
        [Display(Name = "FUENTE")] public string FUENTE { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }

    }
}