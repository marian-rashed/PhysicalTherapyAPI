using PhysicalTherapyAPI.Models;
using PhysicalTherapyAPI.Repositories.Interfaces;
using Microsoft.CodeAnalysis.Operations;
using EntityFrameworkCore.GenericRepository;

namespace PhysicalTherapyAPI.Repositories.Inplementation
{
    public class CategoryRepository:Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext Context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            this.Context = context;
        }
    }
}
