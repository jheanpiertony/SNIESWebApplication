namespace SNIESWebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("Docente")]
    public class Docente : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "ANO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "ID TIPO DOCUMENTO")] public string ID_TIPO_DOCUMENTO { get; set; }
        [Display(Name = "TIPO DOCUMENTO")] public string TIPO_DOCUMENTO { get; set; }
        [Display(Name = "NUMERO DOCUMENTO")] public string NUMERO_DOCUMENTO { get; set; }
        [Display(Name = "PRIMER NOMBRE")] public string PRIMER_NOMBRE { get; set; }
        [Display(Name = "SEGUNDO NOMBRE")] public string SEGUNDO_NOMBRE { get; set; }
        [Display(Name = "PRIMER APELLIDO")] public string PRIMER_APELLIDO { get; set; }
        [Display(Name = "SEGUNDO APELLIDO")] public string SEGUNDO_APELLIDO { get; set; }
        [Display(Name = "FECHA NACIMIENTO")] public string FECHA_NACIMIENTO { get; set; }
        [Display(Name = "PAIS NACIMIENTO")] public string PAIS_NACIMIENTO { get; set; }
        [Display(Name = "MUNICIPIO NACIMIENTO")] public string MUNICIPIO_NACIMIENTO { get; set; }
        [Display(Name = "NIVEL MAXIMO ESTUDIO")] public string NIVEL_MAXIMO_ESTUDIO { get; set; }
        [Display(Name = "TITULO RECIBIDO")] public string TITULO_RECIBIDO { get; set; }
        [Display(Name = "FECHA GRADO")] public string FECHA_GRADO { get; set; }
        [Display(Name = "PAIS INSTITUCION ESTUDIO")] public string PAIS_INSTITUCION_ESTUDIO { get; set; }
        [Display(Name = "TITULO CONVALIDADO")] public string TITULO_CONVALIDADO { get; set; }
        [Display(Name = "ID IES ESTUDIO")] public string ID_IES_ESTUDIO { get; set; }
        [Display(Name = "NOMBRE INSTITUCION ESTUDIO")] public string NOMBRE_INSTITUCION_ESTUDIO { get; set; }
        [Display(Name = "METODOLOGIA PROGRAMA")] public string METODOLOGIA_PROGRAMA { get; set; }
        [Display(Name = "FECHA INGRESO")] public string FECHA_INGRESO { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }

    [Table("CapacitacionDocente")]
    public class CapacitacionDocente : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "ANO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "ID TIPO DOCUMENTO")] public string ID_TIPO_DOCUMENTO { get; set; }
        [Display(Name = "TIPO DOCUMENTO")] public string TIPO_DOCUMENTO { get; set; }
        [Display(Name = "NÚMERO DOCUMENTO")] public string NUMERO_DOCUMENTO { get; set; }
        [Display(Name = "PRIMER NOMBRE")] public string PRIMER_NOMBRE { get; set; }
        [Display(Name = "PRIMER APELLIDO")] public string PRIMER_APELLIDO { get; set; }
        [Display(Name = "SEGUNDO APELLIDO")] public string SEGUNDO_APELLIDO { get; set; }
        [Display(Name = "ID TIPO CAPACITACIÓN")] public string ID_TIPO_CAPACITACION { get; set; }
        [Display(Name = "TIPO CAPACITACIÓN")] public string TIPO_CAPACITACION { get; set; }
        [Display(Name = "NO HORAS CURSADAS")] public string NO_HORAS_CURSADAS { get; set; }
        [Display(Name = "ID TIPO CURSO")] public string ID_TIPO_CURSO { get; set; }
        [Display(Name = "TIPO CURSO")] public string TIPO_CURSO { get; set; }
        [Display(Name = "ID TEMA CURSO")] public string ID_TEMA_CURSO { get; set; }
        [Display(Name = "TEMA CURSO")] public string TEMA_CURSO { get; set; }
        [Display(Name = "ID PAIS")] public string ID_PAIS { get; set; }
        [Display(Name = "PAIS")] public string PAIS { get; set; }
        [Display(Name = "NOMBRE CURSO")] public string NOMBRE_CURSO { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }

    [Table("ContratoDocente")]
    public class ContratoDocente : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "ANO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "ID TIPO DOCUMENTO")] public string ID_TIPO_DOCUMENTO { get; set; }
        [Display(Name = "TIPO DOCUMENTO")] public string TIPO_DOCUMENTO { get; set; }
        [Display(Name = "NUMERO DOCUMENTO")] public string NUMERO_DOCUMENTO { get; set; }
        [Display(Name = "PRIMER NOMBRE")] public string PRIMER_NOMBRE { get; set; }
        [Display(Name = "PRIMER APELLIDO")] public string PRIMER_APELLIDO { get; set; }
        [Display(Name = "SEGUNDO APELLIDO")] public string SEGUNDO_APELLIDO { get; set; }
        [Display(Name = "PORCENTAJE DOCENCIA")] public string PORCENTAJE_DOCENCIA { get; set; }
        [Display(Name = "PORCENTAJE INVESTIGACIÓN")] public string PORCENTAJE_INVESTIGACION { get; set; }
        [Display(Name = "PORCENTAJE ADMINISTRATIVA")] public string PORCENTAJE_ADMINISTRATIVA { get; set; }
        [Display(Name = "PORCENTAJE EXTENSIÓN")] public string PORCENTAJE_EXTENSION { get; set; }
        [Display(Name = "PORCENTAJE OTRAS ACTIVIDADES")] public string PORCENTAJE_OTRAS_ACTIVIDADES { get; set; }
        [Display(Name = "HORAS DEDICACIÓN SEMESTRE")] public string HORAS_DEDICACION_SEMESTRE { get; set; }
        [Display(Name = "DEDICACIÓN")] public string DEDICACION { get; set; }
        [Display(Name = "TIPO CONTRATO")] public string TIPO_CONTRATO { get; set; }
        [Display(Name = "ASIGNACIÓN BASICA MENSUAL")] public string ASIGNACION_BASICA_MENSUAL { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
}