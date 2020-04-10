using PalmTreeRecipe.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PalmTreeRecipe.Methods {
    public class UserValidation {

        private string alphaNumericRegex = @"(^[a-zA-Z0-9]+$)";
        private EmailAddressAttribute emailAttr = new EmailAddressAttribute();

        public bool ValidateAddUpdateProfileInfo(User user, bool isAdd, ref List<string> errors)
        {
            //add user validation here for the postback
            //username validation
            if (string.IsNullOrEmpty(user.username))
            {
                errors.Add("Username cannot be empty");
            }
            else if (!Regex.IsMatch(user.username, alphaNumericRegex))
            {
                errors.Add("Username must be alphanumeric");
            }
            else if (user.username.Length < 8)
            {
                errors.Add("Username must be at least 5 characters");
            }
            //password validation if adding
            if(isAdd)
            {
                if (string.IsNullOrEmpty(user.password))
                {
                    errors.Add("Password cannot be empty");
                }
                else if (string.IsNullOrEmpty(user.confirmPassword))
                {
                    errors.Add("The confirmation password cannot be empty");
                }
                else if (!user.password.Equals(user.confirmPassword))
                {
                    errors.Add("The password and the confirmation must match");
                }
                else if (user.password.Length < 8)
                {
                    errors.Add("The password must be at least 8 characters long");
                }
            }
            //first name validation
            if(string.IsNullOrEmpty(user.firstName))
            {
                errors.Add("Username cannot be empty");
            }
            //last name validation
            if(string.IsNullOrEmpty(user.lastName))
            {
                errors.Add("Last name cannot be empty");
            }
            //email validation
            if(string.IsNullOrEmpty(user.emailAddress))
            {
                errors.Add("Email cannot be empty");
            } else if(!emailAttr.IsValid(user.emailAddress))
            {
                errors.Add("Email address is not valid");
            }
            //if we have no errors return true
            if(errors.Count().Equals(0))
            {
                return true;
            }
            return false;
        }

    }
}
