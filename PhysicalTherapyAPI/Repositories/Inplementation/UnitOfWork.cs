using AutoMapper;
using PhysicalTherapyAPI.Repositories.Inplementation;
using PhysicalTherapyAPI.Repositories.Interfaces;

namespace PhysicalTherapyAPI.Repositories.Inplementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext Context;
        public IExerciseRepository ExerciseRepository { get ; set ; }
        public ICategoryRepository CategoryRepository { get ; set ; }

        public UnitOfWork(ApplicationDbContext context, IMapper mapper)
        {
            this.Context = context;
            ExerciseRepository = new ExerciseRepository(context);
            CategoryRepository = new CategoryRepository(context);
        }

        public void save()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
