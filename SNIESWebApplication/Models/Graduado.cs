﻿namespace SNIESWebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("Graduado")]
    public class Graduado : Entidad
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
        [Display(Name = "PROGRAMA")] public string PROGRAMA { get; set; }
        [Display(Name = "COD DANE")] public string COD_DANE { get; set; }
        [Display(Name = "DEPARTAMENTO")] public string DEPARTAMENTO { get; set; }
        [Display(Name = "MUNICIPIO")] public string MUNICIPIO { get; set; }
        [Display(Name = "ECAES RESULTADO")] public string ECAES_RESULTADO { get; set; }
        [Display(Name = "ECAES OBSERVACIÓN")] public string ECAES_OBSERVACION { get; set; }
        [Display(Name = "NO ACTA GRADO")] public string NO_ACTA_GRADO { get; set; }
        [Display(Name = "FECHA GRADO")] public string FECHA_GRADO { get; set; }
        [Display(Name = "FOLIO")] public string FOLIO { get; set; }
        [Display(Name = "FUENTE")] public string FUENTE { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }

    }
}