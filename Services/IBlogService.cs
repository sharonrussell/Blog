using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface IBlogService
    {
        [OperationContract]
        IEnumerable<BlogDto> GetBlogs();

        [OperationContract]
        void AddBlog(BlogDto blog);

        [OperationContract]
        void RemoveBlog(int blogId);
    }
}
