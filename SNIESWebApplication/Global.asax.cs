using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SNIESWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SNIESWebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            CrearRoles(db);            
            db.Dispose();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void AsignarRolAdministrador(ApplicationDbContext db, string rol)
        {
            var usuarioManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var usuarioAdministrador = usuarioManager.FindByEmail("desarrollador@unicoc.edu.co");

            if (usuarioAdministrador != null)
            {
                if (!usuarioManager.IsInRole(usuarioAdministrador.Id, rol))
                {

                }usuarioManager.AddToRole(usuarioAdministrador.Id, rol);
            }
        }

        private void CrearRoles(ApplicationDbContext db)
        {
            var rolMarager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            foreach (var itemRol in ListaRoles())
            {
                if (rolMarager.RoleExists(itemRol))
                {
                    if (itemRol == "Desarrollador")
                    {
                        AsignarRolAdministrador(db, itemRol);
                    }                    
                }
                else
                {
                    rolMarager.Create(new IdentityRole(itemRol));
                    if (itemRol == "Desarrollador")
                    {
                        AsignarRolAdministrador(db, itemRol);
                    }
                }
            }
        }

        public string[] ListaRoles()
        {
            String[] listaRolesString = new[] 
            {
                "Administrador",
                "Desarrollador",
                "PuntoInformacion",
                "RRHH",
                "Usuario",
                "BienStar",
                "Calidad"
            };
            return listaRolesString;
        }
    }
}
