using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20251112_Library_System
{
    internal class Program
    {
        static Shelf Shelf1 = new Shelf();

        static void Main(string[] args)
        {
            BorrowedBooks borrowedBooks = new BorrowedBooks();

            /// <summary> This is the main entry point for the Library System application. </summary>
            /// 

            string username;
            int passcode;
            //int role = 0;
            bool accountFound = false;
            bool exit = false;
            int choice;
            int bookChoice = 0;
            string borrowedBook;
            int borrowCount = 0;
            int returnChoice;

            Console.WriteLine("Welcome to the Library System!");
            Console.WriteLine();

            //Console.Write("Please select your role \n1. User\n2. Admin: ");
            //while (role < 1 && role > 2)
            //{
            //    int.TryParse(Console.ReadLine(), out role);
            //}

            /// <summary> This section checks if the entered username and passcode match any existing user accounts. </summary>
            List<string> users = Users.GetUsers();

            /// Loop until a valid account is found
            while (!accountFound)
            {
                passcode = 0;

                Console.Write("Please type your username: ");
                username = Console.ReadLine();

                Console.Write("Please type your passcode: ");
                while (passcode < 1)
                {
                    int.TryParse(Console.ReadLine(), out passcode);
                }

                foreach (string user in users)
                {
                    if (username == user.Split(',')[0] && passcode.ToString() == user.Split(',')[1])
                    {
                        Console.WriteLine($"Welcome, {username}!");
                        accountFound = true;
                    }

                    //else
                    //{
                    //    Console.WriteLine(user.Split(',')[0] + user.Split(',')[1]);
                    //}
                }

                if (!accountFound)
                {
                    Console.WriteLine("Account not found. Please check your username and passcode.");
                }
            }

            /// <summary> This section provides the main menu for borrowing, viewing, and returning books. </summary>
            while (!exit)
            {
                Book Book1 = new Book();

                choice = 0;

                Console.WriteLine("Please select an option:");
                Console.WriteLine("1. Borrow Books");
                Console.WriteLine("2. View borrowed books");
                Console.WriteLine("3. Return and Exit");

                while (choice < 1 || choice > 3)
                {
                    int.TryParse(Console.ReadLine(), out choice);
                }

                /// <summary> This switch statement handles the user's menu choice for borrowing, viewing, and returning books. </summary>
                switch (choice)
                {
                    

                    /// <summary> Case 1: Borrow Books </summary>
                    case 1:

                        if (borrowCount == 0)
                        {
                            Console.WriteLine("Books available in the library:");

                            for (int i = 0; i < Shelf1.bookList.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {Shelf1.bookList[i]}");
                            }

                            Console.WriteLine();
                            Console.WriteLine("================================");
                            Console.WriteLine();

                            while (bookChoice < 1 || bookChoice > Shelf1.numOfBooks)
                            {
                                Console.Write("Enter the number of the book you want to borrow: ");
                                int.TryParse(Console.ReadLine(), out bookChoice);
                            }

                            borrowedBook = RetrieveBook(bookChoice - 1, Shelf1.bookList);
                            Book1 = new Book(borrowedBook.Trim().Split(',')[0], borrowedBook.Trim().Split(',')[1], borrowedBook.Trim().Split(',')[2]);
                            borrowCount++;

                            Console.WriteLine($"You have borrowed {Book1.Title}");
                            borrowedBooks.borrowedBookList.Add($"{Book1.Title}, {Book1.Author}, {Book1.PublicationYear}"); 
                            Console.WriteLine($"{borrowedBooks.borrowedBookList.Count} book/s borrowed.");
                            Console.WriteLine($"{Shelf1.bookList.Count} book/s available in the library.");
                        }

                        else
                        {
                            Console.WriteLine("You have already borrowed a book. You cannot borrow more than one book at a time.");
                        }

                         break;

                    /// <summary> Case 2: View borrowed books </summary>
                    case 2:
                        Console.WriteLine($"{borrowedBooks.borrowedBookList.Count} book/s borrowed.");
                        if (borrowedBooks.borrowedBookList.Count == 0)
                        {
                            Console.WriteLine("You have not borrowed any books yet.");
                        }

                        else
                        {
                            Console.WriteLine("Your borrowed book/s:");
                            foreach (string book in borrowedBooks.borrowedBookList)
                            {
                                Console.WriteLine(book);
                            }
                        }
                        break;

                    /// <summary> Case 3: Return and Exit </summary>
                    case 3:

                        if (borrowedBooks.borrowedBookList.Count == 0)
                        {
                            exit = true;
                            break;
                        }

                        else
                        {
                            Console.Write("Please return these/this borrowed book/s if you want to exit.");
                            foreach (string book in borrowedBooks.borrowedBookList)
                            {
                                Console.WriteLine(book);
                            }

                            Console.WriteLine();
                            Console.WriteLine("Return? 1. Yes\t2. No");

                            returnChoice = 0;
                            while (returnChoice < 1 || returnChoice > 2)
                            {
                                Console.Write("Enter your choice: ");
                                int.TryParse(Console.ReadLine(), out returnChoice);
                            }

                            switch (returnChoice)
                            {
                                case 1:
                                    foreach (string book in borrowedBooks.borrowedBookList)
                                    {
                                        ReturnBook(book);
                                        Console.WriteLine($"Returned: {book}");
                                        Console.WriteLine($"{borrowedBooks.borrowedBookList.Count} book/s borrowed.");
                                        Console.WriteLine($"{Shelf1.bookList.Count} book/s available in the library.");
                                        exit = true;
                                    }
                                    break;
                                case 2:
                                    Console.WriteLine("You must return the borrowed books before exiting.\n" +
                                        "Going back to main manu.");
                                    exit = false;
                                    break;
                            }
                        }
                        break;
                }
            }
            Console.WriteLine("Exiting the Library System. Goodbye!");
            Console.ReadLine();
        }

        public static string RetrieveBook(int index, List<string> bookList)
        {
            string borrowedBook = bookList[index];
            Shelf1.bookList.RemoveAt(index);
            Shelf1.numOfBooks--;
            return borrowedBook;
        }

        public static void ReturnBook(string book)
        {
            Shelf1.ReturnBook(book);
        }
    }
}
