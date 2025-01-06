using System.Collections.Generic;
using LSC.OnlineCourse.Core.Data;
using LSC.OnlineCourse.Core.Models;
using Microsoft.EntityFrameworkCore;


namespace LSC.OnlineCourse.Data
{
    //There's a concept called primary ctor in dot net 8 where we can add the constructor parameters to the class itself
    // public class CourcseCategoryRepository(OnlineCourseDbContext dbContext) : ICourseCategoryRepository
    //private readonly OnlineCourseDbContext _dbContext=dbContext ;

    public class CourseCategoryRepository : ICourseCategoryRepository
    {
      private readonly OnlineCourseDbContext _dbContext;
        public CourseCategoryRepository(OnlineCourseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Whenever we are dealing with primary key values to find eements there's a possibility of null values being returned that's y adding ? to the return type is important
        public async Task<CourseCategory?> GetByIdAsync(int id)
        {
         var data = await _dbContext.CourseCategories.FindAsync(id).AsTask();
          return data; 
        }

        public async Task<List<CourseCategory>> GetCourseCategoriesAsync()
        {
            var data = await _dbContext.CourseCategories.ToListAsync();
            return data;
        }
    }
}