using System;
using System.Collections.Generic;
using Domain;

namespace DataAccess.Repository
{
    public interface IBlogRepository
    {
        void AddEntry(Guid blogId, string title, string body);

        void RemoveEntry(Guid blogId, Guid entryId);

        IList<Blog> GetBlogs();
    }
}
