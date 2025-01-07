using LSC.OnlineCourse.Core.Models;
using LSC.OnlineCourse.Data;

namespace LSC.OnlineCourse.Service
{
    public interface ICourseService
    {
        Task<List<CourseModel>> GetAllCoursesAsync(int? categoryId = null);
        Task<CourseDetailModel> GetCourseDetailAsync(int courseId);
    }

    public class CourseService : ICourseService
    {
        private readonly ICourseRepository courseRepository;
        // private readonly IMapper mapper;

        public CourseService(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
            // this.mapper = mapper;
        }
        
        public Task<List<CourseModel>> GetAllCoursesAsync(int? categoryId = null)
        {
            return courseRepository.GetAllCourseAsync(categoryId);
        }


        public Task<CourseDetailModel> GetCourseDetailAsync(int courseId)
        {
            return courseRepository.GetCourseDetailAsync(courseId);
        }
    }
}