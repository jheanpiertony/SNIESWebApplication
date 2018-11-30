namespace SNIESWebApplication.Models
{   
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("ClasificacionDerechoPecuniarioIESPrivada")]
    public class ClasificacionDerechoPecuniarioIESPrivada : Entidad
    {
        [Display(Name = "ID IES")] public string ID_IES { get; set; }
        [Display(Name = "IES NOMBRE")] public string IES_NOMBRE { get; set; }
        [Display(Name = "AÑO")] public string ANO { get; set; }
        [Display(Name = "ID TIPO DERECHO PECUNIARIO")] public string ID_TIPO_DERECHO_PECUNIARIO { get; set; }
        [Display(Name = "DESC TIPO DERECHO PECUNIARIO")] public string DESC_TIPO_DERECHO_PECUNIARIO { get; set; }
        [Display(Name = "CODIGO DERECHO PECUNIARIO")] public string CODIGO_DERECHO_PECUNIARIO { get; set; }
        [Display(Name = "DESCRIPCION DERECHO PECUNIARIO")] public string DESCRIPCION_DERECHO_PECUNIARIO { get; set; }
        [Display(Name = "CLASIFICADOR")] public string CLASIFICADOR { get; set; }
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }

    [Table("ProyectoInversionDerechoPecuniario")]
    public class IESDerechoPecuniario : Entidad
    {
        [Display(Name = "PERIODO")] public string FECHA_PERIODO { get; set; }
    }



}