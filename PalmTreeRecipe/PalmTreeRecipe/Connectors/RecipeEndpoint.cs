using Newtonsoft.Json;
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
                cmd.Parameters.AddWithValue("@stepjson", JsonConvert.SerializeObject(recipe.Steps));
                //TODO: going to add these fields in eventually
                cmd.Parameters.AddWithValue("@recipeicon", DBNull.Value);
                cmd.Parameters.AddWithValue("@recipetags", DBNull.Value);
                cmd.Parameters.AddWithValue("@ingredients", JsonConvert.SerializeObject(recipe.Ingredients));
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
            string query = "SELECT & FROM [Recipe] ORDER BY createdOnDateTime ASC";
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
                    r.Steps = JsonConvert.DeserializeObject<List<Step>>(getValueOrDefault<string>(3, results));
                    r.Icon = getValueOrDefault<string>(4, results);
                    r.Tags = getValueOrDefault<string>(5, results);
                    r.Ingredients = JsonConvert.DeserializeObject<List<Ingredient>>(getValueOrDefault<string>(6, results));
                    r.CreatedOnDateTime = getValueOrDefault<DateTime>(7, results);
                    latestRecipes.Add(r);
                }
            }
            return latestRecipes;
        }

        public List<Recipe> searchRecipes(RecipeSearch search)
        {
            List<Recipe> searchResults = new List<Recipe>();

            bool bHasTypedText = !string.IsNullOrEmpty(search.TypedText);
            bool bHasUserID = !search.SearchByUserID.Equals(0);

            string query = "SELECT * FROM [Recipe]";
            if(bHasTypedText)
            {
                query += " WHERE [recipeName] LIKE %@name%";
            }
            //if we have typed text it'll be an "an". if not we're only comparing against user id
            if(bHasUserID && bHasTypedText)
            {
                query += " AND [userId] = @id";
            } else if(bHasUserID && !bHasTypedText)
            {
                query += " WHERE [userId] = @id";
            }
            using(SqlConnection conn = new SqlConnection(DB_URL))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                if(bHasTypedText)
                {
                    cmd.Parameters.AddWithValue("@name", search.TypedText);
                }
                if(bHasUserID)
                {
                    cmd.Parameters.AddWithValue("@id", search.SearchByUserID);
                }
                SqlDataReader results = cmd.ExecuteReader();
                while(results.Read())
                {
                    Recipe r = new Recipe();
                    r.RecipeID = getValueOrDefault<int>(0, results);
                    r.RecipeName = getValueOrDefault<string>(2, results);
                    r.UserID = getValueOrDefault<int>(1, results);
                    string stepJSON = getValueOrDefault<string>(3, results);
                    r.Steps = JsonConvert.DeserializeObject<List<Step>>(stepJSON);
                    r.Icon = getValueOrDefault<string>(4, results);
                    r.Tags = getValueOrDefault<string>(5, results);
                    string ingredientJSON = getValueOrDefault<string>(6, results);
                    r.Ingredients = JsonConvert.DeserializeObject<List<Ingredient>>(ingredientJSON);
                    r.CreatedOnDateTime = getValueOrDefault<DateTime>(7, results);

                    searchResults.Add(r);
                }
            }
            return searchResults;
        }

        public Recipe getRecipeByID(int RecipeID)
        {
            string query = "SELECT * FROM [Recipe] WHERE [recipeId] = @id";
            using(SqlConnection conn = new SqlConnection(DB_URL))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", RecipeID);
                SqlDataReader results = cmd.ExecuteReader();
                if(results.Read())
                {
                    Recipe r = new Recipe();
                    r.RecipeID = getValueOrDefault<int>(0, results);
                    r.RecipeName = getValueOrDefault<string>(2, results);
                    r.UserID = getValueOrDefault<int>(1, results);
                    r.Steps = JsonConvert.DeserializeObject<List<Step>>(getValueOrDefault<string>(3, results));
                    r.Icon = getValueOrDefault<string>(4, results);
                    r.Tags = getValueOrDefault<string>(5, results);
                    r.Ingredients = JsonConvert.DeserializeObject<List<Ingredient>>(getValueOrDefault<string>(6, results));
                    r.CreatedOnDateTime = getValueOrDefault<DateTime>(7, results);
                    return r;
                }
            }
            return null;
        }

    }
}
