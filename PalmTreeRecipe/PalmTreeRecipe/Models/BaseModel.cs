using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalmTreeRecipe.Models {
    public class BaseModel {

        public List<string> errorMessages { get; set; }

        public bool success { get; set; }

        public BaseModel()
        {
            this.errorMessages = new List<string>();
        }

    }
}
