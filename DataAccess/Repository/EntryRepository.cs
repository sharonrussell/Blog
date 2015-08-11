using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.Context;
using DataAccess.Exceptions;
using Domain;

namespace DataAccess.Repository
{
    public class EntryRepository : IEntryRepository
    {
        private readonly IContextFactory _contextFactory;

        private BlogContext _context;

        public EntryRepository(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddEntry(Guid blogId, string title, string body)
        {
            if (String.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException("title");
            }

            if (String.IsNullOrWhiteSpace(body))
            {
                throw new ArgumentNullException("body");
            }

            using (_context = _contextFactory.CreateContext())
            {
                Blog blog = _context.Blogs.SingleOrDefault(b => b.BlogId == blogId);

                if (blog == null)
                {
                    throw new ObjectDoesNotExistException("blog");
                }

                Entry entry = new Entry(title, body);

                blog.AddEntry(entry);

                _context.Entries.Add(entry);
                _context.SaveChanges();
            }
        }

        public void RemoveEntry(Guid blogId, Guid entryId)
        {
            using (_context = _contextFactory.CreateContext())
            {
                Blog blog = _context.Blogs.Include(o => o.Entries).SingleOrDefault(b => b.BlogId == blogId);

                if (blog == null)
                {
                    throw new ObjectDoesNotExistException("blog");
                }

                Entry entry = blog.Entries.SingleOrDefault(e => e.EntryId == entryId);

                if (entry == null)
                {
                    throw new ObjectDoesNotExistException("entry");
                }

                blog.RemoveEntry(entry.EntryId);

                _context.Entries.Remove(entry);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Entry> GetEntries(Guid blogId)
        {
            using (_context = _contextFactory.CreateContext())
            {
                return _context.Entries.Where(e => e.BlogId == blogId).ToList();
            }
        } 
    }
}
