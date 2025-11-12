using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20251112_Library_System
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string username;
            int passcode = 0;
            int role = 0;

            Console.WriteLine("Welcome to the Library System!");
            Console.WriteLine();

            Console.Write("Please type your username: ");
            username = Console.ReadLine();

            Console.Write("Please type your passcode: ");
            while (passcode < 1)
            {
                int.TryParse(Console.ReadLine(), out passcode);
            }

            Console.Write("Please select your role \n1. User\n2. Admin: ");
            while (role < 1 && role > 2)
            {
                int.TryParse(Console.ReadLine(), out role);
            }

            switch (role)
            {
                case 1:
                    List<string> users = Users.GetUsers();

                    foreach (string user in users)
                    {
                        if (username == user.Split(',')[0] && passcode.ToString() == user.Split(',')[1])
                        {
                            Console.WriteLine($"Welcome, {username}!");
                        }
                        
                case 2:
            }
            

            Console.ReadLine();
        }
    }
}
