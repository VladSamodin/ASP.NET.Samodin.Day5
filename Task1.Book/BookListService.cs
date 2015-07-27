using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Task1.Book
{
    public class BookListService
    {
        private static ILogger logger;
        private IRepository<Book> repository;
        private List<Book> list;

        public BookListService(IRepository<Book> repository, ILogger newLogger = null)
        {
            if (BookListService.logger == null)
            {
                BookListService.logger = newLogger ?? new NLogger();
            }
            else if (newLogger != null)
            {
                BookListService.logger = newLogger;
            }

            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            this.repository = repository;
            list = repository.GetList().ToList();
        }

        public void AddBook(Book toAdd)
        {
            if (toAdd == null)
            {
                throw new ArgumentNullException("toAdd");
            }
            int index = list.FindIndex((Book book) => toAdd.Equals(book));
            if (index != -1)
            {
                logger.Error("List already contains this book");
                throw new ArgumentException("List already contains this book", "toAdd");
            }
            list.Add(toAdd);
        }

        public void RemoveBook(Book toRemove)
        {
            if (toRemove == null)
            {
                throw new ArgumentNullException("toRemove");
            }
            int index = list.FindIndex((Book book) => toRemove.Equals(book));
            if (index == -1)
            {
                logger.Error("List doesn't contain this book");
                throw new ArgumentException("List doesn't contain this book", "toRemove");
            }
            list.Remove(toRemove);
        }

        public Book FindBook(Predicate<Book> match)
        {
            if (match == null)
            {
                throw new ArgumentNullException("match");
            }
            return list.Find(match);
        }

        public void SortBooks(Comparison<Book> comparison)
        {
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            list.Sort(comparison);
        }

        public void Save()
        {
            repository.SaveList(list);
        }
    }
}
