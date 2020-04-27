using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalmTreeRecipe.Models {
    public class RecipeSearch {

        public string TypedText { get; set; }

        public List<RecipeItemView> SearchResults { get; set; }

        public int SearchByUserID { get; set; }

    }
}
