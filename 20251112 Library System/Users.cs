using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20251112_Library_System
{
    internal class Users
    {
        private static List<string> userList = new List<string>
        {
            "John, 1234",
            "Alice, 5678",
            "Bob, 9101"
        };

        public static List<string> GetUsers()
        {
            return userList;
        }

        public static void AddUser(string name, string pin)
        {
            userList.Add($"{name}, {pin}");
        }


    }
}
