namespace SNIESWebApplication.Models
{
    using SNIESWebApplication.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("CentroInvCentroInvestigacion")]
    public class CentroInvCentroInvestigacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CÓDIGO PROYECTO")] public string CODIGO_PROYECTO { get; set; }
        [Display(Name = "NOMBRE PROYECTO")] public string NOMBRE_PROYECTO { get; set; }
        [Display(Name = "DURACIÓN")] public string DURACION { get; set; }
        [Display(Name = "OBJETIVO")] public string OBJETIVO { get; set; }
        [Display(Name = "RESUMEN")] public string RESUMEN { get; set; }
        [Display(Name = "RESULTADO")] public string RESULTADO { get; set; }
        [Display(Name = "FECHA INICIO")] public string FECHA_INICIO { get; set; }
        [Display(Name = "TIPO PROYECTO")] public string TIPO_PROYECTO { get; set; }
        [Display(Name = "SOCIO ECONÓMICO")] public string SOCIO_ECONOMICO { get; set; }
        //[Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }

    [Table("CentroGruProductoInvestigacion")]
    public class CentroGruCentroInvestigacion : Entidad
    {
        [Display(Name = "ID IES")] public string ID_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "COD CENTRO")] public string COD_CENTRO { get; set; }
        [Display(Name = "NOMBRE CENTRO")] public string NOMBRE_CENTRO { get; set; }
        [Display(Name = "COD GRUPO")] public string COD_GRUPO { get; set; }
        [Display(Name = "FECHA ADMISIÓN")] public string FECHA_ADMISION { get; set; }
        [Display(Name = "FECHA RETIRO")] public string FECHA_RETIRO { get; set; }
        //[Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }

    //**************************//

    [Table("ProductoInvestigacion")]
    public class ProductoInvestigacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "CÓDIGO PRODUCTO")] public string CODIGO_PRODUCTO { get; set; }
        [Display(Name = "NOMBRE PRODUCTO")] public string NOMBRE_PRODUCTO { get; set; }
        [Display(Name = "AÑO PRODUCTO")] public string AÑO_PRODUCTO { get; set; }
        [Display(Name = "MES PRODUCTO")] public string MES_PRODUCTO { get; set; }
        [Display(Name = "CINE CAMPO DETALLADO")] public string CINE_CAMPO_DETALLADO { get; set; }
        [Display(Name = "TIPO PRODUCTO")] public string TIPO_PRODUCTO { get; set; }
        //[Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }

    //**************************//

    [Table("GrupoInvGrupoInvestigacion")]
    public class GrupoInvGrupoInvestigacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "CÓDIGO GRUPO")] public string CODIGO_GRUPO { get; set; }
        [Display(Name = "NOMBRE GRUPO")] public string NOMBRE_GRUPO { get; set; }
        [Display(Name = "COD CINE")] public string COD_CINE { get; set; }
        [Display(Name = "CINE CAMPO DETALLADO")] public string CINE_CAMPO_DETALLADO { get; set; }
        [Display(Name = "FECHA INICIO")] public string FECHA_INICIO { get; set; }
        [Display(Name = "FECHA VIGENCIA")] public string FECHA_VIGENCIA { get; set; }
    }

    [Table("InvInternGrupoInvestigacion")]
    public class InvInternGrupoInvestigacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "ID TIPO DOCUMENTO")] public string ID_TIPO_DOCUMENTO { get; set; }
        [Display(Name = "TIPO DOCUMENTO")] public string TIPO_DOCUMENTO { get; set; }
        [Display(Name = "NUMERO DOCUMENTO")] public string NUMERO_DOCUMENTO { get; set; }
        [Display(Name = "PRIMER NOMBRE")] public string PRIMER_NOMBRE { get; set; }
        [Display(Name = "SEGUNDO NOMBRE")] public string SEGUNDO_NOMBRE { get; set; }
        [Display(Name = "PRIMER APELLIDO")] public string PRIMER_APELLIDO { get; set; }
        [Display(Name = "SEGUNDO APELLIDO")] public string SEGUNDO_APELLIDO { get; set; }
        [Display(Name = "TIPO PARTICIPACIÓN")] public string TIPO_PARTICIPACION { get; set; }
        [Display(Name = "CINE CAMPO DETALLADO")] public string CINE_CAMPO_DETALLADO { get; set; }
        [Display(Name = "MÁXIMO NIVEL ESTUDIO")] public string MÁXIMO_NIVEL_ESTUDIO { get; set; }
        [Display(Name = "FECHA INGRESO")] public string FECHA_INGRESO { get; set; }
        [Display(Name = "FECHA RETIRO")] public string FECHA_RETIRO { get; set; }
    }

    [Table("InvExternGrupoInvestigacion")]
    public class InvExternGrupoInvestigacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "ID TIPO DOCUMENTO")] public string ID_TIPO_DOCUMENTO { get; set; }
        [Display(Name = "TIPO DOCUMENTO")] public string TIPO_DOCUMENTO { get; set; }
        [Display(Name = "NUMERO DOCUMENTO")] public string NUMERO_DOCUMENTO { get; set; }
        [Display(Name = "PRIMER NOMBRE")] public string PRIMER_NOMBRE { get; set; }
        [Display(Name = "SEGUNDO NOMBRE")] public string SEGUNDO_NOMBRE { get; set; }
        [Display(Name = "PRIMER APELLIDO")] public string PRIMER_APELLIDO { get; set; }
        [Display(Name = "SEGUNDO APELLIDO")] public string SEGUNDO_APELLIDO { get; set; }
        [Display(Name = "TIPO PARTICIPACIÓN")] public string TIPO_PARTICIPACION { get; set; }
        [Display(Name = "CINE CAMPO DETALLADO")] public string CINE_CAMPO_DETALLADO { get; set; }
        [Display(Name = "FECHA INGRESO")] public string FECHA_INGRESO { get; set; }
        [Display(Name = "FECHA RETIRO")] public string FECHA_RETIRO { get; set; }
    }

    //*************************************//

    [Table("RedInvestigacion")]
    public class RedInvestigacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "CÓDIGO RED")] public string CODIGO_RED { get; set; }
        [Display(Name = "NOMBRE RED")] public string NOMBRE_RED { get; set; }
        [Display(Name = "CINE CAMPO DETALLADO")] public string CINE_CAMPO_DETALLADO { get; set; }
        [Display(Name = "FECHA CREACIÓN")] public string FECHA_CREACION { get; set; }
    }

    [Table("RedIesRedeInvestigacion")]
    public class RedIesRedeInvestigacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "CÓDIGO RED")] public string CODIGO_RED { get; set; }
        [Display(Name = "NOMBRE RED")] public string NOMBRE_RED { get; set; }
        [Display(Name = "FECHA INGRESO")] public string FECHA_INGRESO { get; set; }
        [Display(Name = "FECHA RETIRO")] public string FECHA_RETIRO { get; set; }
    }

    [Table("RedIntegrInvestigacion")]
    public class RedIntegrInvestigacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "CÓDIGO RED")] public string CODIGO_RED { get; set; }
        [Display(Name = "NOMBRE RED")] public string NOMBRE_RED { get; set; }
        [Display(Name = "NOMBRE ENTIDAD")] public string NOMBRE_ENTIDAD { get; set; }
        [Display(Name = "SECTOR")] public string SECTOR { get; set; }
        [Display(Name = "PAÍS")] public string PAIS { get; set; }
        [Display(Name = "FECHA INGRESO")] public string FECHA_INGRESO { get; set; }
        [Display(Name = "FECHA RETIRO")] public string FECHA_RETIRO { get; set; }
    }

    //**************************************************//

    [Table("OtraActividadInvestigacion")]
    public class OtraActividadInvestigacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string AÑO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "TIPO ACTIVIDAD INVESTIGACIÓN")] public string TIPO_ACTIVIDAD_INVESTIGACION { get; set; }
        [Display(Name = "FECHA RETIRO")] public string FECHA_PERIODO { get; set; }
    }

    [Table("FteNaActividadInvestigacion")]
    public class FteNaActividadInvestigacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string AÑO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "TIPO ACTIVIDAD")] public string TIPO_ACTIVIDAD { get; set; }
        [Display(Name = "FUENTE NACIONAL")] public string FUENTE_NACIONAL { get; set; }
        [Display(Name = "VALOR FINANCIACIÓN")] public string VALOR_FINANCIACION { get; set; }
        [Display(Name = "FECHA RETIRO")] public string FECHA_PERIODO { get; set; }
    }

    [Table("FteInActividadInvestigacion")]
    public class FteInActividadInvestigacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string AÑO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "TIPO ACTIVIDAD")] public string TIPO_ACTIVIDAD { get; set; }
        [Display(Name = "INSTITUCIÓN")] public string INSTITUCION { get; set; }
        [Display(Name = "PAÍS")] public string PAIS { get; set; }
        [Display(Name = "FUENTE INTERNACIONAL")] public string FUENTE_INTERNACIONAL { get; set; }
        [Display(Name = "VALOR FINANCIACIÓN")] public string VALOR_FINANCIACION { get; set; }
        [Display(Name = "FECHA RETIRO")] public string FECHA_PERIODO { get; set; }
    }

    [Table("GastoActividadInvestigacion")]
    public class GastoActividadInvestigacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string AÑO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "TIPO ACTIVIDAD")] public string TIPO_ACTIVIDAD { get; set; }
        [Display(Name = "TIPO GASTO")] public string TIPO_GASTO { get; set; }
        [Display(Name = "VALOR GASTO")] public string VALOR_GASTO { get; set; }
        [Display(Name = "FECHA RETIRO")] public string FECHA_PERIODO { get; set; }
    }

    //***************************************************************//

    [Table("ProyectoInvestigacion")]
    public class ProyectoInvestigacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string AÑO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CÓDIGO PROYECTO")] public string CODIGO_PROYECTO { get; set; }
        [Display(Name = "NOMBRE PROYECTO")] public string NOMBRE_PROYECTO { get; set; }
        [Display(Name = "DURACIÓN")] public string DURACION { get; set; }
        [Display(Name = "OBJETIVO")] public string OBJETIVO { get; set; }
        [Display(Name = "RESUMEN")] public string RESUMEN { get; set; }
        [Display(Name = "RESULTADO")] public string RESULTADO { get; set; }
        [Display(Name = "FECHA INICIO")] public string FECHA_INICIO { get; set; }
        [Display(Name = "TIPO PROYECTO")] public string TIPO_PROYECTO { get; set; }
        [Display(Name = "SOCIO ECONÓMICO")] public string SOCIO_ECONOMICO { get; set; }

        [Display(Name = "FECHA RETIRO")] public string FECHA_PERIODO { get; set; }
    }

     [Table("PICentroProyectoInvestigacion")]
    public class PICentroProyectoInvestigacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string AÑO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CÓDIGO PROYECTO")] public string CODIGO_PROYECTO { get; set; }
        [Display(Name = "NOMBRE PROYECTO")] public string NOMBRE_PROYECTO { get; set; }
        [Display(Name = "CÓDIGO CENTRO")] public string CODIGO_CENTRO { get; set; }

        [Display(Name = "FECHA RETIRO")] public string FECHA_PERIODO { get; set; }
    }

     [Table("PIGrupoProyectoInvestigacion")]
    public class PIGrupoProyectoInvestigacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string AÑO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CÓDIGO PROYECTO")] public string CODIGO_PROYECTO { get; set; }
        [Display(Name = "NOMBRE PROYECTO")] public string NOMBRE_PROYECTO { get; set; }
        [Display(Name = "CÓDIGO GRUPO")] public string CODIGO_GRUPO { get; set; }

        [Display(Name = "FECHA RETIRO")] public string FECHA_PERIODO { get; set; }
    }

     [Table("ProyIRedProyectoInvestigacion")]
    public class ProyIRedProyectoInvestigacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string AÑO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CÓDIGO PROYECTO")] public string CODIGO_PROYECTO { get; set; }
        [Display(Name = "NOMBRE PROYECTO")] public string NOMBRE_PROYECTO { get; set; }
        [Display(Name = "CÓDIGO RED")] public string CODIGO_RED { get; set; }

        [Display(Name = "FECHA RETIRO")] public string FECHA_PERIODO { get; set; }
    }

     [Table("PIProducProyectoInvestigacion")]
    public class PIProducProyectoInvestigacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string AÑO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CÓDIGO PROYECTO")] public string CODIGO_PROYECTO { get; set; }
        [Display(Name = "NOMBRE PROYECTO")] public string NOMBRE_PROYECTO { get; set; }
        [Display(Name = "CÓDIGO PRODUCTO")] public string CODIGO_PRODUCTO { get; set; }

        [Display(Name = "FECHA RETIRO")] public string FECHA_PERIODO { get; set; }
    }

     [Table("PIFteNaProyectoInvestigacion")]
    public class PIFteNaProyectoInvestigacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string AÑO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CÓDIGO PROYECTO")] public string CODIGO_PROYECTO { get; set; }
        [Display(Name = "NOMBRE PROYECTO")] public string NOMBRE_PROYECTO { get; set; }
        [Display(Name = "FUENTE NACIONAL")] public string FUENTE_NACIONAL { get; set; }
        [Display(Name = "VALOR FINANCIACIÓN")] public string VALOR_FINANCIACION { get; set; }

        [Display(Name = "FECHA RETIRO")] public string v { get; set; }
    }

     [Table("PIFteInProyectoInvestigacion")]
    public class PIFteInProyectoInvestigacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string AÑO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CÓDIGO PROYECTO")] public string CODIGO_PROYECTO { get; set; }
        [Display(Name = "NOMBRE PROYECTO")] public string NOMBRE_PROYECTO { get; set; }
        [Display(Name = "NOMBRE INSTITUCIÓN")] public string NOMBRE_INSTITUCION { get; set; }
        [Display(Name = "PAÍS")] public string PAIS { get; set; }
        [Display(Name = "FUENTE INTERNACIONAL")] public string FUENTE_INTERNACIONAL { get; set; }
        [Display(Name = "VALOR FINANCIACIÓN")] public string VALOR_FINANCIACION { get; set; }

        [Display(Name = "FECHA RETIRO")] public string FECHA_PERIODO { get; set; }
    }

     [Table("PIGastoProyectoInvestigacion")]
    public class PIGastoProyectoInvestigacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string AÑO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CÓDIGO PROYECTO")] public string CODIGO_PROYECTO { get; set; }
        [Display(Name = "NOMBRE PROYECTO")] public string NOMBRE_PROYECTO { get; set; }
        [Display(Name = "TIPO GASTO")] public string TIPO_GASTO { get; set; }
        [Display(Name = "VALOR GASTO")] public string VALOR_GASTO { get; set; }

        [Display(Name = "FECHA RETIRO")] public string FECHA_PERIODO { get; set; }
    }

     [Table("InternoIProyectoInvestigacion")]
    public class InternoIProyectoInvestigacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string AÑO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CÓDIGO PROYECTO")] public string CODIGO_PROYECTO { get; set; }
        [Display(Name = "NOMBRE PROYECTO")] public string NOMBRE_PROYECTO { get; set; }
        [Display(Name = "TIPO PARTICIPANTE")] public string TIPO_PARTICIPANTE { get; set; }
        [Display(Name = "CINE CAMPO DETALLADO")] public string CINE_CAMPO_DETALLADO { get; set; }
        [Display(Name = "NIVEL ESTUDIO")] public string NIVEL_ESTUDIO { get; set; }
        [Display(Name = "FECHA INGRESO")] public string FECHA_INGRESO { get; set; }
        [Display(Name = "FECHA RETIRO")] public string FECHA_RETIRO { get; set; }
        [Display(Name = "ID PARTICIPANTE")] public string ID_PARTICIPANTE { get; set; }
        [Display(Name = "TIPO DOCUMENTO")] public string TIPO_DOCUMENTO { get; set; }
        [Display(Name = "NUMERO DOCUMENTO")] public string NUMERO_DOCUMENTO { get; set; }
        [Display(Name = "PRIMER NOMBRE")] public string PRIMER_NOMBRE { get; set; }
        [Display(Name = "SEGUNDO NOMBRE")] public string SEGUNDO_NOMBRE { get; set; }
        [Display(Name = "PRIMER APELLIDO")] public string PRIMER_APELLIDO { get; set; }
        [Display(Name = "SEGUNDO APELLIDO")] public string SEGUNDO_APELLIDO { get; set; }
        [Display(Name = "SEXO BIOLOGICO")] public string SEXO_BIOLOGICO { get; set; }

        [Display(Name = "FECHA RETIRO")] public string FECHA_PERIODO { get; set; }
    }

     [Table("ExternoIProyectoInvestigacion")]
    public class ExternoIProyectoInvestigacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string AÑO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CÓDIGO PROYECTO")] public string CODIGO_PROYECTO { get; set; }
        [Display(Name = "NOMBRE PROYECTO")] public string NOMBRE_PROYECTO { get; set; }
        [Display(Name = "TIPO DOCUMENTO")] public string TIPO_DOCUMENTO { get; set; }
        [Display(Name = "NUMERO DOCUMENTO")] public string NUMERO_DOCUMENTO { get; set; }
        [Display(Name = "PRIMER NOMBRE")] public string PRIMER_NOMBRE { get; set; }
        [Display(Name = "SEGUNDO NOMBRE")] public string SEGUNDO_NOMBRE { get; set; }
        [Display(Name = "PRIMER APELLIDO")] public string PRIMER_APELLIDO { get; set; }
        [Display(Name = "SEGUNDO APELLIDO")] public string SEGUNDO_APELLIDO { get; set; }
        [Display(Name = "TIPO PARTICIPANTE")] public string TIPO_PARTICIPANTE { get; set; }
        [Display(Name = "CINE CAMPO DETALLADO")] public string CINE_CAMPO_DETALLADO { get; set; }
        [Display(Name = "FECHA INGRESO")] public string FECHA_INGRESO { get; set; }
        [Display(Name = "FECHA RETIRO")] public string FECHA_RETIRO { get; set; }
        [Display(Name = "FECHA RETIRO")] public string FECHA_PERIODO { get; set; }
    }

}