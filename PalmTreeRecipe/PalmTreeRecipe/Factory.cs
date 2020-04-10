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

    }
}
