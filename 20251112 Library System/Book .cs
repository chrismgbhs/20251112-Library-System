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

        public Book()
        {
            Title = "";
            Author = "";
            PublicationYear = "";
        }

        public Book(string title, string author, string publicationYear)
        {
            Title = title;
            Author = author;
            PublicationYear = publicationYear;
        }
    }
}
