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
        [FaultContract(typeof (ObjectDoesNotExistException))]
        void RemoveEntry(Guid blogId, Guid entryId);

        [OperationContract]
        IEnumerable<BlogDto> GetBlogs();
    }
}
