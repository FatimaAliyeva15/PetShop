using PetShop_Core.Models;
using PetShop_Core.RepositoryAbstracts;
using PetShop_Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop_Data.RepositoryConcretes
{
    public class ProfessionalRepository : GenericRepository<Professional>, IProfessionalRepository
    {
        public ProfessionalRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
