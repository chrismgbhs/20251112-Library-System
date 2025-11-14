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
            BorrowedBooks[] borrowedBooks = new BorrowedBooks[Users.userList.Count];
            Book[] book = new Book[Users.userList.Count];

            for (int counter = 0; counter < Users.userList.Count; counter++)
            {
                borrowedBooks[counter] = new BorrowedBooks();
                book[counter] = new Book();
            }

            /// <summary> This is the main entry point for the Library System application. </summary>
            /// 

            string username;
            int passcode;
            //int role = 0;
            bool accountFound;
            bool exit;
            int choice;
            int bookChoice;
            int bookReturnChoice;
            string borrowedBook;
            int borrowCount;
            int returnChoice;
            int userNumber = 0;


            Console.WriteLine("Welcome to the Library System!");
            Console.WriteLine();

            //Console.Write("Please select your role \n1. User\n2. Admin: ");
            //while (role < 1 && role > 2)
            //{
            //    int.TryParse(Console.ReadLine(), out role);
            //}

            /// <summary> This section checks if the entered username and passcode match any existing user accounts. </summary>
            List<string> users = Users.GetUsers();

            while (true)
            {
                Console.Clear();
                accountFound = false;
                exit = false;
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
                            userNumber = Array.IndexOf(Users.GetUsers().ToArray(), user);
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
                    Console.WriteLine();
                    choice = 0;
                    borrowCount = 0;
                    bookChoice = 0;

                    Console.WriteLine("Please select an option:");
                    Console.WriteLine("1. Borrow Books");
                    Console.WriteLine("2. View Borrowed books");
                    Console.WriteLine("3. Return Book");
                    Console.WriteLine("4. Logout");
                    Console.Write("\nEnter your choice (1-4): ");

                    while (choice < 1 || choice > 4)
                    {
                        int.TryParse(Console.ReadLine(), out choice);
                    }

                    /// <summary> This switch statement handles the user's menu choice for borrowing, viewing, and returning books. </summary>
                    switch (choice)
                    {


                        /// <summary> Case 1: Borrow Books </summary>
                        case 1:

                            Console.WriteLine();
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
                                book[userNumber] = new Book(borrowedBook.Trim().Split(',')[0], borrowedBook.Trim().Split(',')[1], borrowedBook.Trim().Split(',')[2]);
                                borrowCount++;

                                Console.WriteLine();
                                Console.WriteLine($"You have borrowed {book[userNumber].Title}");
                                borrowedBooks[userNumber].borrowedBookList.Add($"{book[userNumber].Title}, {book[userNumber].Author}, {book[userNumber].PublicationYear}");
                                Console.WriteLine($"{borrowedBooks[userNumber].borrowedBookList.Count} book/s borrowed.");
                                Console.WriteLine($"{Shelf1.bookList.Count} book/s available in the library.");
                            }

                            else
                            {
                                Console.WriteLine("You have already borrowed a book. You cannot borrow more than one book at a time.");
                            }

                            break;

                        /// <summary> Case 2: View borrowed books </summary>
                        case 2:
                            Console.WriteLine();
                            Console.WriteLine($"{borrowedBooks[userNumber].borrowedBookList.Count} book/s borrowed.");
                            if (borrowedBooks[userNumber].borrowedBookList.Count == 0)
                            {
                                Console.WriteLine("You have not borrowed any books yet.");
                            }

                            else
                            {
                                Console.WriteLine("Your borrowed book/s:");
                                DisplayBorrowedBooks(borrowedBooks[userNumber].borrowedBookList, userNumber);
                            }
                            break;

                        /// <summary> Case 3: Return </summary>
                        case 3:
                            Console.WriteLine();
                            if (borrowedBooks[userNumber].borrowedBookList.Count == 0)
                            {
                                Console.WriteLine("You have no borrowed books to return. Going back to main menu.");
                                exit = true;
                                break;
                            }

                            else
                            {
                                returnChoice = 0;
                                Console.WriteLine("Please select borrowed book/s to return.");
                                DisplayBorrowedBooks(borrowedBooks[userNumber].borrowedBookList, userNumber);

                                Console.WriteLine();

                                while (returnChoice < 1 || returnChoice > borrowedBooks[userNumber].borrowedBookList.Count)
                                {
                                    Console.Write("Return: ");
                                    int.TryParse(Console.ReadLine(), out returnChoice);
                                }

                                Console.WriteLine();
                                Console.WriteLine($"Returned: {borrowedBooks[userNumber].borrowedBookList[returnChoice - 1]}");
                                ReturnBook(borrowedBooks[userNumber].borrowedBookList[returnChoice - 1], userNumber, borrowedBooks[userNumber]);
                                Console.WriteLine($"{borrowedBooks[userNumber].borrowedBookList.Count} book/s borrowed.");
                                Console.WriteLine($"{Shelf1.bookList.Count} book/s available in the library.");
                                Console.WriteLine("Going back to main menu.");
                            }
                            break;

                        case 4:
                            exit = true;
                            break;
                    }
                }
                Console.WriteLine("Logging out the Library System. Goodbye!");
                Console.ReadLine();
            }

        }

        public static string RetrieveBook(int index, List<string> bookList)
        {
            string borrowedBook = bookList[index];
            Shelf1.bookList.RemoveAt(index);
            Shelf1.numOfBooks--;
            return borrowedBook;
        }

        public static void ReturnBook(string book, int userNumber, BorrowedBooks borrowedBooks)
        {

            Shelf1.ReturnBook(book);
            borrowedBooks.borrowedBookList.RemoveAt(borrowedBooks.borrowedBookList.IndexOf(book));
        }

        public static void DisplayBorrowedBooks(List<string> borrowedBooksList, int userNumber)
        {
            for (int counter = 0; counter < borrowedBooksList.Count; counter++)
            {
                Console.WriteLine($"{counter + 1}. {borrowedBooksList[counter]}");
            }
        }
    }
}
