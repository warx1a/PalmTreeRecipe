using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalmTreeRecipe.Models {
    public class RecipeItemView {

        public Recipe Recipe { get; set; }
        public User CreatedByUser { get; set; }
        public List<Review> Reviews { get; set; }

    }
}
