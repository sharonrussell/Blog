using System.Collections.Generic;
using System.ServiceModel;
using Domain;

namespace Services
{
    [ServiceContract]
    public interface IBlogService
    {
        [OperationContract]
        IEnumerable<BlogDto> GetBlogs();

        [OperationContract]
        void AddBlog(BlogDto blog);
    }
}
