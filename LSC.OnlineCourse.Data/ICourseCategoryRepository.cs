using LSC.OnlineCourse.Core.Models;

namespace LSC.OnlineCourse.Data
{
    public interface ICourseCategoryRepository
    {

        //For Async methods it will awlays return Task of T type and usually as a practice we would keep async as prefix to that action 
        //Refer to the ? added to the return type to handle null values
       Task<CourseCategory?> GetByIdAsync(int id);
        Task<List<CourseCategory>> GetCourseCategoriesAsync();
    }
}