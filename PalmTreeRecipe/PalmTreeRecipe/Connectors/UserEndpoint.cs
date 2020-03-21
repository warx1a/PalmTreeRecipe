using PalmTreeRecipe.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PalmTreeRecipe.Connectors
{
    public class UserEndpoint:
        DataConnector
    {

        /// <summary>
        /// Will return the session id if a valid login, null otherwise
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>session_id|null</returns>
        public string login(string username, string password)
        {
            string query = "SELECT * FROM User WHERE username = @username AND password = @password";
            using(SqlConnection conn = new SqlConnection(DB_URL))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                SqlDataReader results = cmd.ExecuteReader();
                if(results.HasRows && results.Read())
                {
                    int userId = results.GetInt32(0);
                    Guid sessionId = Guid.NewGuid();
                    if (updateSessionForUser(userId, sessionId.ToString()))
                    {
                        return sessionId.ToString();
                    }
                }
            }
            return null;
        }

        public User getUserBySessionId(string sessionId)
        {
            string query = "SELECT * FROM User WHERE sessionId = @sessionid";
            using(SqlConnection conn = new SqlConnection(DB_URL))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@sessionid", sessionId);
                SqlDataReader results = cmd.ExecuteReader();
                if(results.HasRows && results.Read())
                {
                    int userId = getValueOrDefault<int>(0, results);
                    string username = getValueOrDefault<string>(1, results);
                    string password = getValueOrDefault<string>(2, results);
                    string email = getValueOrDefault<string>(3, results);
                    string firstName = getValueOrDefault<string>(5, results);
                    string lastName = getValueOrDefault<string>(6, results);
                    int userType = getValueOrDefault<int>(7, results);
                    User u = new User(userId, username, password, email, sessionId, firstName, lastName, userType);
                    return u;
                }
            }
            return null;
        }

        /// <summary>
        /// Function to update the session id by the user id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="session_id"></param>
        /// <returns>true if successful, false otherwise</returns>
        public bool updateSessionForUser(int userId, string session_id)
        {
            string query = "UPDATE User SET sessionId = @sessionid WHERE userId = @id";
            using(SqlConnection conn = new SqlConnection(DB_URL))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@sessionid", session_id);
                cmd.Parameters.AddWithValue("@id", userId);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected.Equals(1);
            }
        }

        public User createUser(User user)
        {
            string query = "INSERT INTO User (username, password, emailAddress, sessionId, firstName, lastName, userType) VALUES " +
                "(@username, @password, @emailaddress, @sessionid, @firstname, @lastname, @usertype)";
            //the user doesn't have an id or a session id yet so we need to generate the session id
            Guid sessionid = Guid.NewGuid();
            user.sessionId = sessionid.ToString();
            using(SqlConnection conn = new SqlConnection(DB_URL))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", user.username);
                cmd.Parameters.AddWithValue("@password", user.password);
                cmd.Parameters.AddWithValue("@emailaddress", user.emailAddress);
                cmd.Parameters.AddWithValue("@sessionid", user.sessionId);
                cmd.Parameters.AddWithValue("@firstname", user.firstName);
                cmd.Parameters.AddWithValue("@lastname", user.lastName);
                cmd.ExecuteNonQuery();
            }
            return user;
        }
    }
}
