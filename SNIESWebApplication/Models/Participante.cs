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
        [Display(Name = "PAÍS")] public string PAIS { get; set; }
        [Display(Name = "MUNICIPIO")] public string MUNICIPIO { get; set; }
        [Display(Name = "EMAIL INSTITUCIONAL")] public string EMAIL_INSTITUCIONAL { get; set; }
        [Display(Name = "DIRECCIÓN INSTITUCIONAL")] public string DIRECCION_INSTITUCIONAL { get; set; }
        [Display(Name = "EMAIL PERSONAL")] public string EMAIL_PEROSNAL { get; set; }
        [Display(Name = "CELULAR PERSONAL")] public string CELULAR_PERSONAL { get; set; }
        [Display(Name = "VERIFICADO POR FUENTE OFICIAL")] public string VERIFICADO_POR_FUENTE_OFICIAL { get; set; }
        [Display(Name = "FUENTE")] public string FUENTE { get; set; }
        [Display(Name = "FECHA PERIODO")] public string FECHA_PERIODO { get; set; }
    }
}