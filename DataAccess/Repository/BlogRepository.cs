using System;
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

        public void AddBlog(Blog blog)
        {
            if (blog == null)
            {
                throw new ArgumentNullException("blog");
            }

            using (_context = _contextFactory.CreateContext())
            {
                _context.Blogs.Add(blog);
                _context.SaveChanges();
            }
        }

        public void RemoveBlog(Guid blogId)
        {
            using (_context = _contextFactory.CreateContext())
            {
                Blog blog = _context.Blogs.SingleOrDefault(b => b.BlogId == blogId);

                _context.Blogs.Remove(blog);
                _context.SaveChanges();
            }
        }
    }
}
