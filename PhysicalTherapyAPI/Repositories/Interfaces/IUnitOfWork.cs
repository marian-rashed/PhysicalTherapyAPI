using PhysicalTherapyAPI.Repositories.Interfaces;

namespace PhysicalTherapyAPI.Repositories.Inplementation
{
    public interface IUnitOfWork
    {
        IExerciseRepository ExerciseRepository { get; set; }
        ICategoryRepository CategoryRepository { get; set; }
        void save();
    }
}
