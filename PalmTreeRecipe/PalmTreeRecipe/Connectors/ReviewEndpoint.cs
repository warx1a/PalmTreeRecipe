using PalmTreeRecipe.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PalmTreeRecipe.Connectors {
    public class ReviewEndpoint:
        DataConnector{

        public bool addReview(Review review)
        {
            string query = "INSERT INTO Review(userId, recipeId, reviewText, reviewTimestamp, rating) VALUES (@user, @recipe, @text, @timestamp, @rating)";
            using(SqlConnection conn = new SqlConnection(DB_URL))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", review.userId);
                cmd.Parameters.AddWithValue("@recipe", review.recipeId);
                cmd.Parameters.AddWithValue("@text", review.text);
                cmd.Parameters.AddWithValue("@timestamp", review.timestamp);
                cmd.Parameters.AddWithValue("@rating", review.rating);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected.Equals(1);
            }
        }

        public List<Review> getReviewsForRecipe(int recipeId)
        {
            List<Review> recipeReviews = new List<Review>();
            string query = "SELECT * FROM Review WHERE [recipeId] = @id ORDER BY [reviewTimestamp] ASC";
            using(SqlConnection conn = new SqlConnection(DB_URL))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", recipeId);
                SqlDataReader results = cmd.ExecuteReader();
                while(results.Read())
                {
                    int reviewId = getValueOrDefault<int>(0, results);
                    int userId = getValueOrDefault<int>(1, results);
                    string text = getValueOrDefault<string>(3, results);
                    DateTime timestamp = getValueOrDefault<DateTime>(4, results);
                    int rating = getValueOrDefault<int>(5, results);
                    Review review = new Review(reviewId, userId, recipeId, text, timestamp, rating);
                    recipeReviews.Add(review);
                }
            }
            return recipeReviews;
        }

    }
}
