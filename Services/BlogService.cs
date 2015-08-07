using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using DataAccess.Exceptions;
using DataAccess.Repository;
using Domain;

namespace Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        protected BlogService()
        {
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

            try
            {
                _blogRepository.AddEntry(blogId, title, body);
            }
            catch (ObjectDoesNotExistException ex)
            {
                throw new FaultException<ObjectDoesNotExistException>(ex);
            }
        }

        public IEnumerable<BlogDto> GetBlogs()
        {
            List<BlogDto> blogs = new List<BlogDto>();
            List<EntryDto> entries = new List<EntryDto>();
            IEnumerable<Blog> dbBlogs = _blogRepository.GetBlogs();

            foreach (Blog dbBlog in dbBlogs)
            {
                BlogDto blogDto = new BlogDto
                {
                    Author = dbBlog.Author,
                    BlogId = dbBlog.BlogId
                };

                entries.AddRange(dbBlog.Entries.Select(entry => new EntryDto
                {
                    BlogId = entry.BlogId, 
                    EntryId = entry.EntryId, 
                    Body = entry.Body, 
                    Title = entry.Title
                }));

                blogDto.Entries = entries;
                blogs.Add(blogDto);
            }

            return blogs;
        }
    }
}
