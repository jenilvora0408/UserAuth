using UserAuth.Entities.Data;
using UserAuth.Entities.Models;
using UserAuth.Repository.Interface;

namespace UserAuth.Repository.Repository
{
    public class StudentRepository: GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext context) : base(context) { }
    }
}
