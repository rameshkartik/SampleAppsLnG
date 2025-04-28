using System.Net;
using System.Reflection;
using System.Text;
using System.Xml;

namespace ProdConfigFormatAddressLib
{
    public class FormatAddress
    {
        public FormatAddress() 
        { 
        }

        public void FetchAddressFromConfig(string selectedFilePath,string sourceServerValue)
        {
            var filePath = Assembly.GetExecutingAssembly().Location.Substring(0, Assembly.GetExecutingAssembly().Location.LastIndexOf("\\"))+"\\Converted.txt";

            if (!File.Exists(filePath))
            {
                File.Create(filePath);
                

            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(selectedFilePath);

            //var xmlNode = xmlDoc.SelectNodes("//endpoint[@name='ApplicationControlEvents']")?.Item(0);
            StringBuilder stringBuilder = new StringBuilder();

            var xmlNodes = xmlDoc.SelectNodes("//endpoint");
            foreach(XmlNode xmlNodeEndpoint in xmlNodes)
            {
                var name = xmlNodeEndpoint?.Attributes?["name"]?.Value;
                var address = string.Empty;
                if(name == null)
                {
                    address = xmlNodeEndpoint?.Attributes?["address"]?.Value;
                    ReplaceServerDetails(address, stringBuilder,"NameEmpty", sourceServerValue);
                }
                else
                {
                    var xmlNode = xmlDoc.SelectNodes("//endpoint[@name='" + name + "']")?.Item(0);
                    address = xmlNode?.Attributes?["address"]?.Value;
                    ReplaceServerDetails(address, stringBuilder, name, sourceServerValue);

                }
                
            }

            WriteToFile(stringBuilder,filePath);

        }

        private void ReplaceServerDetails(string address,StringBuilder stringBuilder,string name,string sourceServer)
        {
            if (address != null)
            {
                var convertedAddress = address.Replace(sourceServer, "INSTALLER_SBSServer_NAME");
                var port = convertedAddress.Replace("%CC_App_Server_Port%", "%SBS_App_Server_Port%");
                var instance = port.Replace("%VirtualInstance%", "${INSTANCE_NAME}");
                var InstanceData = instance.Replace("%Instance%", "${INSTANCE_NAME}");
                var FinalData = instance.Replace("net.pipe://localhost", "%INSTALLER_HTTP_PROTOCOL%://%INSTALLER_SBSServer_NAME%:%SBS_App_Server_Port%");
                stringBuilder?.Append(name);
                stringBuilder?.Append("|");
                stringBuilder?.Append(FinalData);
                stringBuilder?.Append(" ");
                stringBuilder?.Append("\n");
            }
        }

        private void WriteToFile(StringBuilder stringBuilder,string path)
        {
            File.WriteAllText(path, stringBuilder?.ToString());
            Console.WriteLine(stringBuilder?.ToString());
            Console.ReadLine();
        }

    }
}
