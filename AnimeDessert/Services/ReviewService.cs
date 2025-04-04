using AnimeDessert.Data;
using AnimeDessert.Interfaces;
using AnimeDessert.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimeDessert.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;
        // dependency injection of database context
        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReviewDto>> ListReviews()
        {
            // include will join the (r)eview with 1 dessert
            List<Review> Reviews = await _context.Reviews
                .Include(r => r.Dessert)
                .ToListAsync();
            // empty list of data transfer object ReviewDto
            List<ReviewDto> ReviewDtos = new List<ReviewDto>();
            // foreach Review record in database
            foreach (Review Review in Reviews)
            {
                ReviewDto ReviewDto = new ReviewDto()
                {
                    ReviewId = Review.ReviewId,
                    ReviewNumber = Review.ReviewNumber,
                    ReviewContent = Review.ReviewContent,
                    ReviewTime = Review.ReviewTime,
                    ReviewUser = Review.ReviewUser,
                    DessertId = Review.DessertId,
                    DessertName = Review.Dessert?.DessertName,

                };

                // create new instance of ReviewDto, add to list
                ReviewDtos.Add(ReviewDto);
            }
            // return ReviewDtos
            return ReviewDtos;

        }

        public async Task<ReviewDto?> FindReview(int id)
        {
            // include will join (r)eview with 1 dessert
            // first or default async will get the first (r)eview matching the {id}
            var review = await _context.Reviews
                .Include(i => i.Dessert)
                .FirstOrDefaultAsync(r => r.ReviewId == id);

            // no review found
            if (review == null)
            {
                return null;
            }
            // create an instance of reviewDto
            ReviewDto reviewDto = new ReviewDto()
            {
                ReviewId = review.ReviewId,
                ReviewNumber = review.ReviewNumber,
                ReviewContent = review.ReviewContent,
                ReviewTime = review.ReviewTime,
                ReviewUser = review.ReviewUser,
                DessertId = review.DessertId,
                DessertName = review.Dessert?.DessertName
            };
            return reviewDto;

        }

        public async Task<ServiceResponse> UpdateReview(ReviewDto reviewDto)
        {
            ServiceResponse serviceResponse = new();

            Dessert? dessert = await _context.Desserts.FindAsync(reviewDto.DessertId);
            Review? review = await _context.Reviews.FindAsync(reviewDto.ReviewId);

            // Posted data must link to valid entity
            if (dessert == null)
            {
                serviceResponse.Status = ServiceStatus.NotFound;
                //404 Not Found
                return serviceResponse;
            }

            // Create instance of Review
            if (review == null)
            {
                serviceResponse.Messages.Add("Review could not be found");
                serviceResponse.Status = ServiceStatus.Error;
                return serviceResponse;
            }

            review.ReviewNumber = reviewDto.ReviewNumber;
            review.ReviewContent = reviewDto.ReviewContent;
            review.ReviewTime = reviewDto.ReviewTime;
            review.ReviewUser = reviewDto.ReviewUser;
            review.Dessert = dessert;
            review.DessertId = reviewDto.DessertId;


            //Review review = new Review()
            //{

            //    ReviewNumber =
            //    ReviewContent = reviewDto.ReviewContent,
            //    ReviewTime = reviewDto.ReviewTime,
            //    ReviewUser = reviewDto.ReviewUser,
            //    Dessert = dessert;
            //    DessertId = reviewDto.DessertId

            //};

            // flags that the object has changed
            _context.Entry(review).State = EntityState.Modified;



            try
            {
                //SQL Equivalent: Update Reviews set ... where ReviewId={id}
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                serviceResponse.Status = ServiceStatus.Error;
                serviceResponse.Messages.Add("An error occured updating the record");
                return serviceResponse;
            }
            serviceResponse.Status = ServiceStatus.Updated;

            return serviceResponse;
        }

        public async Task<ServiceResponse> AddReview(ReviewDto reviewDto)
        {
            ServiceResponse serviceResponse = new();
            Dessert? dessert = await _context.Desserts.FindAsync(reviewDto.DessertId);

            // Data must link to a valid entity
            if (dessert == null)
            {
                serviceResponse.Status = ServiceStatus.NotFound;
                if (dessert == null)
                {
                    serviceResponse.Messages.Add("Dessert was not found. ");
                }

                return serviceResponse;
            }

            Review review = new Review()
            {
                ReviewNumber = reviewDto.ReviewNumber,
                ReviewContent = reviewDto.ReviewContent,
                ReviewTime = reviewDto.ReviewTime,
                ReviewUser = reviewDto.ReviewUser,
                Dessert = dessert,
                DessertId = reviewDto.DessertId,


            };
            // SQL Equivalent: Insert into reviews (..) values (..)

            try
            {
                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Status = ServiceStatus.Error;
                serviceResponse.Messages.Add("There was an error adding the Review.");
                serviceResponse.Messages.Add(ex.Message);

            }


            serviceResponse.Status = ServiceStatus.Created;
            serviceResponse.CreatedId = review.ReviewId;
            return serviceResponse;
        }



        public async Task<ServiceResponse> DeleteReview(int id)
        {
            ServiceResponse response = new();
            // Review must exist in the first place
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                response.Status = ServiceStatus.NotFound;
                response.Messages.Add("Review cannot be deleted because it does not exist.");
                return response;
            }

            try
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                response.Status = ServiceStatus.Error;
                response.Messages.Add("Error encountered while deleting the review");
                return response;
            }

            response.Status = ServiceStatus.Deleted;

            return response;

        }

        public async Task<IEnumerable<ReviewDto>> ListReviewsForDessert(int id)
        {
            // WHERE dessertid == id
            List<Review> reviews = await _context.Reviews
                .Include(r => r.Dessert)
                .Where(r => r.DessertId == id)
                .ToListAsync();

            // empty list of data transfer object ReviewDto
            List<ReviewDto> reviewDtos = new List<ReviewDto>();
            // foreach Review record in database
            foreach (Review review in reviews)
            {
                // create new instance of ReviewDto, add to list
                reviewDtos.Add(new ReviewDto()
                {
                    ReviewId = review.ReviewId,
                    ReviewNumber = review.ReviewNumber,
                    ReviewContent = review.ReviewContent,
                    ReviewTime = review.ReviewTime,
                    ReviewUser = review.ReviewUser,
                    DessertId = review.DessertId,
                    DessertName = review.Dessert?.DessertName
                });
            }
            // return ReviewDtos
            return reviewDtos;

        }

    }
}
