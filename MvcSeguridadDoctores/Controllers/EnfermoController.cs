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

        [AuthorizeHospital(Policy = "PERMISOSELEVADOS")]
        public async Task<IActionResult> DeleteEnfermo(int id) {
            return View();
        }

        [AuthorizeHospital]
        [HttpPost]
        [ActionName("DeleteEnfermo")]
        public async Task<IActionResult> DeleteEnfermoPost(int id) {
            await this.repo.DeleteEnfermoAsync(id);
            return RedirectToAction("Enfermos");
        }

    }
}
