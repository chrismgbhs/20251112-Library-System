using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20251112_Library_System
{
    internal class Shelf
    {
        /// <summary>
        /// This is a list of books available in the library shelf.
        /// </summary>
        public static List<string> bookList = new List<string>();

        /// <summary>
        /// This variable defines the number of books to be generated.
        /// </summary>
        public static int numOfBooks = 50;

        /// <summary>
        /// This variable keeps track of the number of digits in the highest book number.
        /// </summary>
        public static int numOfDigits = 0;

        /// <summary>
        /// This method generates a list of books with authors and publication years.
        /// </summary>
        public static void GenerateBooks()
        {

            foreach (char digit in numOfBooks.ToString())
            {
                numOfDigits++;
            }

            for (int i = 1; i <= numOfBooks; i++)
            {

                if (i.ToString().Count() < numOfDigits)
                {
                    int digitDiff = numOfBooks.ToString().Count()-i.ToString().Count();
                    string titleNumber = $"{i}";

                    for (int counter = 0; counter < digitDiff; counter++)
                    {
                        titleNumber = $"{0}"+$"{titleNumber}";
                    }

                    bookList.Add($"Book Title {titleNumber}, Author {i}, Year {1975 + i}");
                }

                else
                {
                    bookList.Add($"Book Title {i}, Author {i}, Year {1975 + i}");
                }           
            }
        }

        /// <summary>
        /// This method allows a user to return a book to the shelf.
        /// </summary>
        /// <param name="book"></param>
        public static void ReturnBook(string book)
        {
            bookList.Add(book);
        }
    }
}
