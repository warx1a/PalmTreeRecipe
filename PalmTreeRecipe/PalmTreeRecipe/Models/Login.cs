using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalmTreeRecipe.Models
{
    public class Login
    {

        public string username { get; set; }
        public string password { get; set; }
        public bool success { get; set; }
        public bool message { get; set; }

    }
}
