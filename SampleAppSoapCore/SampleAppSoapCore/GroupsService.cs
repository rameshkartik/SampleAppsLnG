using System.ServiceModel;

namespace Services
{

    [ServiceContract]
    public interface IGroupService
    {
        [OperationContract]
        string SetGroup(string url);
    }

    public class GroupService : IGroupService
    {
        public string SetGroup(string url)
        {
            return "Group assigned";
        }
    }
}
