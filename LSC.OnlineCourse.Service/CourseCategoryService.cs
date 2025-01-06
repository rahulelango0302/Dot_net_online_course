using System.Collections.Generic;
using System.Threading.Tasks;
using LSC.OnlineCourse.Data;
using LSC.OnlineCourse.Service;
namespace LSC.OnlineCourse.Service
{
    

    public class CourseCategoryService : ICourseCategoryService
    {
       private readonly ICourseCategoryRepository _courseCategoryRepository;
       public CourseCategoryService(ICourseCategoryRepository courseCategoryRepository)
       {

        _courseCategoryRepository = courseCategoryRepository;
        
       }
        public async Task<Core.Models.CourseCategoryModel?> GetByIdAsync(int id)
        {
            var data = await _courseCategoryRepository.GetByIdAsync(id);
            var returnData = new Core.Models.CourseCategoryModel
            {
                CategoryId = data.CategoryId,
                CategoryName = data.CategoryName,
                Description = data.Description
            };

            return returnData;
        }

        public async Task<List<Core.Models.CourseCategoryModel>> GetCourseCategoriesAsync()
        {
            var data = await _courseCategoryRepository.GetCourseCategoriesAsync();
            var returnData = new List<Core.Models.CourseCategoryModel>();
            returnData=data.Select(x => new Core.Models.CourseCategoryModel
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                Description = x.Description
            }).ToList();

            return returnData;
        }
    }

}