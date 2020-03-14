using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalmTreeRecipe.Models
{
    public class User
    {

        public int userId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string emailAddress { get; set; }
        public string sessionId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int userType { get; set; }
        public string confirmPassword { get; set; }

        public User()
        {

        }

        public User(int userId, string username, string password, string emailAddress, string sessionId, string firstName, string lastName, int userType)
        {
            this.userId = userId;
            this.username = username;
            this.password = password;
            this.emailAddress = emailAddress;
            this.sessionId = sessionId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.userType = userType;
        }

    }
}
