using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VideoPostMvc.Models;
using VideoPostMvc.Repositories;

namespace VideoPostMvc.Controllers
{
    public class AccountController : Controller
    {
        private UsuRepository repo;

        public AccountController(UsuRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string contraseña)
        {
            Usuario usuario = this.repo.LogInUsuario(email, contraseña);

            if (usuario == null)
            {

                return View();
            }
            else
            {
                var datos = new List<Claim>
                {
                    new Claim("name",usuario.Nombre!),
                    new Claim("id",usuario.IdUsuario.ToString()),
                    new Claim("rol",usuario.Rol.ToString())
                };
                ClaimsIdentity identidad = new(datos, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties propiedades = new()
                {
                    IsPersistent = false,
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)

                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identidad), propiedades);
                ViewData["INICIADO"] = "HAS INICIADO SESION ";
                return RedirectToAction("Index", "Home");

            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
