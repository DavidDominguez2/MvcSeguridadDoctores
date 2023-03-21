using Microsoft.AspNetCore.Mvc;
using MvcSeguridadDoctores.Filters;
using MvcSeguridadDoctores.Models;
using MvcSeguridadDoctores.Repositories;

namespace MvcSeguridadDoctores.Controllers {
    public class EnfermoController : Controller {

        private RepositoryHospital repo;

        public EnfermoController(RepositoryHospital repo) {
            this.repo = repo;
        }

        public async Task<IActionResult> Enfermos() {
            List<Enfermo> enfermos = await this.repo.GetEnfermosAsync();
            return View(enfermos);
        }

        [AuthorizeHospital]
        public async Task<IActionResult> DeleteEnfermo() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEnfermo(int inscripcion) {
            await this.repo.DeleteEnfermoAsync(inscripcion);
            return RedirectToAction("Enfermos");
        }

    }
}
