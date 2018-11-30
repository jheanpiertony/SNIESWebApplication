namespace SNIESWebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("MovilidadInternacionalizacion")]
    public class MovilidadEstudianteExteriorInternacionalizacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "PAÍS")] public string PAIS { get; set; }
        [Display(Name = "INSTITUCIÓN EXTRANJERA")] public string INSTITUCION_EXTRANJERA { get; set; }
        [Display(Name = "TIPO MOVILIDAD")] public string TIPO_MOVILIDAD { get; set; }
        [Display(Name = "DÍAS MOVILIDAD")] public string DIAS_MOVILIDAD { get; set; }
        [Display(Name = "MOVILIDAD POR CONVENIO")] public string MOVILIDAD_POR_CONVENIO { get; set; }
        [Display(Name = "CÓDIGO CONVENIO")] public string CODIGO_CONVENIO { get; set; }
        [Display(Name = "ID PARTICIPANTE")] public string ID_PARTICIPANTE { get; set; }
        [Display(Name = "TIPO DOCUMENTO")] public string TIPO_DOCUMENTO { get; set; }
        [Display(Name = "NUMERO DOCUMENTO")] public string NUMERO_DOCUMENTO { get; set; }
        [Display(Name = "PRIMER NOMBRE")] public string PRIMER_NOMBRE { get; set; }
        [Display(Name = "SEGUNDO NOMBRE")] public string SEGUNDO_NOMBRE { get; set; }
        [Display(Name = "PRIMER APELLIDO")] public string PRIMER_APELLIDO { get; set; }
        [Display(Name = "SEGUNDO APELLIDO")] public string SEGUNDO_APELLIDO { get; set; }
        [Display(Name = "SEXO BIOLOGICO")] public string SEXO_BIOLOGICO { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }

    [Table("MovilidadDocenteExteriorInternacionalizacion")]
    public class MovilidadDocenteExteriorInternacionalizacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "PAÍS")] public string PAIS { get; set; }
        [Display(Name = "INSTITUCIÓN EXTRANJERA")] public string INSTITUCION_EXTRANJERA { get; set; }
        [Display(Name = "TIPO MOVILIDAD")] public string TIPO_MOVILIDAD { get; set; }
        [Display(Name = "DÍAS MOVILIDAD")] public string DIAS_MOVILIDAD { get; set; }
        [Display(Name = "MOVILIDAD POR CONVENIO")] public string MOVILIDAD_POR_CONVENIO { get; set; }
        [Display(Name = "CÓDIGO CONVENIO")] public string CODIGO_CONVENIO { get; set; }
        [Display(Name = "ID PARTICIPANTE")] public string ID_PARTICIPANTE { get; set; }
        [Display(Name = "TIPO DOCUMENTO")] public string TIPO_DOCUMENTO { get; set; }
        [Display(Name = "NUMERO DOCUMENTO")] public string NUMERO_DOCUMENTO { get; set; }
        [Display(Name = "PRIMER NOMBRE")] public string PRIMER_NOMBRE { get; set; }
        [Display(Name = "SEGUNDO NOMBRE")] public string SEGUNDO_NOMBRE { get; set; }
        [Display(Name = "PRIMER APELLIDO")] public string PRIMER_APELLIDO { get; set; }
        [Display(Name = "SEGUNDO APELLIDO")] public string SEGUNDO_APELLIDO { get; set; }
        [Display(Name = "SEXO BIOLOGICO")] public string SEXO_BIOLOGICO { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }

    [Table("MovilidadDocenteExteriorColombiaInternacionalizacion")]
    public class MovilidadDocenteExteriorColombiaInternacionalizacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "PAÍS")] public string PAIS { get; set; }
        [Display(Name = "INSTITUCIÓN EXTRANJERA")] public string INSTITUCION_EXTRANJERA { get; set; }
        [Display(Name = "TIPO MOVILIDAD")] public string TIPO_MOVILIDAD { get; set; }
        [Display(Name = "DÍAS MOVILIDAD")] public string DIAS_MOVILIDAD { get; set; }
        [Display(Name = "CÓDIGO CONVENIO")] public string CODIGO_CONVENIO { get; set; }
        [Display(Name = "TIPO DOCUMENTO")] public string TIPO_DOCUMENTO { get; set; }
        [Display(Name = "NUMERO DOCUMENTO")] public string NUMERO_DOCUMENTO { get; set; }
        [Display(Name = "PRIMER NOMBRE")] public string PRIMER_NOMBRE { get; set; }
        [Display(Name = "SEGUNDO NOMBRE")] public string SEGUNDO_NOMBRE { get; set; }
        [Display(Name = "PRIMER APELLIDO")] public string PRIMER_APELLIDO { get; set; }
        [Display(Name = "SEGUNDO APELLIDO")] public string SEGUNDO_APELLIDO { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }

    [Table("MovilidadEstudianteExteriorColombiaInternacionalizacion")]
    public class MovilidadEstudianteExteriorColombiaInternacionalizacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "PAÍS")] public string PAIS { get; set; }
        [Display(Name = "INSTITUCIÓN EXTRANJERA")] public string INSTITUCION_EXTRANJERA { get; set; }
        [Display(Name = "TIPO MOVILIDAD")] public string TIPO_MOVILIDAD { get; set; }
        [Display(Name = "DÍAS MOVILIDAD")] public string DIAS_MOVILIDAD { get; set; }
        [Display(Name = "CÓDIGO CONVENIO")] public string CODIGO_CONVENIO { get; set; }
        [Display(Name = "TIPO DOCUMENTO")] public string TIPO_DOCUMENTO { get; set; }
        [Display(Name = "NUMERO DOCUMENTO")] public string NUMERO_DOCUMENTO { get; set; }
        [Display(Name = "PRIMER NOMBRE")] public string PRIMER_NOMBRE { get; set; }
        [Display(Name = "SEGUNDO NOMBRE")] public string SEGUNDO_NOMBRE { get; set; }
        [Display(Name = "PRIMER APELLIDO")] public string PRIMER_APELLIDO { get; set; }
        [Display(Name = "SEGUNDO APELLIDO")] public string SEGUNDO_APELLIDO { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }

    [Table("MovilidadAdministrativoExteriorInternacionalizacion")]
    public class MovilidadAdministrativoExteriorInternacionalizacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "ID TIPO DOCUMENTO")] public string ID_TIPO_DOCUMENTO { get; set; }
        [Display(Name = "TIPO DOCUMENTO")] public string TIPO_DOCUMENTO { get; set; }
        [Display(Name = "NUMERO DOCUMENTO")] public string NUMERO_DOCUMENTO { get; set; }
        [Display(Name = "PAÍS")] public string PAIS { get; set; }
        [Display(Name = "INSTITUCIÓN EXTRANJERA")] public string INSTITUCION_EXTRANJERA { get; set; }
        [Display(Name = "TIPO MOVILIDAD")] public string TIPO_MOVILIDAD { get; set; }
        [Display(Name = "DÍAS MOVILIDAD")] public string DIAS_MOVILIDAD { get; set; }
        [Display(Name = "CÓDIGO CONVENIO")] public string CODIGO_CONVENIO { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }

    [Table("MovilidadAdministrativoColombiaInternacionalizacion")]
    public class MovilidadAdministrativoColombiaInternacionalizacion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "AÑO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "PAÍS")] public string PAIS { get; set; }
        [Display(Name = "INSTITUCIÓN EXTRANJERA")] public string INSTITUCION_EXTRANJERA { get; set; }
        [Display(Name = "TIPO MOVILIDAD")] public string TIPO_MOVILIDAD { get; set; }
        [Display(Name = "DÍAS MOVILIDAD")] public string DIAS_MOVILIDAD { get; set; }
        [Display(Name = "CÓDIGO CONVENIO")] public string CODIGO_CONVENIO { get; set; }
        [Display(Name = "ID PARTICIPANTE")] public string ID_PARTICIPANTE { get; set; }
        [Display(Name = "TIPO DOCUMENTO")] public string TIPO_DOCUMENTO { get; set; }
        [Display(Name = "NUMERO DOCUMENTO")] public string NUMERO_DOCUMENTO { get; set; }
        [Display(Name = "PRIMER NOMBRE")] public string PRIMER_NOMBRE { get; set; }
        [Display(Name = "SEGUNDO NOMBRE")] public string SEGUNDO_NOMBRE { get; set; }
        [Display(Name = "PRIMER APELLIDO")] public string PRIMER_APELLIDO { get; set; }
        [Display(Name = "SEGUNDO APELLIDO")] public string SEGUNDO_APELLIDO { get; set; }
        [Display(Name = "SEXO BIOLOGICO")] public string SEXO_BIOLOGICO { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
}