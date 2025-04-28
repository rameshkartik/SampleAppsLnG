using LandisGyr.Scheduler.ClientApiContracts;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;
using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;
using LandisGyr.CommandCenter.Common;
using System;
using System.Collections.Generic;
 using System.IO;
 using System.Linq;
 using System.Net.Http;
 using System.Threading;
using System.Threading.Tasks;
namespace SerializationLib
{
    public class Library<T>
    {

        private Stream extractOutputStream = null;
        public void CreatePath()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                //string fileName = "/TST44-WIN-WEB/DATAEXTRACT/Batch_20250319_14_59-20250319_15_04_1.tmp.Gas";
                //extractOutputStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
                //Console.WriteLine("File Created in Linux");

                string fileName = "\\\\TST44-WIN-WEB\\DATAEXTRACT\\Batch_20250319_14_59-20250319_15_04_1.tmp.Gas";
                extractOutputStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
                Console.WriteLine("File Created in Linux");
            }
        }

      

        public void PerformSerialize()
        {
            EmployeeModel obj = CreateEmpObject();

            var serializedstring = DataContractSerializeObject(obj);

            EmployeeModel em = (EmployeeModel)DeserializeEmpModel<EmployeeModel>(serializedstring);

            Console.WriteLine(em.EmployeeName);

        }

        


        public static string DataContractSerializeObject<T>(T objectToSerialize)
        {
            using (var output = new StringWriter())
            {
                using (var writer = new XmlTextWriter(output) { Formatting = Formatting.Indented })
                {
                    DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(EmployeeModel));

                    dataContractSerializer.WriteObject(writer, objectToSerialize);

                    return output.GetStringBuilder().ToString();
                }
            }
        }

     

        public object DeserializeEmpModel<T>(string serializedstring)
        {
            using (Stream stream = new MemoryStream())
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(serializedstring);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(BaseEmployeeModel));
                EmployeeModel o = (BaseEmployeeModel)(dataContractSerializer?.ReadObject(stream));
                return o;
            }
        }

        public EmployeeModel CreateEmpObject()
        {
            EmployeeModel model = new EmployeeModel();
            model.EmployeeName = "Ram";
            model.EmployeeID2 = 1;
            model.AddressObject = new Address();
            model.AddressObject.City = "Delhi";
            model.AddressObject.Region = "North";
            return model;
        }















        //public static T DataContractDeSerializeObject<T>(string xml)
        //{
        //    using (Stream stream = new MemoryStream())
        //    {
        //        byte[] data = System.Text.Encoding.UTF8.GetBytes(xml);
        //        stream.Write(data, 0, data.Length);
        //        stream.Position = 0;
        //        DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(EmployeeModel));
        //        return (T)dataContractSerializer.ReadObject(stream);
        //    }

        //}
    }

    public class ReqLib
    {
        const string URI_RESOURCE_JOBS = @"api/scheduler/jobs";
        const string URI_RESOURCE_JOBS_FORMAT_STRING = "api/scheduler/jobs/{0}";
        private IDeserializer deserializer = null;
        private ISerializer serializer = null;

        public ReqLib()
        {
            deserializer = NewtonSoftJsonRestSharpSerializer.Default;
            serializer = (ISerializer)deserializer;
        }
        public async Task<TaskSchedulingOperationResultTO> SendReq()
        {
            SchedulerMessageSpecificationTO request = new SchedulerMessageSpecificationTO();
            string jobId = "1773";
            IRestClient client = CreateRestClient("http://localhost:50200/");
            IRestRequest apiReq = CreateRestRequest(string.Format(URI_RESOURCE_JOBS_FORMAT_STRING, jobId), Method.POST);
            apiReq.AddJsonBody(request);
            var response = await Execute<TaskSchedulingOperationResultTO>(client, apiReq);
            return response.Data;
        }

        private IRestClient CreateRestClient(string baseUrl)
        {
            var client = new RestClient(baseUrl);
            client.AddHandler("application/json", RestShaprDeserializer.deserializer);

            return client;
        }

        private IRestRequest CreateRestRequest(string url, RestSharp.Method method)
        {
            var request = new RestRequest(url, method);
            request.JsonSerializer = serializer;
            return request;
        }

        private async Task<IRestResponse<T>> Execute<T>(IRestClient client, IRestRequest request) where T : class, new()
        {
            return await client.ExecuteAsync<T>(request);
        }
    }

    public class RestShaprDeserializer
    {
        public static Func<IDeserializer> deserializer = GetDeserializerFactory;


        private static IDeserializer GetDeserializerFactory()
        {
            return NewtonSoftJsonRestSharpSerializer.Default;
        }

    }
}
