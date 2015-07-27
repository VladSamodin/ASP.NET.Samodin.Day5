using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Book;

namespace Task1.Book.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            FileBookRepository fileBookRepository = new FileBookRepository("file");
            BookListService bls = new BookListService(fileBookRepository);
            Console.WriteLine("Исходный список книг:");
            PrintListBooks(fileBookRepository.GetList().ToList());
            Book book = new Book("Льюис Кэрролл", "Алиса в Стране чудес", 237, 1865, "Сказка");

            Console.WriteLine("Пытаемся добавить книгу");
            PrintBook(book);
            try
            {
                bls.AddBook(book);
                Console.WriteLine("Книга успешно добавлена");
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: {0}\n", e.Message);
            }

            Console.WriteLine("Пытаемся удалить книгу");
            try
            {
                bls.RemoveBook(book);
                Console.WriteLine("Книга успешно удалена");
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: {0}\n", e.Message);
            }
            Console.WriteLine();

            Console.WriteLine("Сохраняем список в файл и читаем из него");
            bls.Save();
            PrintListBooks(fileBookRepository.GetList().ToList());

            Console.WriteLine("Снова пытаемся удалить книгу");
            try
            {
                bls.RemoveBook(book);
                Console.WriteLine("Книга успешно удалена");
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: {0}\n", e.Message);
            }

            Console.WriteLine("Пытаемся найти удаленную книгу по названию \"Алиса в Стране чудес\"");
            Book seaarchResult = bls.FindBook(b => b.Title == "Алиса в Стране чудес");
            if (seaarchResult == null)
            {
                Console.WriteLine("Книга не найдена");
            }
            else
            {
                Console.WriteLine("Книга найдена");
                PrintBook(seaarchResult);
            }
            Console.WriteLine();

            Console.WriteLine("Добавляем удаленную книгу и свнова пытаеся ее найти");
            bls.AddBook(book);
            seaarchResult = bls.FindBook(b => b.Title == "Алиса в Стране чудес");
            if (seaarchResult == null)
            {
                Console.WriteLine("Книга не найдена");
            }
            else
            {
                Console.WriteLine("Книга найдена\n");
                PrintBook(seaarchResult);
            }
            bls.Save();
            Console.WriteLine();
            Console.WriteLine("Полученный список");
            PrintListBooks(fileBookRepository.GetList().ToList());

            Console.WriteLine("Сортировка книг по автору");
            bls.SortBooks((a, b) => string.Compare(a.Author, b.Author));
            bls.Save();
            PrintListBooks(fileBookRepository.GetList().ToList());

            Console.WriteLine("Сортировка книг по названию");
            bls.SortBooks((a, b) => string.Compare(a.Title, b.Title));
            bls.Save();
            PrintListBooks(fileBookRepository.GetList().ToList());
            MakeTestFile("file");
            Console.ReadKey();
        }

        private static void MakeTestFile(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
            BookListService bls = new BookListService(new FileBookRepository(path));
            Book toAdd = new Book("Антуан де Сент-Экзюпери", "Маленький принц", 54, 1943, "Сказка");
            bls.AddBook(toAdd);
            toAdd = new Book("Макс Фрай", "Лабиринт Менина", 316, 2003, "Фэнтези");
            bls.AddBook(toAdd);
            toAdd = new Book("Льюис Кэрролл", "Алиса в Стране чудес", 237, 1865, "Сказка");
            bls.AddBook(toAdd);
            toAdd = new Book("Виталий Зыков", "Безымянный раб", 609, 2004, "Фэнтези");
            bls.AddBook(toAdd);
            bls.Save();
        }

        private static void PrintListBooks(List<Book> listBooks)
        {
            for (int i = 0; i < listBooks.Count; i++)
            {
                Console.WriteLine("Книга {0}", i + 1);
                PrintBook(listBooks[i]);
            }
        }

        private static void PrintBook(Book book)
        {
            Console.WriteLine(" Автор: {0} \n Название: {1} \n Количество страниц: {2} \n Год публикации: {3} \n Жанр: {4} \n",
                    book.Author, book.Title, book.PageCount, book.YearPublishing, book.Genre);
        }
    }
}
