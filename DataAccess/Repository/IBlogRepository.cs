using System.Collections.Generic;
using Domain;

namespace DataAccess.Repository
{
    public interface IBlogRepository
    {
        IList<Blog> GetBlogs();

        void AddBlog(Blog blog);
        void RemoveBlog(int blogId);
    }
}
