using MangaGods.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MangaGods.Logic
{
    internal class RoleActions
    {
        /// <summary>
        /// Método interno que crea a través del identity, un rol que se llama "CanEdit" y un usuario llamado 
        /// de la misma manera que tiene dicho rol.
        /// </summary>
        internal void CrearUsuarioAdmin()
        {
            var context = new ApplicationDbContext();
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleMgr = new RoleManager<IdentityRole>(roleStore);

            if (!roleMgr.RoleExists("Admin"))
            {
                roleMgr.Create(new IdentityRole {Name = "Admin" });
            }

            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var appUser = new ApplicationUser
            {
                UserName = "admin@mangagods.com",
                Email = "admin@mangagods.com"
            };

            userMgr.Create(appUser, "Pa$$word1");

            if (!userMgr.IsInRole(userMgr.FindByEmail("admin@mangagods.com").Id, "Admin"))
            {
                userMgr.AddToRole(userMgr.FindByEmail("admin@mangagods.com").Id,"Admin");
            }
        }
    }
}
