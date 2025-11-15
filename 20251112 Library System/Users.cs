using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20251112_Library_System
{
    internal class Users
    {
        /// <summary>
        /// This is a list of users with their corresponding PINs.
        /// </summary>
        public static List<string> userList = new List<string>
        {
            "John,1234",
            "Alice,5678",
            "Bob,9101",
            "Eve,1121",
            "Charlie,3141"
        };

        public static List<string> GetUsers()
        {
            return userList;
        }
    }
}
