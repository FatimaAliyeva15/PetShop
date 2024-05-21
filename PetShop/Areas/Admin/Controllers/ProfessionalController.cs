using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShop_Business.Exceptions;
using PetShop_Business.Services.Abstracts;
using PetShop_Core.Models;
using FileNotFoundException = PetShop_Business.Exceptions.FileNotFoundException;

namespace PetShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProfessionalController : Controller
    {
        private readonly IProfessionalService _professionalService;

        public ProfessionalController(IProfessionalService professionalService)
        {
            _professionalService = professionalService;
        }

        public IActionResult Index()
        {
            List<Professional> professionals = _professionalService.GetAllProfessionals();
            return View(professionals);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Professional professional)
        {
            if(!ModelState.IsValid)
                return View();

            try
            {
                _professionalService.AddProfessional(professional);
            }
            catch(NullReferenceException ex)
            {
                return NotFound();
            }
            catch(FileContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (ImageNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var existProfessional = _professionalService.GetProfessional(x => x.Id == id);
            if(existProfessional == null)
                return NotFound();

            try
            {
                _professionalService.DeleteProfesional(id);
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(FileNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return RedirectToAction(nameof(Index));

        }

        public IActionResult Update(int id)
        {
            var existProfessional = _professionalService.GetProfessional(x => x.Id == id);
            if (existProfessional == null)
                return NotFound();

            return View(existProfessional);
        }

        [HttpPost]
        public IActionResult Update(int id, Professional professional)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _professionalService.UpdateProfesional(id, professional);
            }
            catch (NullReferenceException ex)
            {
                return NotFound();
            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (ImageNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
