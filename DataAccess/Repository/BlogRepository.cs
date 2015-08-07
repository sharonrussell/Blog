using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.Context;
using DataAccess.Exceptions;
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

        public void AddEntry(Guid blogId, string title, string body)
        {
            if (title == null)
            {
                throw new ArgumentNullException("title");
            }

            if (body == null)
            {
                throw new ArgumentNullException("body");
            }

            using (_context = _contextFactory.CreateContext())
            {
                Entry entry = new Entry(title, body);

                Blog blog = GetBlog(blogId);
                
                blog.AddEntry(entry);

                _context.Entries.Add(entry);
                _context.SaveChanges();
            }
        }

        public IList<Blog> GetBlogs()
        {
            using (_context = _contextFactory.CreateContext())
            {
                return _context.Blogs.Include(b => b.Entries).ToList();
            }
        }

        private Blog GetBlog(Guid blogId)
        {
            Blog blog = _context.Blogs.SingleOrDefault(b => b.BlogId == blogId);

            if (blog == null)
            {
                throw new ObjectDoesNotExistException();
            }

            return blog;
        }
    }
}
