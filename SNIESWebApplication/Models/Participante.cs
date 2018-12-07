namespace SNIESWebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("Participante")]
    public class Participante : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "IDENTIFICADOR SNIES")] public string IDENTIFICADOR_SNIES { get; set; }
        [Display(Name = "TIPO DOCUMENTO")] public string TIPO_DOCUMENTO { get; set; }
        [Display(Name = "NUM DOCUMENTO")] public string NUM_DOCUMENTO { get; set; }
        [Display(Name = "PRIMER NOMBRE")] public string PRIMER_NOMBRE { get; set; }
        [Display(Name = "SEGUNDO NOMBRE")] public string SEGUNDO_NOMBRE { get; set; }
        [Display(Name = "PRIMER APELLIDO")] public string PRIMER_APELLIDO { get; set; }
        [Display(Name = "SEGUNDO APELLIDO")] public string SEGUNDO_APELLIDO { get; set; }
        [Display(Name = "ID SEXO")] public string ID_SEXO { get; set; }
        [Display(Name = "FECHA NACIMIENTO")] public string FECHA_NACIMIENTO { get; set; }
        [Display(Name = "ID PAÍS")] public string PAIS { get; set; }
        [Display(Name = "ID MUNICIPIO")] public string MUNICIPIO { get; set; }
        [Display(Name = "EMAIL INSTITUCIONAL")] public string EMAIL_INSTITUCIONAL { get; set; }
        [Display(Name = "DIRECCIÓN INSTITUCIONAL")] public string DIRECCION_INSTITUCIONAL { get; set; }
        [Display(Name = "EMAIL PERSONAL")] public string EMAIL_PEROSNAL { get; set; }
        [Display(Name = "CELULAR PERSONAL")] public string CELULAR_PERSONAL { get; set; }
        [Display(Name = "VERIFICADO POR FUENTE OFICIAL")] public string VERIFICADO_POR_FUENTE_OFICIAL { get; set; }
        [Display(Name = "FUENTE")] public string FUENTE { get; set; }
        //[Display(Name = "FECHA PERIODO")] public string FECHA_PERIODO { get; set; }
        [Display(Name = "FECHA EXPEDICION")] public string FECHA_EXPEDICION { get; set; }
        [Display(Name = "ID ESTADO CIVIL")] public string ID_ESTADO_CIVIL { get; set; }
    }

    //public class ParticipanteSaint 
    //{
    //    [Display(Name = "ID TIPO DOCUMENTO")] public string ID_TIPO_DOCUMENTO { get; set; }
    //    [Display(Name = "NUM DOCUMENTO")] public string NUM_DOCUMENTO { get; set; }
    //    [Display(Name = "FECHA EXPEDICION")] public string FECHA_EXPEDICION { get; set; }
    //    [Display(Name = "PRIMER NOMBRE")] public string PRIMER_NOMBRE { get; set; }
    //    [Display(Name = "SEGUNDO NOMBRE")] public string SEGUNDO_NOMBRE { get; set; }
    //    [Display(Name = "PRIMER APELLIDO")] public string PRIMER_APELLIDO { get; set; }
    //    [Display(Name = "SEGUNDO APELLIDO")] public string SEGUNDO_APELLIDO { get; set; }
    //    [Display(Name = "ID SEXO BIOLOGICO")] public string ID_SEXO_BIOLOGICO { get; set; }
    //    [Display(Name = "ID ESTADO CIVIL")] public string ID_ESTADO_CIVIL { get; set; }
    //    [Display(Name = "FECHA NACIMIENTO")] public string FECHA_NACIMIENTO { get; set; }
    //    [Display(Name = "ID PAIS")] public string ID_PAIS { get; set; }
    //    [Display(Name = "ID MUNICIPIO")] public string ID_MUNICIPIO { get; set; }
    //    [Display(Name = "TELEFONO CONTACTO")] public string TELEFONO_CONTACTO { get; set; }
    //    [Display(Name = "EMAIL PERSONAL")] public string EMAIL_PERSONAL { get; set; }
    //    [Display(Name = "EMAIL INSTITUCIONAL")] public string EMAIL_INSTITUCIONAL { get; set; }
    //    [Display(Name = "DIRECCION INSTITUCIONAL")] public string DIRECCION_INSTITUCIONAL { get; set; }

    //}
}