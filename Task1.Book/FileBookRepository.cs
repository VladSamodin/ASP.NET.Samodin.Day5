using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Book
{
    public class FileBookRepository : IRepository<Book>
    {
        public string Path { get; private set; }

        public FileBookRepository(string filePath)
        {
            Path = filePath;
        }

        public IEnumerable<Book> GetList()
        {
            List<Book> listBooks = new List<Book>();
            if (!File.Exists(Path))
            {
                return listBooks;
            }
            using (var binaryReader = new BinaryReader(File.OpenRead(Path)))
            {
                int count = binaryReader.ReadInt32();
                
                for (int i = 0; i < count; i++)
                {
                    string author = binaryReader.ReadString();
                    string title = binaryReader.ReadString();
                    int pageCount = binaryReader.ReadInt32();
                    int yearPublishing = binaryReader.ReadInt32();
                    string genre = binaryReader.ReadString();
                    Book book = new Book(author, title, pageCount, yearPublishing, genre);
                    listBooks.Add(book);
                }
            }
            return listBooks;
        }

        public void SaveList(IEnumerable<Book> list)
        {
            FileStream fs = File.Exists(Path) ? File.OpenWrite(Path) : File.Create(Path);
            using (var binaryWriter = new BinaryWriter(fs))
            {
                int count = list.Count();
                binaryWriter.Write(count);
                foreach (Book book in list)
                {
                    binaryWriter.Write(book.Author);
                    binaryWriter.Write(book.Title);
                    binaryWriter.Write(book.PageCount);
                    binaryWriter.Write(book.YearPublishing);
                    binaryWriter.Write(book.Genre);
                }
            }
        }
    }
}
