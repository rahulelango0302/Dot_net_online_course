using LSC.OnlineCourse.Core.Models;

namespace LSC.OnlineCourse.Data
{
    public interface ICourseRepository
    {

        //For Async methods it will awlays return Task of T type and usually as a practice we would keep async as prefix to that action 
        //Refer to the ? added to the return type to handle null values
        GetAllCourseAsync(int categoryId=null);
        Task<> GetCourseDetailAsync(int courseId);
    }
}