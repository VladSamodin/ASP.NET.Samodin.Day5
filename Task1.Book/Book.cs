using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Book
{
    public class Book : IEquatable<Book>, IComparable<Book>
    {
        private readonly string author;
        private readonly string title;
        public string Author { get { return author; } }
        public string Title { get { return title; } }
        public int PageCount { get; set; }
        public int YearPublishing { get; set; }
        public string Genre { get; set; }

        public Book(string author, string title, int pageCount, int yearPublishing, string genre)
        {
            this.author = author;
            this.title = title;
            PageCount = pageCount;
            YearPublishing = yearPublishing;
            Genre = genre;
        }

        public bool Equals(Book other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (object.ReferenceEquals(this, other))
            {
                return true;
            }
            if (this.Author == other.Author && this.Title == other.Title)
            {
                return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
 	        return Equals(obj as Book);
        }

        public int CompareTo(Book other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return -1;
            }
            return String.Compare(this.Title, other.Title);
        }

        public override int GetHashCode()
        {
            return Author.GetHashCode() + Title.GetHashCode();
        }
    }
}
