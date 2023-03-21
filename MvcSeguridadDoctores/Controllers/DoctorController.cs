using Microsoft.AspNetCore.Mvc;
using MvcSeguridadDoctores.Filters;
using MvcSeguridadDoctores.Models;
using MvcSeguridadDoctores.Repositories;

namespace MvcSeguridadDoctores.Controllers {
    public class DoctorController : Controller {

        private RepositoryHospital repo;

        public DoctorController(RepositoryHospital repo) {
            this.repo = repo;
        }

        [AuthorizeHospital]
        public async Task<IActionResult> PerfilDoctor() {
            string apellido = HttpContext.User.Identity.Name;
            Doctor doctor = await this.repo.PerfilDoctorAsync(apellido);
            return View(doctor);
        }

    }
}
