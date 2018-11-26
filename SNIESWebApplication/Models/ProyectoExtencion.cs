namespace SNIESWebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("ProyectoExtencion")]
    public class ProyectoExtencion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "ANO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "UNIDAD ORGANZIACIONAL")] public string UNIDAD_ORGANZIACIONAL { get; set; }
        [Display(Name = "CÓDIGO PROYECTO")] public string CODIGO_PROYECTO { get; set; }
        [Display(Name = "NOMBRE PROYECTO")] public string NOMBRE_PROYECTO { get; set; }
        [Display(Name = "DESCRIPCIÓN PROYECTO")] public string DESCRIPCION_PROYECTO { get; set; }
        [Display(Name = "VALOR PROYECTO")] public string VALOR_PROYECTO { get; set; }
        [Display(Name = "ÁREA EXTENSIÓN")] public string ÁREA_EXTENSION { get; set; }
        [Display(Name = "FECHA INICIO")] public string FECHA_INICIO { get; set; }
        [Display(Name = "FECHA FINAL")] public string FECHA_FINAL { get; set; }
        [Display(Name = "NOMBRE CONTACTO")] public string NOMBRE_CONTACTO { get; set; }
        [Display(Name = "APELLIDO CONTACTO")] public string APELLIDO_CONTACTO { get; set; }
        [Display(Name = "TELÉFONO CONTACTO")] public string TELEFONO_CONTACTO { get; set; }
        [Display(Name = "CORREO CONTACTO")] public string CORREO_CONTACTO { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
    [Table("AreaTrabajoProyectoExtencion")]
    public class AreaTrabajoProyectoExtencion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "ANO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "UNIDAD ORGANIZACINAL")] public string UNIDAD_ORGANIZACINAL { get; set; }
        [Display(Name = "CÓDIGO PROYECTO")] public string CODIGO_PROYECTO { get; set; }
        [Display(Name = "NOMBRE PROYECTO")] public string NOMBRE_PROYECTO { get; set; }
        [Display(Name = "ÁREA TRABAJO")] public string ÁREA_TRABAJO { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }

    [Table("CicloVitalProyectoExtencion")]
    public class CicloVitalProyectoExtencion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "ANO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "UNIDAD ORGANIZACINAL")] public string UNIDAD_ORGANIZACINAL { get; set; }
        [Display(Name = "CÓDIGO PROYECTO")] public string CODIGO_PROYECTO { get; set; }
        [Display(Name = "NOMBRE PROYECTO")] public string NOMBRE_PROYECTO { get; set; }
        [Display(Name = "CICLO VITAL")] public string CICLO_VITAL { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
    [Table("EntidadNacionalProyectoExtencion")]
    public class EntidadNacionalProyectoExtencion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string AÑO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "UNIDAD ORGANIZACINAL")] public string UNIDAD_ORGANIZACINAL { get; set; }
        [Display(Name = "CÓDIGO PROYECTO")] public string CODIGO_PROYECTO { get; set; }
        [Display(Name = "NOMBRE PROYECTO")] public string NOMBRE_PROYECTO { get; set; }
        [Display(Name = "ENTIDAD NACIONAL")] public string ENTIDAD_NACIONAL { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
    [Table("FuenteInternacionalProyectoExtencion")]
    public class FuenteInternacionalProyectoExtencion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "ANO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "UNIDAD ORGANIZACINAL")] public string UNIDAD_ORGANIZACINAL { get; set; }
        [Display(Name = "CÓDIGO PROYECTO")] public string CODIGO_PROYECTO { get; set; }
        [Display(Name = "NOMBRE PROYECTO")] public string NOMBRE_PROYECTO { get; set; }
        [Display(Name = "NOMBRE INSTITUCIÓN")] public string NOMBRE_INSTITUCION { get; set; }
        [Display(Name = "PAÍS")] public string PAIS { get; set; }
        [Display(Name = "FUENTE INTERNACIONAL")] public string FUENTE_INTERNACIONAL { get; set; }
        [Display(Name = "VALOR FINANCIACIÓN")] public string VALOR_FINANCIACION { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
    [Table("FuenteNacionalProyectoExtencion")]
    public class FuenteNacionalProyectoExtencion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "ANO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "UNIDAD ORGANIZACINAL")] public string UNIDAD_ORGANIZACINAL { get; set; }
        [Display(Name = "CÓDIGO PROYECTO")] public string CODIGO_PROYECTO { get; set; }
        [Display(Name = "NOMBRE PROYECTO")] public string NOMBRE_PROYECTO { get; set; }
        [Display(Name = "FUENTE NACIONAL")] public string FUENTE_NACIONAL { get; set; }
        [Display(Name = "VALOR FINANCIACIÓN")] public string VALOR_FINANCIACION { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
    [Table("OtraEntidadProyectoExtencion")]
    public class OtraEntidadProyectoExtencion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "ANO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "UNIDAD ORGANIZACINAL")] public string UNIDAD_ORGANIZACINAL { get; set; }
        [Display(Name = "CÓDIGO PROYECTO")] public string CODIGO_PROYECTO { get; set; }
        [Display(Name = "NOMBRE PROYECTO")] public string NOMBRE_PROYECTO { get; set; }
        [Display(Name = "NOMBRE ENTIDAD")] public string NOMBRE_ENTIDAD { get; set; }
        [Display(Name = "PAÍS")] public string PAIS { get; set; }
        [Display(Name = "SECTOR ENTIDAD")] public string SECTOR_ENTIDAD { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
    [Table("PoblacionCondiProyectoExtencion")]
    public class PoblacionCondiProyectoExtencion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "ANO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "UNIDAD ORGANIZACINAL")] public string UNIDAD_ORGANIZACINAL { get; set; }
        [Display(Name = "CÓDIGO PROYECTO")] public string CODIGO_PROYECTO { get; set; }
        [Display(Name = "NOMBRE PROYECTO")] public string NOMBRE_PROYECTO { get; set; }
        [Display(Name = "POBLACIÓN")] public string POBLACION { get; set; }
        [Display(Name = "CANTIDAD")] public string CANTIDAD { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
    [Table("PoblacionGrupoProyectoExtencion")]
    public class PoblacionGrupoProyectoExtencion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "ANO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "UNIDAD ORGANIZACINAL")] public string UNIDAD_ORGANIZACINAL { get; set; }
        [Display(Name = "CÓDIGO PROYECTO")] public string CODIGO_PROYECTO { get; set; }
        [Display(Name = "NOMBRE PROYECTO")] public string NOMBRE_PROYECTO { get; set; }
        [Display(Name = "POBLACIÓN")] public string POBLACION { get; set; }
        [Display(Name = "CANTIDAD")] public string CANTIDAD { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
}