using System;
using System.Collections.Generic;
using System.ServiceModel;
using DataAccess.Exceptions;

namespace Services
{
    [ServiceContract]
    public interface IBlogService
    {
        [OperationContract]
        [FaultContract(typeof(ObjectDoesNotExistException))]
        void AddEntry(Guid blogId, string title, string body);

        [OperationContract]
        IEnumerable<BlogDto> GetBlogs();
    }
}
