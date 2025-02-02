using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAuth.Entities.Data;
using UserAuth.Entities.Models;
using UserAuth.Repository.Interface;

namespace UserAuth.Repository.Repository
{
    public class CourseRepository: GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(AppDbContext context):base(context) { }
    }
}
