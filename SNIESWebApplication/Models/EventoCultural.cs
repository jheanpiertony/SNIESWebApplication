namespace SNIESWebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("EventoCultural")]
    public class EventoCultural : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string AÑO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CÓDIGO UNIDAD")] public string CODIGO_UNIDAD { get; set; }
        [Display(Name = "CÓDIGO EVENTO")] public string CODIGO_EVENTO { get; set; }
        [Display(Name = "EVENTO")] public string EVENTO { get; set; }
        [Display(Name = "FECHA INICIO")] public string FECHA_INICIO { get; set; }
        [Display(Name = "FECHA FINAL")] public string FECHA_FINAL { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }

    [Table("FteNacionEventoCultural")]
    public class FteNacionEventoCultural : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string AÑO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CÓDIGO UNIDAD")] public string CODIGO_UNIDAD { get; set; }
        [Display(Name = "CÓDIGO EVENTO")] public string CODIGO_EVENTO { get; set; }
        [Display(Name = "ID FUENTE NACIONAL")] public string ID_FUENTE_NACIONAL { get; set; }
        [Display(Name = "FUENTE NACIONAL")] public string FUENTE_NACIONAL { get; set; }
        [Display(Name = "VALOR FINANCIACIÓN")] public string VALOR_FINANCIACION { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }

    [Table("FteInternEventoCultural")]
    public class FteInternEventoCultural : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string AÑO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CÓDIGO UNIDAD")] public string CODIGO_UNIDAD { get; set; }
        [Display(Name = "CÓDIGO EVENTO")] public string CODIGO_EVENTO { get; set; }
        [Display(Name = "EVENTO")] public string EVENTO { get; set; }
        [Display(Name = "NOMBRE INSTITUCIÓN")] public string NOMBRE_INSTITUCION { get; set; }
        [Display(Name = "ID PAÍS")] public string ID_PAIS { get; set; }
        [Display(Name = "PAÍS")] public string PAIS { get; set; }
        [Display(Name = "ID FUENTE INTERNACIONAL")] public string ID_FUENTE_INTERNACIONAL { get; set; }
        [Display(Name = "FUENTE INTERNACIONAL")] public string FUENTE_INTERNACIONAL { get; set; }
        [Display(Name = "VALOR FINANCIACIÓN")] public string VALOR_FINANCIACION { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }

    [Table("RecHumanoEventoCultural")]
    public class RecHumanoEventoCultural : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string AÑO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CÓDIGO UNIDAD")] public string CODIGO_UNIDAD { get; set; }
        [Display(Name = "CÓDIGO EVENTO")] public string CODIGO_EVENTO { get; set; }
        [Display(Name = "EVENTO")] public string EVENTO { get; set; }
        [Display(Name = "ID TIPO DOCUMENTO")] public string ID_TIPO_DOCUMENTO { get; set; }
        [Display(Name = "TIPO DOCUMENTO")] public string TIPO_DOCUMENTO { get; set; }
        [Display(Name = "NUMERO DOCUMENTO")] public string NUMERO_DOCUMENTO { get; set; }
        [Display(Name = "PRIMER NOMBRE")] public string PRIMER_NOMBRE { get; set; }
        [Display(Name = "SEGUNDO NOMBRE")] public string SEGUNDO_NOMBRE { get; set; }
        [Display(Name = "PRIMER APELLIDO")] public string PRIMER_APELLIDO { get; set; }
        [Display(Name = "SEGUNDO APELLIDO")] public string SEGUNDO_APELLIDO { get; set; }
        [Display(Name = "DEDICACIÓN")] public string DEDICACION { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }

    [Table("BeneficiarEventoCultural")]
    public class BeneficiarEventoCultural : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string AÑO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CÓDIGO UNIDAD")] public string CODIGO_UNIDAD { get; set; }
        [Display(Name = "CÓDIGO EVENTO")] public string CODIGO_EVENTO { get; set; }
        [Display(Name = "ID TIPO BENEFICIARIO")] public string ID_TIPO_BENEFICIARIO { get; set; }
        [Display(Name = "TIPO BENEFICIARIO")] public string TIPO_BENEFICIARIO { get; set; }
        [Display(Name = "TOTAL ASISTENTES")] public string TOTAL_ASISTENTES { get; set; }
        [Display(Name = "VALOR ENTRADA")] public string VALOR_ENTRADA { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
}