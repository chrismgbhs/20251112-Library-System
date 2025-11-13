using System;
using System.Collections.Generic;
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
        public List<string> bookList = new List<string>();
        public int numOfBooks = 50;

        /// <summary>
        /// This method generates a list of books with authors and publication years.
        /// </summary>
        public Shelf()
        {
            for (int i = 1; i <= numOfBooks; i++)
            {
                bookList.Add($"Book Title {i}, Author {i}, Year {1975 + i}");
            }
        }

        public void ReturnBook(string book)
        {
            bookList.Add(book);
        }
    }
}
