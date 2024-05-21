using Microsoft.AspNetCore.Hosting;
using PetShop_Business.Exceptions;
using PetShop_Business.Services.Abstracts;
using PetShop_Core.Models;
using PetShop_Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop_Business.Services.Concretes
{
    public class ProfessionalService : IProfessionalService
    {

        private readonly IProfessionalRepository _professionalRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProfessionalService(IProfessionalRepository professionalRepository, IWebHostEnvironment webHostEnvironment)
        {
            _professionalRepository = professionalRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public void AddProfessional(Professional professional)
        {
            if (professional == null)
                throw new NullReferenceException("Professional not found");

            if (professional.ImgFile == null)
                throw new ImageNotFoundException("ImgFile", "Image is required");

            if (!professional.ImgFile.ContentType.Contains("image/"))
                throw new FileContentTypeException("ImgFile", "File content type error");
            if (professional.ImgFile.Length > 2097152)
                throw new FileSizeException("ImgFile", "File size error");

            string fileName = professional.ImgFile.FileName;
            string path = _webHostEnvironment.WebRootPath + @"\upload\professional\" + fileName;
            using(FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                professional.ImgFile.CopyTo(fileStream);
            }
            professional.ImgUrl = fileName;

            _professionalRepository.Add(professional);
            _professionalRepository.Commit();
        }

        public void DeleteProfesional(int id)
        {
            var existProfessional = _professionalRepository.Get(x => x.Id == id);
            if (existProfessional == null)
                throw new EntityNotFoundException("", "Entity not found");

            string path = _webHostEnvironment.WebRootPath + @"\upload\professional\" + existProfessional.ImgUrl;
            if (!File.Exists(path))
                throw new Exceptions.FileNotFoundException("Img", "File not found");

            File.Delete(path);

            _professionalRepository.Delete(existProfessional);
            _professionalRepository.Commit();
        }

        public List<Professional> GetAllProfessionals(Func<Professional, bool>? func = null)
        {
            return _professionalRepository.GetAll(func);
        }

        public Professional GetProfessional(Func<Professional, bool>? func = null)
        {
            return _professionalRepository.Get(func);
        }

        public void UpdateProfesional(int id, Professional professional)
        {
            var existProfessional = _professionalRepository.Get(x => x.Id == id);
            if (existProfessional == null)
                throw new EntityNotFoundException("", "Entity not found");

            if (professional.ImgFile != null)
            {
                if (!professional.ImgFile.ContentType.Contains("image/"))
                    throw new FileContentTypeException("ImgFile", "File content type error");
                if (professional.ImgFile.Length > 2097152)
                    throw new FileSizeException("ImgFile", "File size error");

                string fileName = professional.ImgFile.FileName;
                string path = _webHostEnvironment.WebRootPath + @"\upload\professional\" + fileName;
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    professional.ImgFile.CopyTo(fileStream);
                }
                professional.ImgUrl = fileName;

                existProfessional.ImgUrl = professional.ImgUrl;
            }

            existProfessional.Fullname = professional.Fullname;
            existProfessional.Description = professional.Description;

            _professionalRepository.Commit();

        }
    }
}
