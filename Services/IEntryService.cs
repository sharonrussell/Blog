using System.ServiceModel;
using DataAccess.Exceptions;

namespace Services
{
    [ServiceContract]
    public interface IEntryService
    {
        [OperationContract]
        [FaultContract(typeof(ObjectDoesNotExistException))]
        void AddEntry(int blogId, string title, string body);

        [OperationContract]
        [FaultContract(typeof(ObjectDoesNotExistException))]
        void RemoveEntry(int blogId, int entryId);

        [OperationContract]
        [FaultContract(typeof(ObjectDoesNotExistException))]
        void EditEntry(EntryDto entryDto);

        [OperationContract]
        [FaultContract(typeof(ObjectDoesNotExistException))]
        EntryDto GetEntry(int entryId);
    }
}
