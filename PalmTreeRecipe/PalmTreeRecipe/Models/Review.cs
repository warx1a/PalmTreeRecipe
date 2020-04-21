using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalmTreeRecipe.Models
{
    public class Review : BaseModel
    {

        public int reviewId { get; set; }
        public int recipeId { get; set; }
        public int rating { get; set; }
        public string text { get; set; }
        public int userId { get; set; }
        public DateTime timestamp { get; set; }

        public Review()
        {

        }

        public Review(int reviewId, int userId, int recipeId, string text, DateTime timestamp, int rating)
        {
            this.reviewId = reviewId;
            this.userId = userId;
            this.recipeId = recipeId;
            this.text = text;
            this.timestamp = timestamp;
            this.rating = rating;
        }

    }
}
