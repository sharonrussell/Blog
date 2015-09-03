using System.Collections.Generic;
using System.Linq;
using DataAccess.Repository;
using Domain;

namespace Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IEntryRepository _entryRepository;

        public BlogService(IBlogRepository blogRepository, IEntryRepository entryRepository)
        {
            _blogRepository = blogRepository;
            _entryRepository = entryRepository;
        }

        protected BlogService()
        {
        }

        public IEnumerable<BlogDto> GetBlogs()
        {
            IEnumerable<Blog> dbBlogs = _blogRepository.GetBlogs();

            List<BlogDto> blogDtos = new List<BlogDto>();

            foreach (Blog dbBlog in dbBlogs)
            {
                List<EntryDto> entryDtos = new List<EntryDto>();

                entryDtos.AddRange(_entryRepository.GetEntries(dbBlog.BlogId).Select(entry => new EntryDto
                {
                    BlogId = dbBlog.BlogId, 
                    EntryId = entry.EntryId, 
                    Body = entry.Body, 
                    Title = entry.Title,
                    Date = entry.Date
                }).OrderByDescending(e => e.Date));

                blogDtos.Add(new BlogDto
                {
                    Author = dbBlog.Author,
                    BlogId = dbBlog.BlogId,
                    Entries = entryDtos
                });
            }

            return blogDtos;
        }

        public void AddBlog(BlogDto blogDto)
        {
            Blog blog = new Blog(blogDto.Author);

            _blogRepository.AddBlog(blog);
        }

        public void RemoveBlog(int blogId)
        {
            _blogRepository.RemoveBlog(blogId);
        }
    }
}
