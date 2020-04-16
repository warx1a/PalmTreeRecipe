using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalmTreeRecipe.Models
{
    public class Recipe : BaseModel {

        public Recipe()
        {

        }

        public Recipe(int RecipeID, int UserID, string Tags, string RecipeName, string Ingredients, string steps)
        {
            this.RecipeID = RecipeID;
            this.UserID = UserID;
            this.Tags = Tags;
            this.RecipeName = RecipeName;
            this.Ingredients = Ingredients;
            this.Steps = steps;
        }

        public string Icon { get; set; }
        public int RecipeID { get; set; }
        public int UserID { get; set; }
        public string Tags { get; set; }
        public string RecipeName { get; set; }
        public string Ingredients { get; set; }
        public string Steps { get; set; }
        public DateTime CreatedOnDateTime { get; set; }

    }
}
