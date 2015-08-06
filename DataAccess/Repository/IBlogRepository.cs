using System;
using System.ServiceModel;

namespace DataAccess.Repository
{
    [ServiceContract]
    public interface IBlogRepository
    {
        [OperationContract]
        void AddEntry(Guid blogId, string title, string body);
    }
}
