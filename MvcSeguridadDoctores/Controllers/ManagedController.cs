using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MvcSeguridadDoctores.Models;
using MvcSeguridadDoctores.Repositories;
using System.Security.Claims;

namespace MvcSeguridadDoctores.Controllers {
    public class ManagedController : Controller {

        private RepositoryHospital repo;

        public ManagedController(RepositoryHospital repo) {
            this.repo = repo;
        }

        public IActionResult LogIn() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string username, string password) {
            Doctor doctor = await this.repo.ExisteDoctorAsync(username, int.Parse(password));

            if (doctor != null) {
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

                Claim claimNameDoctor = new Claim(ClaimTypes.Name, username);
                identity.AddClaim(claimNameDoctor);

                Claim claimIdDoctor = new Claim(ClaimTypes.NameIdentifier, doctor.DoctorNo.ToString());
                identity.AddClaim(claimIdDoctor);

                Claim claimRole = new Claim(ClaimTypes.Role, doctor.Especialidad);
                identity.AddClaim(claimRole);

                Claim claimSalario = new Claim("SALARIO", doctor.Salario.ToString());
                identity.AddClaim(claimSalario);

                if (doctor.DoctorNo == 982) {
                    identity.AddClaim(new Claim("Administrador", "Soy el admin"));
                }

                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);

                string controller = TempData["controller"].ToString();
                string action = TempData["action"].ToString();
                string id = TempData["inscripcion"].ToString();

                return RedirectToAction(action, controller, new { inscripcion = id });
            } else {
                ViewData["MENSAJE"] = "Usuario/Password incorrectas";
                return View();
            }
        }

        public async Task<IActionResult> ErrorAcceso() {
            return View();
        }

        public async Task<IActionResult> LogOut() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Enfermos", "Enfermo");
        }
    }
}
