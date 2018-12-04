namespace SNIESWebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("ServicioExtension")]
    public class ServicioExtension : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CODIGO UNIDAD ORGANIZACIONAL")] public string CODIGO_UNIDAD_ORGANIZACIONAL { get; set; }
        [Display(Name = "CODIGO SERVICIO")] public string CODIGO_SERVICIO { get; set; }
        [Display(Name = "NOMBRE SERVICIO")] public string NOMBRE_SERVICIO { get; set; }
        [Display(Name = "DESCRIPCION SERVICIO")] public string DESCRIPCION_SERVICIO { get; set; }
        [Display(Name = "VALOR SERVICIO")] public string VALOR_SERVICIO { get; set; }
        [Display(Name = "AREA EXTENSION")] public string AREA_EXTENSION { get; set; }
        [Display(Name = "FECHA INICIO")] public string FECHA_INICIO { get; set; }
        [Display(Name = "FECHA FINAL")] public string FECHA_FINAL { get; set; }
        [Display(Name = "NOMBRE CONTACTO")] public string NOMBRE_CONTACTO { get; set; }
        [Display(Name = "APELLIDO CONTACTO")] public string APELLIDO_CONTACTO { get; set; }
        [Display(Name = "TELEFONO CONTACTO")] public string TELEFONO_CONTACTO { get; set; }
        [Display(Name = "EMAIL CONTACTO")] public string EMAIL_CONTACTO { get; set; }
        [Display(Name = "TIENE COSTO")] public string TIENE_COSTO { get; set; }
        [Display(Name = "CRITERIO ELEGIBILIDAD")] public string CRITERIO_ELEGIBILIDAD { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
    [Table("AreaTrabajoServicioExtension")]
    public class AreaTrabajoServicioExtension : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CODIGO UNIDAD ORGANIZACIONAL")] public string CODIGO_UNIDAD_ORGANIZACIONAL { get; set; }
        [Display(Name = "CODIGO SERVICIO")] public string CODIGO_SERVICIO { get; set; }
        [Display(Name = "NOMBRE SERVICIO")] public string NOMBRE_SERVICIO { get; set; }
        [Display(Name = "ÁREA TRABAJO")] public string ÁREA_TRABAJO { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
    [Table("CicloVitalServicioExtension")]
    public class CicloVitalServicioExtension : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CODIGO UNIDAD ORGANIZACIONAL")] public string CODIGO_UNIDAD_ORGANIZACIONAL { get; set; }
        [Display(Name = "CODIGO SERVICIO")] public string CODIGO_SERVICIO { get; set; }
        [Display(Name = "NOMBRE SERVICIO")] public string NOMBRE_SERVICIO { get; set; }
        [Display(Name = "CICLO VITAL")] public string CICLO_VITAL { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
    [Table("EntidadNacionalServicioExtension")]
    public class EntidadNacionalServicioExtension : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CODIGO UNIDAD ORGANIZACIONAL")] public string CODIGO_UNIDAD_ORGANIZACIONAL { get; set; }
        [Display(Name = "CODIGO SERVICIO")] public string CODIGO_SERVICIO { get; set; }
        [Display(Name = "NOMBRE SERVICIO")] public string NOMBRE_SERVICIO { get; set; }
        [Display(Name = "ENTIDAD NACIONAL")] public string ENTIDAD_NACIONAL { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
    [Table("FuenteInternacionalServicioExtension")]
    public class FuenteInternacionalServicioExtension : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CODIGO UNIDAD ORGANIZACIONAL")] public string CODIGO_UNIDAD_ORGANIZACIONAL { get; set; }
        [Display(Name = "CODIGO SERVICIO")] public string CODIGO_SERVICIO { get; set; }
        [Display(Name = "NOMBRE SERVICIO")] public string NOMBRE_SERVICIO { get; set; }
        [Display(Name = "NOMBRE INSTITUCIÓN")] public string NOMBRE_INSTITUCION { get; set; }
        [Display(Name = "PAÍS")] public string PAIS { get; set; }
        [Display(Name = "FUENTE INTERNACIONAL")] public string FUENTE_INTERNACIONAL { get; set; }
        [Display(Name = "VALOR FINANCIACIÓN")] public string VALOR_FINANCIACION { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
    [Table("FuenteNacionalServicioExtension")]
    public class FuenteNacionalServicioExtension : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string AÑO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CODIGO UNIDAD ORGANIZACIONAL")] public string CODIGO_UNIDAD_ORGANIZACIONAL { get; set; }
        [Display(Name = "CODIGO SERVICIO")] public string CODIGO_SERVICIO { get; set; }
        [Display(Name = "NOMBRE SERVICIO")] public string NOMBRE_SERVICIO { get; set; }
        [Display(Name = "FUENTE NACIONAL")] public string FUENTE_NACIONAL { get; set; }
        [Display(Name = "VALOR FINANCIACIÓN")] public string VALOR_FINANCIACION { get; set; }

        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
    [Table("OtraEntidadServicioExtension")]
    public class OtraEntidadServicioExtension : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CODIGO UNIDAD ORGANIZACIONAL")] public string CODIGO_UNIDAD_ORGANIZACIONAL { get; set; }
        [Display(Name = "CODIGO SERVICIO")] public string CODIGO_SERVICIO { get; set; }
        [Display(Name = "NOMBRE SERVICIO")] public string NOMBRE_SERVICIO { get; set; }
        [Display(Name = "NOMBRE ENTIDAD")] public string NOMBRE_ENTIDAD { get; set; }
        [Display(Name = "PAÍS")] public string PAIS { get; set; }
        [Display(Name = "SECTOR ENTIDAD")] public string SECTOR_ENTIDAD { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
    [Table("PoblacionCondicionalServicioExtension")]
    public class PoblacionCondicionalServicioExtension : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CODIGO UNIDAD ORGANIZACIONAL")] public string CODIGO_UNIDAD_ORGANIZACIONAL { get; set; }
        [Display(Name = "CODIGO SERVICIO")] public string CODIGO_SERVICIO { get; set; }
        [Display(Name = "NOMBRE SERVICIO")] public string NOMBRE_SERVICIO { get; set; }
        [Display(Name = "POBLACIÓN CONDICIÓN")] public string POBLACION_CONDICION { get; set; }
        [Display(Name = "CANTIDAD")] public string CANTIDAD { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
    [Table("PoblacionGrupoServicioExtension")]
    public class PoblacionGrupoServicioExtension : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CODIGO UNIDAD ORGANIZACIONAL")] public string CODIGO_UNIDAD_ORGANIZACIONAL { get; set; }
        [Display(Name = "CODIGO SERVICIO")] public string CODIGO_SERVICIO { get; set; }
        [Display(Name = "NOMBRE SERVICIO")] public string NOMBRE_SERVICIO { get; set; }
        [Display(Name = "POBLACIÓN GRUPO")] public string POBLACION_GRUPO { get; set; }
        [Display(Name = "CANTIDAD")] public string CANTIDAD { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
}