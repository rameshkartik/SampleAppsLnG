using System.ServiceModel;

namespace Services
{

    [ServiceContract]
    public interface IEndPointService
    {
        [OperationContract]
        string SendRequest(string url);

        [OperationContract]
        string GetResponse(string url);
    }

    public class EndPointService : IEndPointService
    {
        public string GetResponse(string url)
        {
            return "Response Returned";
        }

        public string SendRequest(string url)
        {
            return "Request Sent";
        }
    }
}
