using CourseStore.Core.Domain.Contracts;
using CourseStore.Core.Domain.Entities;
using CourseStore.Infrastructures.DataAccess.DbContexts;

namespace CourseStore.Infrastructures.DataAccess.Repositories
{
    public class CourseRepository : BaseRepository<Course, CourseStoreContext>, ICourseRepository
    {
        public CourseRepository(CourseStoreContext dbContext) : base(dbContext)
        {
        }
    }
}
