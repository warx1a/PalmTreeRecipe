using PalmTreeRecipe.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PalmTreeRecipe.Connectors {
    public class RecipeEndpoint:
        DataConnector{

        public Recipe addRecipe(User recipeOwner, Recipe recipe)
        {
            string query = "INSERT INTO [Recipe] (userId, recipeName, stepJSON, recipeIcon, recipeTags, ingredientJSON) VALUES (@userid, @recipename, @stepjson, @recipeicon, @recipetags, @ingredients)";
            using(SqlConnection conn = new SqlConnection(DB_URL))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userid", recipeOwner.userId);
                cmd.Parameters.AddWithValue("@recipename", recipe.RecipeName);
                cmd.Parameters.AddWithValue("@stepjson", recipe.Steps);
                //TODO: going to add these fields in eventually
                cmd.Parameters.AddWithValue("@recipeicon", DBNull.Value);
                cmd.Parameters.AddWithValue("@recipetags", DBNull.Value);
                cmd.Parameters.AddWithValue("@ingredients", recipe.Ingredients);
                int rowsAffected = cmd.ExecuteNonQuery();

            }
            return recipe;
        }

    }
}
