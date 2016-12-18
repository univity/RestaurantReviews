using CMMI.API.Data.Context;
using CMMI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMI.API.Data
{
    /// <summary>
    /// The data repository used to manage reviews
    /// </summary>
    public class ReviewsRepository
    {
        /// <summary>
        /// Gets a review
        /// </summary>
        /// <param name="id">The id of the review to be retrieved</param>
        /// <returns>The review</returns>
        public static async Task<ReviewListModel> Get(int id)
        {
            using (var db = new CMMI.API.Data.Context.CMMIDemoData())
            {
                return await db.Reviews.Where(r => r.Id == id).Select(r => new ReviewListModel() { Id = r.Id, Restaurant = r.Restaurant.Restaurant1, Username = r.User.Username, Review = r.Review1, CreatedDate = r.CreatedDate }).SingleAsync();
            }
        }



        /// <summary>
        /// Creates a new review
        /// </summary>
        /// <param name="review">The review data to be created</param>
        /// <returns>Teh id of the new review</returns>
        public static async Task<int> Create(ReviewDetailModel review)
        {
            using (var db = new CMMI.API.Data.Context.CMMIDemoData())
            {
                var ety = new Review() { RestaurantId = review.RestaurantId, UserId = review.UserId, Review1 = review.Review, CreatedDate = DateTime.UtcNow } ;
                db.Reviews.Add(ety);
                await db.SaveChangesAsync();
                return ety.Id;
            }
        }

        /// <summary>
        /// Retrieves a list of reviews from a user
        /// </summary>
        /// <param name="username">The username to retrieve the list of reviews for.</param>
        /// <returns>The list of reviews</returns>
        public static async Task<List<ReviewListModel>> ListByUsername(string username)
        {
            using (var db = new CMMI.API.Data.Context.CMMIDemoData())
            {
                return await db.Reviews.Where(r => r.User.Username == username).Select(r => new ReviewListModel() { Id = r.Id, Restaurant = r.Restaurant.Restaurant1, Username = r.User.Username, Review = r.Review1, CreatedDate = r.CreatedDate }).ToListAsync();
            }
        }

        /// <summary>
        /// Deletes the review
        /// </summary>
        /// <param name="id">The id for the review</param>
        /// <returns></returns>
        public static async Task<int> Delete(int id)
        {
            using (var db = new CMMI.API.Data.Context.CMMIDemoData())
            {
                var ety = await db.Reviews.SingleAsync(r => r.Id == id);
                db.Reviews.Remove(ety);
                return await db.SaveChangesAsync();
            }
        }
    }

}
