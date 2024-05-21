using PetShop_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop_Business.Services.Abstracts
{
    public interface IProfessionalService
    {
        void AddProfessional(Professional professional);
        void DeleteProfesional(int id);
        void UpdateProfesional(int id, Professional professional);
        Professional GetProfessional(Func<Professional, bool>? func = null);
        List<Professional> GetAllProfessionals(Func<Professional, bool>? func = null);
    }
}
