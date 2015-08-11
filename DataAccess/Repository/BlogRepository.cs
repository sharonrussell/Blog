using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.Context;
using Domain;

namespace DataAccess.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private BlogContext _context;

        private readonly IContextFactory _contextFactory;  

        public BlogRepository(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IList<Blog> GetBlogs()
        {
            using (_context = _contextFactory.CreateContext())
            {
                return _context.Blogs.Include(b => b.Entries).ToList();
            }
        }
    }
}
