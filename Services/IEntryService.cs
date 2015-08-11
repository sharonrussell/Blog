using System;
using System.ServiceModel;
using DataAccess.Exceptions;

namespace Services
{
    [ServiceContract]
    public interface IEntryService
    {
        [OperationContract]
        [FaultContract(typeof(ObjectDoesNotExistException))]
        void AddEntry(Guid blogId, string title, string body);

        [OperationContract]
        [FaultContract(typeof(ObjectDoesNotExistException))]
        void RemoveEntry(Guid blogId, Guid entryId);
    }
}
