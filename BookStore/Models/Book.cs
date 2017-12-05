using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Book
    {
        // ID книги
        public int Id { get; set; }
        // название книги
        public string Name { get; set; }
        // автор книги
        public string Author { get; set; }
        // цена
        public int Price { get; set; }
    }

    public class BookEx
    {
        public BookEx()
        {

        }

        public BookEx(Book book)
        {
            Id = book.Id;
            Name = book.Name;
            Author = book.Author;
            Price = book.Price;
            Key = book.Id.ToString();
        }

        public string Key { get; set; }

        public int Price { get; set; }

        public string Author { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}