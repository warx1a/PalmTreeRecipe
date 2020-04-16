using PalmTreeRecipe.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PalmTreeRecipe.Connectors {
    public class RecipeEndpoint:
        DataConnector{

        public Recipe addRecipe(Recipe recipe)
        {
            string query = "INSERT INTO [Recipe] (userId, recipeName, stepJSON, recipeIcon, recipeTags, ingredientJSON, createdOnDateTime) VALUES (@userid, @recipename, @stepjson, @recipeicon, @recipetags, @ingredients, @createdOn)";
            using(SqlConnection conn = new SqlConnection(DB_URL))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userid", recipe.UserID);
                cmd.Parameters.AddWithValue("@recipename", recipe.RecipeName);
                cmd.Parameters.AddWithValue("@stepjson", recipe.Steps);
                //TODO: going to add these fields in eventually
                cmd.Parameters.AddWithValue("@recipeicon", DBNull.Value);
                cmd.Parameters.AddWithValue("@recipetags", DBNull.Value);
                cmd.Parameters.AddWithValue("@ingredients", recipe.Ingredients);
                cmd.Parameters.AddWithValue("@createdOn", recipe.CreatedOnDateTime);
                int rowsAffected = cmd.ExecuteNonQuery();
                if(rowsAffected.Equals(1))
                {
                    recipe.success = true;
                }
            }
            return recipe;
        }

        public List<Recipe> getLatestRecipes()
        {
            List<Recipe> latestRecipes = new List<Recipe>();
            string query = "SELECT & FROM [Recipe]";
            using(SqlConnection conn = new SqlConnection(DB_URL))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader results = cmd.ExecuteReader();
                while(results.Read())
                {
                    Recipe r = new Recipe();
                    r.RecipeID = getValueOrDefault<int>(0, results);
                    r.RecipeName = getValueOrDefault<string>(2, results);
                    r.UserID = getValueOrDefault<int>(1, results);
                    r.Steps = getValueOrDefault<string>(3, results);
                    r.Icon = getValueOrDefault<string>(4, results);
                    r.Tags = getValueOrDefault<string>(5, results);
                    r.Ingredients = getValueOrDefault<string>(6, results);
                    latestRecipes.Add(r);
                }
            }
            return latestRecipes;
        }

    }
}
