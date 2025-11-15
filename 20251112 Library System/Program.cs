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
            /// <summary> This section initializes the library system by generating books and setting up user accounts. </summary>
            Shelf.GenerateBooks();
            BorrowedBooks[] borrowedBooks = new BorrowedBooks[Users.userList.Count];
            Book[] book = new Book[Users.userList.Count];
            int[] borrowCount = new int[Users.userList.Count];


            /// Initialize borrowedBooks and book arrays
            for (int counter = 0; counter < Users.userList.Count; counter++)
            {
                borrowedBooks[counter] = new BorrowedBooks();
                book[counter] = new Book();
            }

            /// Variables for user authentication and menu navigation
            string username;
            int passcode;
            //int role = 0;
            bool accountFound;
            bool exit;
            int choice;
            int bookChoice;
            int bookReturnChoice;
            string borrowedBook;
            int returnChoice;
            int userNumber = 0;

            /// Welcome message
            Console.WriteLine("Welcome to the Library System!");
            Console.WriteLine();

            /// <summary> This section checks if the entered username and passcode match any existing user accounts. </summary>
            List<string> users = Users.GetUsers();

            /// Main loop for the library system
            while (true)
            {
                // Clear previous session data
                Console.Clear();

                /// Reset variables for new login attempt
                accountFound = false;
                exit = false;

                /// Loop until a valid account is found
                while (!accountFound)
                {
                    /// Reset username and passcode for each login attempt
                    passcode = 0;

                    /// Prompt user for username and passcode
                    Console.Write("Please type your username: ");
                    username = Console.ReadLine();
                    Console.Write("Please type your passcode: ");
                    while (passcode < 1)
                    {
                        int.TryParse(Console.ReadLine(), out passcode);
                    }

                    /// Check if the entered credentials match any user account
                    foreach (string user in users)
                    {
                        if (username == user.Split(',')[0] && passcode.ToString() == user.Split(',')[1])
                        {
                            // Successful login
                            Console.WriteLine($"Welcome, {username}!");

                            /// Set accountFound to true and get the user index
                            accountFound = true;
                            userNumber = Array.IndexOf(Users.GetUsers().ToArray(), user);
                        }
                    }

                    /// If no matching account is found, display an error message
                    if (!accountFound)
                    {
                        Console.WriteLine("Account not found. Please check your username and passcode.");
                    }
                }

                /// <summary> This section provides the main menu for borrowing, viewing, and returning books. </summary>
                while (!exit)
                {
                    Console.WriteLine();

                    /// Reset menu choice variables
                    choice = 0;
                    bookChoice = 0;

                    /// Display menu options
                    Console.WriteLine("Please select an option:");
                    Console.WriteLine("1. Borrow Books");
                    Console.WriteLine("2. View Borrowed books");
                    Console.WriteLine("3. Return Book");
                    Console.WriteLine("4. View Available Books");
                    Console.WriteLine("5. Logout");
                    Console.Write("\nEnter your choice (1-5): ");

                    /// Get user's menu choice
                    while (choice < 1 || choice > 5)
                    {
                        int.TryParse(Console.ReadLine(), out choice);
                    }

                    /// <summary> This switch statement handles the user's menu choice for borrowing, viewing borrowed books, 
                    /// returning books, viewing available books, and logging out. </summary>
                    switch (choice)
                    {
                        /// <summary> Case 1: Borrow Books </summary>
                        case 1:

                            Console.WriteLine();

                            /// Check if the user has already borrowed a book
                            if (borrowCount[userNumber] == 0)
                            {
                                /// Display available books
                                ViewAvailableBooks();

                                Console.WriteLine();

                                /// Prompt user to select a book to borrow
                                while (bookChoice < 1 || bookChoice > Shelf.bookList.Count)
                                {
                                    Console.Write("Enter the number of the book you want to borrow: ");
                                    int.TryParse(Console.ReadLine(), out bookChoice);
                                }

                                /// Retrieve the selected book and update user's borrowed books
                                borrowedBook = RetrieveBook(bookChoice - 1, Shelf.bookList);

                                /// Create a new Book object for the borrowed book
                                book[userNumber] = new Book(borrowedBook.Split(',')[0], borrowedBook.Split(',')[1], borrowedBook.Split(',')[2]);

                                /// Increment the borrow count for the user
                                borrowCount[userNumber]++;

                                Console.WriteLine();

                                /// Confirm the borrowed book to the user
                                Console.WriteLine($"You have borrowed {book[userNumber].Title}");

                                /// Add the borrowed book to the user's borrowed books list
                                borrowedBooks[userNumber].borrowedBookList.Add($"{book[userNumber].Title}, {book[userNumber].Author.Trim()}, " +
                                    $"{book[userNumber].PublicationYear.Trim()}");

                                /// Display the number of borrowed books and available books in the library
                                Console.WriteLine($"{borrowedBooks[userNumber].borrowedBookList.Count} book/s borrowed.");
                                Console.WriteLine($"{Shelf.bookList.Count} book/s available in the library.");
                            }

                            /// If the user has already borrowed a book, display a message
                            else
                            {
                                Console.WriteLine("You have already borrowed a book. You cannot borrow more than one book at a time.");
                            }

                            break;

                        /// <summary> Case 2: View borrowed books </summary>
                        case 2:
                            Console.WriteLine();

                            /// Display the number of borrowed books
                            Console.WriteLine($"{borrowedBooks[userNumber].borrowedBookList.Count} book/s borrowed.");

                            /// Check if the user has borrowed any books
                            if (borrowedBooks[userNumber].borrowedBookList.Count == 0)
                            {
                                /// Inform the user that no books have been borrowed
                                Console.WriteLine("You have not borrowed any books yet.");
                            }

                            /// If the user has borrowed books, display the list of borrowed books
                            else
                            {
                                Console.WriteLine("Your borrowed book/s:");
                                DisplayBorrowedBooks(borrowedBooks[userNumber].borrowedBookList, userNumber);
                            }
                            break;

                        /// <summary> Case 3: Return </summary>
                        case 3:
                            Console.WriteLine();

                            /// Check if the user has any borrowed books to return
                            if (borrowedBooks[userNumber].borrowedBookList.Count == 0)
                            {
                                /// Inform the user that there are no borrowed books to return
                                Console.WriteLine("You have no borrowed books to return. Going back to main menu.");
                                exit = true;
                                break;
                            }

                            /// If the user has borrowed books, prompt them to select a book to return
                            else
                            {
                                /// Reset return choice variable
                                returnChoice = 0;

                                /// Display the list of borrowed books
                                Console.WriteLine("Please select borrowed book/s to return.");
                                DisplayBorrowedBooks(borrowedBooks[userNumber].borrowedBookList, userNumber);

                                Console.WriteLine();

                                /// Prompt user to select a book to return
                                while (returnChoice < 1 || returnChoice > borrowedBooks[userNumber].borrowedBookList.Count)
                                {
                                    Console.Write("Return: ");
                                    int.TryParse(Console.ReadLine(), out returnChoice);
                                }

                                Console.WriteLine();

                                /// Confirm the returned book to the user
                                Console.WriteLine($"Returned: {borrowedBooks[userNumber].borrowedBookList[returnChoice - 1]}");

                                /// Process the return of the selected book
                                borrowCount[userNumber] = ReturnBook(borrowedBooks[userNumber].borrowedBookList[returnChoice - 1], userNumber, 
                                    borrowedBooks[userNumber], borrowCount[userNumber]);

                                /// Display the number of borrowed books and available books in the library
                                Console.WriteLine($"{borrowedBooks[userNumber].borrowedBookList.Count} book/s borrowed.");
                                Console.WriteLine($"{Shelf.bookList.Count} book/s available in the library.");

                                /// Indicate returning to the main menu
                                Console.WriteLine("Going back to main menu.");
                            }
                            break;

                        /// <summary> Case 4: View available books </summary>
                        case 4:
                            Console.WriteLine();

                            /// Display available books in the library
                            ViewAvailableBooks();
                            break;

                        /// <summary> Case 5: Logout </summary>
                        case 5:
                            exit = true;
                            break;
                    }
                }

                /// Logout message
                Console.WriteLine("Logging out the Library System. Goodbye!");
                Console.ReadLine();
            }

        }

        /// <summary>
        /// This is the method to view available books in the library.
        /// </summary>
        public static void ViewAvailableBooks()
        {
            Shelf.bookList.Sort();
            Console.WriteLine("Books available in the library:");

            for (int i = 0; i < Shelf.bookList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Shelf.bookList[i]}");
            }
        }

        /// <summary>
        /// This is the method to retrieve a book from the shelf based on the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="bookList"></param>
        /// <returns></returns>
        public static string RetrieveBook(int index, List<string> bookList)
        {
            string borrowedBook = bookList[index];
            Shelf.bookList.RemoveAt(index);
            Shelf.numOfBooks--;
            return borrowedBook;
        }

        /// <summary>
        /// This is the method to return a book to the shelf.
        /// </summary>
        /// <param name="book"></param>
        /// <param name="userNumber"></param>
        /// <param name="borrowedBooks"></param>
        public static int ReturnBook(string book, int userNumber, BorrowedBooks borrowedBooks, int borrowCount)
        {
            borrowCount--;
            Shelf.ReturnBook(book);
            borrowedBooks.borrowedBookList.RemoveAt(borrowedBooks.borrowedBookList.IndexOf(book));
            return borrowCount;
        }

        /// <summary>
        /// This is a method to display the list of borrowed books for a user.
        /// </summary>
        /// <param name="borrowedBooksList"></param>
        /// <param name="userNumber"></param>
        public static void DisplayBorrowedBooks(List<string> borrowedBooksList, int userNumber)
        {
            for (int counter = 0; counter < borrowedBooksList.Count; counter++)
            {
                Console.WriteLine($"{counter + 1}. {borrowedBooksList[counter]}");
            }
        }
    }
}
