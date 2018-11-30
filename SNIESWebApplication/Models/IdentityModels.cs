using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SNIESWebApplication.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Periodo>().Has

        //}
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.Participante> Participantes { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.Periodo> Periodos { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.Admitido> Admitidos { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.Matriculado> Matriculados { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.Graduado> Graduados { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.Cupo> Cupos { get; set; }

        public System.Data.Entity.DbSet<SNIESWebApplication.Models.Inscrito> Inscritos { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.InscritoPrograma> InscritoPrograma { get; set; }

        public System.Data.Entity.DbSet<SNIESWebApplication.Models.EstudiantePrimerCurso> EstudiantesPrimerCurso { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.RetiroDisciplinario> RetirosDisciplinarios { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.ApoyoEstudiante> ApoyoEstudiantes { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.EstudianteArticulacion> EstudianteArticulacion { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.ActividadBienestar> ActividadBienestar { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.ActividadBeneficiar> ActividadBeneficiar { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.ActividadRecHumano> ActividadRecHumano { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.ActividadCultural> ActividadCultural { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.RecHumanoCultural> RecHumanoCultural { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.Consultaria> Consultaria { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.RecHumanoConsultoria> RecHumanoConsultoria { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.EducacionContinua> EducacionContinua { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.DocenteEducacionContinua> DocenteEducacionContinua { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.BeneficioEducacionContinua> BeneficioEducacionContinua { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.EventoCultural> EventoCultural { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.FteNacionEventoCultural> FteNacionEventoCultural { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.FteInternEventoCultural> FteInternEventoCultural { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.RecHumanoEventoCultural> RecHumanoEventoCultural { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.BeneficiarEventoCultural> BeneficiarEventoCultural { get; set; }

        public System.Data.Entity.DbSet<SNIESWebApplication.Models.ProyectoExtencion> ProyectoExtencion { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.AreaTrabajoProyectoExtencion> AreaTrabajoProyectoExtencion { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.CicloVitalProyectoExtencion> CicloVitalProyectoExtencion { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.EntidadNacionalProyectoExtencion> EntidadNacionalProyectoExtencion { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.FuenteInternacionalProyectoExtencion> FuenteInternacionalProyectoExtencion { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.FuenteNacionalProyectoExtencion> FuenteNacionalProyectoExtencion { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.OtraEntidadProyectoExtencion> OtraEntidadProyectoExtencion { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.PoblacionCondiProyectoExtencion> PoblacionCondiProyectoExtencion { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.PoblacionGrupoProyectoExtencion> PoblacionGrupoProyectoExtencion { get; set; }

        public System.Data.Entity.DbSet<SNIESWebApplication.Models.ServicioExtension> ServicioExtension { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.AreaTrabajoServicioExtension> AreaTrabajoServicioExtension { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.CicloVitalServicioExtension> CicloVitalServicioExtension { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.EntidadNacionalServicioExtension> EntidadNacionalServicioExtension { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.FuenteInternacionalServicioExtension> FuenteInternacionalServicioExtension { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.FuenteNacionalServicioExtension> FuenteNacionalServicioExtension { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.OtraEntidadServicioExtension> OtraEntidadServicioExtension { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.PoblacionCondicionalServicioExtension> PoblacionCondicionalServicioExtension { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.PoblacionGrupoServicioExtension> PoblacionGrupoServicioExtension { get; set; }

        public System.Data.Entity.DbSet<SNIESWebApplication.Models.Docente> Docente { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.CapacitacionDocente> CapacitacionDocente { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.ContratoDocente> ContratoDocente { get; set; }

        public System.Data.Entity.DbSet<SNIESWebApplication.Models.ConvenioInternacional> ConvenioInternacional { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.ConvenioInternacionalInstitucion> ConvenioInternacionalInstitucion { get; set; }

        public System.Data.Entity.DbSet<SNIESWebApplication.Models.ProgramaPresencialeExterior> ProgramaPresencialeExterior { get; set; }

        public System.Data.Entity.DbSet<SNIESWebApplication.Models.MovilidadEstudianteExteriorInternacionalizacion> MovilidadEstudianteExteriorInternacionalizacion { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.MovilidadDocenteExteriorInternacionalizacion> MovilidadDocenteExteriorInternacionalizacion { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.MovilidadDocenteExteriorColombiaInternacionalizacion> MovilidadDocenteExteriorColombiaInternacionalizacion { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.MovilidadEstudianteExteriorColombiaInternacionalizacion> MovilidadEstudianteExteriorColombiaInternacionalizacion { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.MovilidadAdministrativoExteriorInternacionalizacion> MovilidadAdministrativoExteriorInternacionalizacion { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.MovilidadAdministrativoColombiaInternacionalizacion> MovilidadAdministrativoColombiaInternacionalizacion { get; set; }



    }
}