using PhysicalTherapyAPI.DTOs;
using PhysicalTherapyAPI.Models;
using PhysicalTherapyAPI.Repositories.Inplementation;
namespace PhysicalTherapyAPI.Repositories.Interfaces
{
    public interface IExerciseRepository : IRepository<Exercise>
    {
        IEnumerable<Exercise> GetExercisesByCatId(int CatId);
        dynamic GetFilteredExercises(FilterationDto filterObj, string? includeProps = null);
    }
}
