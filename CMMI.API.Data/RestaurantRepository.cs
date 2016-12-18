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
    /// The data reporsitory used for handling restaurants
    /// </summary>
    public class RestaurantRepository
    {
        /// <summary>
        /// Gets a restaurant
        /// </summary>
        /// <param name="id">The id of hte restaurant</param>
        /// <returns>An instance of the restaurant</returns>
        public static async Task<RestaurantListModel> Get(int id)
        {
            using (var db = new CMMI.API.Data.Context.CMMIDemoData())
            {
                return await db.ViewRestaurants.Where(r => r.Id == id).Select(r => new RestaurantListModel() { Id = r.Id, City = r.City, Cuisine = r.Cuisine, Restaurant = r.Restaurant, UserId = r.CreatedBy }).SingleAsync();
            }
        }

        /// <summary>
        /// Creates a new restaurant record
        /// </summary>
        /// <param name="restaurant">The restuarant to be created</param>
        /// <returns>The id of the newly created restaurant.</returns>
        public static async Task<int> Create(RestaurantDetailModel restaurant)
        {
            //Created a basic ado implementation.  If this became more common I would build a more robust library for handling the SqlClient needs.
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CMMIDemoConnectionString"].ConnectionString))
            {
                await cn.OpenAsync();

                using (var cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "RestaurantCreate";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@restaurant", DbType = System.Data.DbType.String, Size = 256, Direction = System.Data.ParameterDirection.Input, Value = restaurant.Restaurant });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@cuisine", DbType = System.Data.DbType.Int16, Direction = System.Data.ParameterDirection.Input, Value = restaurant.CuisineId });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@address", DbType = System.Data.DbType.String, Size = 256, Direction = System.Data.ParameterDirection.Input, Value = restaurant.Address });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@address2", DbType = System.Data.DbType.String, Size = 256, Direction = System.Data.ParameterDirection.Input, Value = restaurant.Address2 });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@city", DbType = System.Data.DbType.String, Size = 256, Direction = System.Data.ParameterDirection.Input, Value = restaurant.City });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@state", DbType = System.Data.DbType.Int16, Direction = System.Data.ParameterDirection.Input, Value = restaurant.StateId });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@zip", DbType = System.Data.DbType.StringFixedLength, Size = 5, Direction = System.Data.ParameterDirection.Input, Value = restaurant.Zipcode });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@userId", DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Input, Value = restaurant.UserId });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@id", DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Output });

                    await cmd.ExecuteNonQueryAsync();

                    return (int)cmd.Parameters["@id"].Value;
                }
            }
        }

        /// <summary>
        /// Returns a list of restaurant based off of the city.
        /// </summary>
        /// <param name="city">The city to search for restaurants in</param>
        /// <returns>The list of restaurants in the city</returns>
        public static async Task<List<RestaurantListModel>> ListByCity(string city)
        {
            using (var db = new CMMI.API.Data.Context.CMMIDemoData())
            {
                return await db.ViewRestaurants.Where(r => r.City.StartsWith(city, StringComparison.CurrentCultureIgnoreCase)).Select(r => new RestaurantListModel() { Id = r.Id, City = r.City, Cuisine = r.Cuisine, Restaurant = r.Restaurant, UserId = r.CreatedBy }).ToListAsync();
            }
        }
    }
}
