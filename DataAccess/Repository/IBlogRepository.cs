using System.Collections.Generic;
using Domain;

namespace DataAccess.Repository
{
    public interface IBlogRepository
    {
        IList<Blog> GetBlogs();
    }
}
