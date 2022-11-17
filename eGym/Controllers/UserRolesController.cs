using eGym.Models;
using eGym.ModelsView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eGym.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class UserRolesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRolesController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            var listaUsuarios = new List<UserRolesView>();
            foreach (IdentityUser item in users)
            {
                var nuevoUsuario = new UserRolesView
                {
                    UserId = item.Id,
                    UserName = item.UserName,
                    Email = item.Email,
                    Roles = new List<string>(await _userManager.GetRolesAsync(item))
                };
                listaUsuarios.Add(nuevoUsuario);
            }

            return View(listaUsuarios);
        }
    }
}
