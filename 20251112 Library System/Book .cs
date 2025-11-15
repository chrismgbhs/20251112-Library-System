using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20251112_Library_System
{
    internal class Book
    {
        public string Title;
        public string Author;
        public string PublicationYear;

        /// <summary>
        /// This is the default constructor for the Book class.
        /// </summary>
        public Book()
        {
            Title = "";
            Author = "";
            PublicationYear = "";
        }

        /// <summary>
        /// This is the parameterized constructor for the Book class.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="author"></param>
        /// <param name="publicationYear"></param>
        public Book(string title, string author, string publicationYear)
        {
            Title = title;
            Author = author;
            PublicationYear = publicationYear;
        }
    }
}
