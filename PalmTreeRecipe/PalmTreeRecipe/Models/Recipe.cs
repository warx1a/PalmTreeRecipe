using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalmTreeRecipe.Models
{
    public class Recipe : BaseModel
    {

        public Recipe()
        {

        }

        public string RecipeName { get; set; }
        public string Ingredients { get; set; }
        public string Steps { get; set; }

    }
}
