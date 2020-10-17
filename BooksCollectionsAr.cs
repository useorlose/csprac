using System;

namespace Practice
{
    class MyConsole
    {
        //members are private by default in C#
        internal static double getDouble(string question)
        {
            Console.WriteLine(question);
            return double.Parse(Console.ReadLine());
        }

        internal static string getString(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }

        internal static int getNumber(string question)
        {
            return int.Parse(getString(question));
        }

        internal static DateTime getDate(string question)
        {
            return DateTime.Parse(getString(question));
        }
    }
    interface IBookManager
    {
        bool AddNewBook(Book bk);
        bool DeleteBook(int id);
        bool UpdateBook(Book bk);
        Book[] GetAllBooks(string title);
    }

    class BookManager : IBookManager
    {

        private Book[] allBooks = null;

        private bool hasBook(int id)
        {
            foreach (Book bk in allBooks)
            {
                if ((bk != null) && (bk.BookID == id))
                    return true;
            }
            return false;
        }
        //Function that is invoked when the object is created
        public BookManager(int size)
        {
            allBooks = new Book[size];
        }
        public bool AddNewBook(Book bk)
        {
            bool available = hasBook(bk.BookID);
            if (available)
                throw new Exception("Book by this ID already exists");
            for (int i = 0; i < allBooks.Length; i++)
            {
                //find the first occurance of null in the array...
                if (allBooks[i] == null)
                {
                    allBooks[i] = new Book();
                    allBooks[i].BookID = bk.BookID;
                    allBooks[i].Title = bk.Title;
                    allBooks[i].Author = bk.Author;
                    allBooks[i].Price = bk.Price;
                    return true;
                }
            }
            return false;
        }

        public bool DeleteBook(int id)
        {
            for (int i = 0; i < allBooks.Length; i++)
            {
                //find the first occurance of null in the array...
                if ((allBooks[i] != null) && (allBooks[i].BookID == id))
                {
                    allBooks[i] = null;
                    return true;
                }
            }
            return false;
        }

        public Book[] GetAllBooks(string title)
        {
            //create a blank array of books of the same size as the original.
            Book[] copy = new Book[allBooks.Length];
            //iterate thro the original to find the matching book.
            int index = 0;
            foreach (Book bk in allBooks)
            {
                //if found add the book to the copy...
                if ((bk != null) && (bk.Title.Contains(title)))
                {
                    copy[index] = bk;
                    index++;
                }
            }

            //return the copy...
            return copy;
        }

        public bool UpdateBook(Book bk)
        {
            for (int i = 0; i < allBooks.Length; i++)
            {
                //find the first occurance of null in the array...
                if ((allBooks[i] != null) && (allBooks[i].BookID == bk.BookID))
                {
                    allBooks[i].Title = bk.Title;
                    allBooks[i].Author = bk.Author;
                    allBooks[i].Price = bk.Price;
                    return true;
                }
            }
            return false;
        }
    }
    class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Author { get; set; }
    }

    //Clients should not instantiate the object. they will call our factory method to get the type of the object they want...
    class BookFactoryComponent
    {
        public static IBookManager GetComponent(int size)
        {
            return new BookManager(size);
        }
    }
    class BooksCollections
    {
        static string menu = string.Empty;
        static IBookManager mgr = null;
        static void InitializeComponent()
        {
            menu = string.Format($"~~~~~~~BOOK STORE MANAGEMENT SOFTWARE~~~~~~~~~~~~~~~~~~~\nTO ADD A NEW BOOK------------->PRESS 1\nTO UPDATE A BOOK------------>PRESS 2\nTO DELETE A BOOK------------PRESS 3\nTO FIND A BOOK------------->PRESS 4\nPS:ANY OTHER KEY IS CONSIDERED AS EXIT THE APP\n");
            int size = MyConsole.getNumber("Enter the no of Books U wish to store in the BookStore");
            mgr = BookFactoryComponent.GetComponent(size);
            mgr.AddNewBook(new Book { BookID = 123, Title = "A Suitable Boy", Author = "Vikram Seth", Price = 1200 });
            mgr.AddNewBook(new Book { BookID = 124, Title = "Disclosure", Author = "Micheal Crichton", Price = 500 });
            mgr.AddNewBook(new Book { BookID = 125, Title = "The Mahabharatha", Author = "C Rajagoalachari", Price = 350 });
            mgr.AddNewBook(new Book { BookID = 126, Title = "The Discovery of India", Author = "J . Nehru", Price = 800 });

        }

        static void Main(string[] args)
        {
            InitializeComponent();
            bool @continue = true;
            do
            {
                string choice = MyConsole.getString(menu);
                @continue = processing(choice);
            } while (@continue);
        }

        private static bool processing(string choice)
        {
            switch (choice)
            {
                case "1":
                    addingBookFeature();
                    break;
                case "2":
                    updatingBookFeature();
                    break;
                case "3":
                    deletingFeature();
                    break;
                case "4":
                    readingFeature();
                    break;
                default:
                    return false;
            }
            return true;
        }

        private static void readingFeature()
        {
            string title = MyConsole.getString("Enter the title or part of the title to search");
            Book[] books = mgr.GetAllBooks(title);
            foreach (var bk in books)
            {
                if (bk != null)
                    Console.WriteLine(bk.Title);
            }
        }

        private static void deletingFeature()
        {
            int id = MyConsole.getNumber("Enter the ID of the book to remove");
            if (mgr.DeleteBook(id))
                Console.WriteLine("Book Deleted successfully");
            else
                Console.WriteLine("Could not find the book to delete");
        }

        private static void updatingBookFeature()
        {
            Book bk = new Book();
            bk.BookID = MyConsole.getNumber("Enter the ISBN no of the book U wish to update");
            bk.Title = MyConsole.getString("Enter the new title of this book");
            bk.Author = MyConsole.getString("Enter the new Author of this book");
            bk.Price = MyConsole.getDouble("Enter the new Price of this book");
            bool result = mgr.UpdateBook(bk);
            if (!result)
                Console.WriteLine($"No book by this id {bk.BookID} found to update");
            else
                Console.WriteLine($"Book by ID {bk.BookID} is updated successfully to the database");
        }

        private static void addingBookFeature()
        {
            Book bk = new Book();
            bk.BookID = MyConsole.getNumber("Enter the ISBN no of this book");
            bk.Title = MyConsole.getString("Enter the title of this book");
            bk.Author = MyConsole.getString("Enter the Author of this book");
            bk.Price = MyConsole.getDouble("Enter the Price of this book");
            try
            {
                bool result = mgr.AddNewBook(bk);
                if (!result)
                    Console.WriteLine("No more books could be added");
                else
                    Console.WriteLine($"Book by title {bk.Title} is added successfully to the database");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}