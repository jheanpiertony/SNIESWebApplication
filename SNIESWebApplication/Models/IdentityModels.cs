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
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.EstudiantePrimerCurso> EstudiantesPrimerCurso { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.RetiroDisciplinario> RetirosDisciplinarios { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.ApoyoEstudiante> ApoyoEstudiantes { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.EstudianteArticulacion> EstudianteArticulacion { get; set; }
        public System.Data.Entity.DbSet<SNIESWebApplication.Models.ActividadBienestar> ActividadBienestar { get; set; }
    }
}