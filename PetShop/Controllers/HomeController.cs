using Microsoft.AspNetCore.Mvc;
using PetShop_Business.Services.Abstracts;
using PetShop_Core.Models;
using System.Diagnostics;

namespace PetShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProfessionalService _professionalService;

        public HomeController(IProfessionalService professionalService)
        {
            _professionalService = professionalService;
        }

        public IActionResult Index()
        {
            List<Professional> professionals = _professionalService.GetAllProfessionals();
            return View(professionals);
        }

    }
}