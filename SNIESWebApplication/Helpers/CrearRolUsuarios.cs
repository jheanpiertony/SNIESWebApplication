namespace SNIESWebApplication.Helpers
{
    using System;
    using SNIESWebApplication.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    
    public class CrearRolUsuarios
    {
        public void CrearRoles(string IdUser)
        {
            using (ApplicationDbContext dbRoles = new ApplicationDbContext())
            {
                var rolMarager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbRoles));
                foreach (var itemRol in ListaRoles())
                {
                    if (rolMarager.RoleExists(itemRol))
                    {
                        //usuarioRoles(itemRol, rolMarager, dbRoles, IdUser)
                    }
                    else
                    {
                        rolMarager.Create(new IdentityRole(itemRol));
                        //usuarioRoles(itemRol, rolMarager, dbRoles, IdUser)
                    }                        
                }
            }
        }

        public void usuarioRoles(string rol, RoleManager<IdentityRole> rolMarager, ApplicationDbContext dbRoles, string idUser)
        {
            var usuarioManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbRoles));
            if (usuarioManager.IsInRole(idUser, rol))
            {
            }
            else
                usuarioManager.AddToRole(idUser, rol);
        }

        public string[] ListaRoles()
        {
            String[] listaRolesString = new[] {
            "Administrador",
            "Desarrollador",
            "PuntoInformacion",
            "RRHH",
            "Usuario"
            };
            return listaRolesString;
        }
    }
}