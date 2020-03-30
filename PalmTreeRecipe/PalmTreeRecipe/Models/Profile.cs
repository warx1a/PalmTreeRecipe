using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalmTreeRecipe.Models {
    public class Profile:
        BaseModel {

        public Profile()
        {

        }

        public User user { get; set; }

    }
}
