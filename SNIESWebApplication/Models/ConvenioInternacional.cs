namespace SNIESWebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("ConvenioInternacional")]
    public class ConvenioInternacional : Entidad
    {
        [Display(Name = "ANO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CODIGO CONVENIO")] public string CODIGO_CONVENIO { get; set; }
        [Display(Name = "ID TIPO CONVENIO")] public string ID_TIPO_CONVENIO { get; set; }
        [Display(Name = "TIPO CONVENIO")] public string TIPO_CONVENIO { get; set; }
        [Display(Name = "ACTIVIDAD FORMACIÓN")] public string ACTIVIDAD_FORMACION { get; set; }
        [Display(Name = "ACTIVIDAD INVESTIGACIÓN")] public string ACTIVIDAD_INVESTIGACION { get; set; }
        [Display(Name = "ACTIVIDAD EXTENSIÓN")] public string ACTIVIDAD_EXTENSION { get; set; }
        [Display(Name = "ACTIVIDAD ADMINISTRATIVA")] public string ACTIVIDAD_ADMINISTRATIVA { get; set; }
        [Display(Name = "OTRAS ACTIVIDADES COOPERACIÓN")] public string OTRAS_ACTIVIDADES_COOPERACION { get; set; }
        [Display(Name = "FECHA INICIO")] public string FECHA_INICIO { get; set; }
        [Display(Name = "FECHA TERMINACION")] public string FECHA_TERMINACION { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }

    [Table("ConvenioInternacionalInstitucion")]
    public class ConvenioInternacionalInstitucion : Entidad
    {
        [Display(Name = "CÓDIGO IES")] public string CODIGO_IES { get; set; }
        [Display(Name = "NOMBRE IES")] public string NOMBRE_IES { get; set; }
        [Display(Name = "ANO")] public string ANO { get; set; }
        [Display(Name = "SEMESTRE")] public string SEMESTRE { get; set; }
        [Display(Name = "CODIGO CONVENIO")] public string CODIGO_CONVENIO { get; set; }
        [Display(Name = "INSTITUCIÓN ASOCIADA")] public string INSTITUCION_ASOCIADA { get; set; }
        [Display(Name = "INSTITUCIÓN MULTILATERAL")] public string INSTITUCION_MULTILATERAL { get; set; }
        [Display(Name = "ID PAIS INSTITUCIÓN ASOCIADA")] public string ID_PAIS_INSTITUCION_ASOCIADA { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }
}