using PalmTreeRecipe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalmTreeRecipe.Methods {
    public class RecipeValidation {

        public bool ValidateAddUpdateRecipe(Recipe recipe, ref List<string> errors)
        {
            if(string.IsNullOrEmpty(recipe.RecipeName))
            {
                errors.Add("Recipe name cannot be empty");
            }
            if(string.IsNullOrEmpty(recipe.Steps))
            {
                errors.Add("Recipe must have at least 1 step");
            }
            if(string.IsNullOrEmpty(recipe.Ingredients))
            {
                errors.Add("Recipe must have at least 1 ingredient");
            }
            //if we have no errors then validation passed
            return errors.Count.Equals(0);
        }

    }
}
