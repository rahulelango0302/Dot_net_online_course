using LSC.OnlineCourse.Core.Data;
using LSC.OnlineCourse.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LSC.OnlineCourse.Data
{
    public class CourseRepository:ICourseRepository
    {
        private readonly OnlineCourseDbContext _dbContext;
        public CourseRepository(OnlineCourseDbContext dbContext)    
        {
            _dbContext = dbContext;
        }
        public async Task<List<CourseModel>> GetAllCourseAsync(int? categoryId = null)
        {
            //We first build a query to dynamically add filters to the query  
            // remember, this query is called deffered execution. it won't run untill we await or use sync method . ToList()
            var query = _dbContext.Courses.Include(c=>c.Category).AsQueryable();

            //Apply the filer if categoryid is provided
            if(categoryId.HasValue){
                query = query.Where(c=>c.CategoryId == categoryId.Value);
            }
             var courses = await query
                .Select(s => new CourseModel
                {
                    CourseId = s.CourseId,
                    Title = s.Title,
                    Description = s.Description,
                    Price = s.Price,
                    CourseType = s.CourseType,
                    SeatsAvailable = s.SeatsAvailable,
                    Duration = s.Duration,
                    CategoryId = s.CategoryId,
                    InstructorId = s.InstructorId,
                    // Thumbnail = s.Thumbnail,
                    // InstructorUserId = s.Instructor.UserId,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    Category = new CourseCategoryModel
                    {
                        CategoryId = s.Category.CategoryId,
                        CategoryName = s.Category.CategoryName,
                        Description = s.Category.Description
                    },
                    UserRating = new UserRatingModel
                    {
                        CourseId = s.CourseId,
                        AverageRating = s.Reviews.Any() ? Convert.ToDecimal(s.Reviews.Average(r => r.Rating)) : 0,
                        TotalRating = s.Reviews.Count
                    }
                })
                .ToListAsync();

            return courses;
        }

        public async Task<CourseDetailModel?> GetCourseDetailAsync(int courseId)
        {
            // If means to pull alll the associatd records for a given course. also it is possible only when tables are related. Like having primary & foreign key relationship
           var course = await _dbContext.Courses
                .Include(c => c.Category)
                .Include(c => c.Reviews)
                .Include(c => c.SessionDetails)
                .Where(c => c.CourseId == courseId)
                .Select(c => new CourseDetailModel
                {
                    CourseId = c.CourseId,
                    Title = c.Title,
                    Description = c.Description,
                    Price = c.Price,
                    CourseType = c.CourseType,
                    SeatsAvailable = c.SeatsAvailable,
                    Duration = c.Duration,
                    CategoryId = c.CategoryId,
                    InstructorId = c.InstructorId,
                    // InstructorUserId = c.Instructor.UserId,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    // Thumbnail = c.Thumbnail,
                    Category = new CourseCategoryModel
                    {
                        CategoryId = c.Category.CategoryId,
                        CategoryName = c.Category.CategoryName,
                        Description = c.Category.Description
                    },
                    Reviews = c.Reviews.Select(r => new UserReviewModel
                    {
                        CourseId = r.CourseId,
                        ReviewId = r.ReviewId,
                        UserId = r.UserId, 
                        UserName = r.User.DisplayName,
                        Rating = r.Rating,
                        Comments = r.Comments,
                        ReviewDate = r.ReviewDate
                    }).OrderByDescending(o => o.Rating).Take(10).ToList(),
                    SessionDetails = c.SessionDetails.Select(sd => new SessionDetailModel
                    {
                        SessionId = sd.SessionId,
                        CourseId = sd.CourseId,
                        Title = sd.Title,
                        Description = sd.Description,
                        VideoUrl = sd.VideoUrl,
                        VideoOrder = sd.VideoOrder
                    }).OrderBy(o => o.VideoOrder).ToList()
                    ,
                    UserRating = new UserRatingModel
                    {
                        CourseId = c.CourseId,
                        AverageRating = c.Reviews.Any() ? Convert.ToDecimal(c.Reviews.Average(r => r.Rating)) : 0,
                        TotalRating = c.Reviews.Count
                    }
                })
                .FirstOrDefaultAsync();

            return course;


        }

    
    }
}