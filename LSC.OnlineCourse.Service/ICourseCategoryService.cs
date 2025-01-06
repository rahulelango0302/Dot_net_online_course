//Service files are used for writing business logics in it
//Entity class should be exposed as model to controller 
using LSC.OnlineCourse.Core.Models;

namespace LSC.OnlineCourse.Service
{
    public interface ICourseCategoryService
    {
               Task<CourseCategoryModel?> GetByIdAsync(int id);
                Task<List<CourseCategoryModel>> GetCourseCategoriesAsync();
    }
}