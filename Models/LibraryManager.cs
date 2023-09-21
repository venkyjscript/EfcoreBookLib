using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.Models
{
    public class LibraryManager:IBookRepo<Book>
    {


       
        readonly BookContext _dbContext;
        public LibraryManager(BookContext context)
        {
            _dbContext = context;
        }

        public IEnumerable<Book> GetAll()
        {
            return  _dbContext.Books.ToList();
        }



        public object Add(Book Book)
        {
            _dbContext.Books.Add(Book);
            _dbContext.SaveChanges();
            return Book.BookId;
        }


        public object Get(int Id)
        {
            if (_dbContext.Books.Any(o => o.BookId == Id))
            {
                return new ApiResponse() { message = "Success", Data = _dbContext.Books.FirstOrDefault(e => e.BookId == Id) };

            }

            return new ApiResponse() { message = "Failure, no such book", Data = null };
        }



        public object Update(Book book, Book Update)
        {
            return new Book();
        }

        public object UpdateById(int id,Book book)
        {
            _dbContext.Update(book);
            _dbContext.SaveChanges();
            return new ApiResponse() { message = "Success", Data = book };
        }



        public object Delete(int id)
        {
            var item=_dbContext.Books.Where(p => p.BookId == id).Select(p => p).FirstOrDefault();
            _dbContext.Remove(item);
            _dbContext.SaveChanges();
            return item;
        }

		public Guid GetOperationID()
		{
			throw new NotImplementedException();
		}
	}


}
