using PalmTreeRecipe.Connectors;
using PalmTreeRecipe.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalmTreeRecipe {
    public class Factory {

        public Factory()
        {

        }


        private UserEndpoint _userEndpoint;
        public UserEndpoint userEndpoint {
            get {
                if(_userEndpoint == null)
                {
                    _userEndpoint = new UserEndpoint();
                }
                return _userEndpoint;
            }
        }

        private RecipeEndpoint _recipeEndpoint;
        public RecipeEndpoint recipeEndpoint {
            get {
                if(_recipeEndpoint == null)
                {
                    _recipeEndpoint = new RecipeEndpoint();
                }
                return _recipeEndpoint;
            }
        }

        private ReviewEndpoint _reviewEndpoint;
        public ReviewEndpoint reviewEndpoint {
            get {
                if(_reviewEndpoint == null)
                {
                    _reviewEndpoint = new ReviewEndpoint();
                }
                return _reviewEndpoint;
            }
        }

        private UserValidation _userValidation;
        public UserValidation userValidation {
            get {
                if(_userValidation == null)
                {
                    _userValidation = new UserValidation();
                }
                return _userValidation;
            }
        }

        private RecipeValidation _recipeValidation;
        public RecipeValidation recipeValidation {
            get {
                if(_recipeValidation == null)
                {
                    _recipeValidation = new RecipeValidation();
                }
                return _recipeValidation;
            }
        }

    }
}
