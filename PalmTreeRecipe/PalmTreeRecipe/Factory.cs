using PalmTreeRecipe.Connectors;
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

    }
}
