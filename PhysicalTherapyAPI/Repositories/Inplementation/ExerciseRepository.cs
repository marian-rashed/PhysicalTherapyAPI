using PhysicalTherapyAPI.Models;
using PhysicalTherapyAPI.Repositories.Interfaces;
using Microsoft.CodeAnalysis.Operations;
using EntityFrameworkCore.GenericRepository;
using PhysicalTherapyAPI.DTOs;

namespace PhysicalTherapyAPI.Repositories.Inplementation
{
    public class ExerciseRepository:Repository<Exercise>, IExerciseRepository
    {
        private readonly ApplicationDbContext Context;
        public ExerciseRepository(ApplicationDbContext context) : base(context)
        {
            this.Context = context;
        }
        public IEnumerable<Exercise> GetExercisesByCatId(int catId)
        {
            return Context.Exercises.Where(e => e.CategoryId == catId);
        }
        public dynamic GetFilteredExercises(FilterationDto filterObj, string? includeProps = null)
        {
            IQueryable<Exercise> exercises = GetAll(includeProps).AsQueryable();
            if (filterObj.CategoryId != null)
            {
                exercises = exercises.Where(c => c.CategoryId == filterObj.CategoryId);
            }
            if(filterObj.ExerciseType != null)
            {
                exercises = exercises.Where(c => c.ExerciseType == filterObj.ExerciseType);
            }
            int Count = exercises.Count();
            var totalPages = (int)Math.Ceiling((decimal)Count / filterObj.PageSize);
            List<Exercise> filteredExercises = exercises.Skip((filterObj.PageNumber - 1) * filterObj.PageSize)
                                         .Take(filterObj.PageSize)
                                         .ToList();
            return new { filteredExercises = filteredExercises , numOfExercises = Count , numOfPages = totalPages };
        }
    }
}
