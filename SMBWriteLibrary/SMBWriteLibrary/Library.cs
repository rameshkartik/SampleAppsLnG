using SMBLibrary;
using SMBLibrary.Client;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;

namespace SMBWriteLibrary
{
    public class Library
    {

        public void CreateConnection()
        {

            SMB2Client client = new SMB2Client(); // SMB2Client can be used as well
            bool isConnected = client.Connect(IPAddress.Parse("10.104.115.130"), SMBTransportType.DirectTCPTransport);
            if (isConnected)
            {
                NTStatus nTStatus = client.Login(String.Empty, "smbtest", "Landis123");
                if (nTStatus == NTStatus.STATUS_SUCCESS)
                {
                    List<string> shares = client.ListShares(out nTStatus);
                    //client.Logoff();
                }
                //client.Disconnect();

                nTStatus = new NTStatus();
                ISMBFileStore fileStore = client.TreeConnect("DATAEXTRACT", out nTStatus);
                string filePath = "NewFileSMB12.txt";
                if (fileStore is SMB1FileStore)
                {
                    filePath = @"\\" + filePath;
                }
                object fileHandle;
                FileStatus fileStatus;
                nTStatus = fileStore.CreateFile(out fileHandle, out fileStatus, filePath, AccessMask.GENERIC_WRITE | AccessMask.SYNCHRONIZE, SMBLibrary.FileAttributes.Normal, ShareAccess.None, CreateDisposition.FILE_CREATE, CreateOptions.FILE_NON_DIRECTORY_FILE | CreateOptions.FILE_SYNCHRONOUS_IO_ALERT, null);
                if (nTStatus == NTStatus.STATUS_SUCCESS)
                {
                    int numberOfBytesWritten;
                    byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes("Hello");
                    nTStatus = fileStore.WriteFile(out numberOfBytesWritten, fileHandle, 0, data);
                    if (nTStatus != NTStatus.STATUS_SUCCESS)
                    {
                        throw new Exception("Failed to write to file");
                    }
                    nTStatus = fileStore.CloseFile(fileHandle);
                }
                nTStatus = fileStore.Disconnect();

            }


            //SMBLibrary.NTStatus nTStatus = new SMBLibrary.NTStatus();

        }

        public NTStatus CreateNetworkConnection(string ipAddress, SMB2Client client,string username,string pwd)
        {
            bool isConnected = client.Connect(IPAddress.Parse(ipAddress), SMBTransportType.DirectTCPTransport);
            if (isConnected)
            {
                NTStatus nTStatus = client.Login(String.Empty, username, pwd);
                return nTStatus;
            }
            return NTStatus.STATUS_LOGON_FAILURE;
        }

        public NTStatus CreateFileAndWrite(NTStatus nTStatus, SMB2Client client,string SharedFolderName,string filePath)
        {
            if(nTStatus == NTStatus.STATUS_SUCCESS)
            {
                ISMBFileStore fileStore = client.TreeConnect(SharedFolderName, out nTStatus);
                if (fileStore is SMB1FileStore)
                {
                    filePath = @"\\" + filePath;
                }
                object fileHandle;
                FileStatus fileStatus;
                nTStatus = fileStore.CreateFile(out fileHandle, out fileStatus, filePath, AccessMask.GENERIC_WRITE | AccessMask.SYNCHRONIZE, SMBLibrary.FileAttributes.Normal, ShareAccess.None, CreateDisposition.FILE_CREATE, CreateOptions.FILE_NON_DIRECTORY_FILE | CreateOptions.FILE_SYNCHRONOUS_IO_ALERT, null);
                if (nTStatus == NTStatus.STATUS_SUCCESS)
                {
                    int numberOfBytesWritten;
                    byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(CreateContent());
                    nTStatus = fileStore.WriteFile(out numberOfBytesWritten, fileHandle, 0, data);
                    if (nTStatus != NTStatus.STATUS_SUCCESS)
                    {
                        throw new Exception("Failed to write to file");
                    }
                    nTStatus = fileStore.CloseFile(fileHandle);
                    return nTStatus;
                }
            }
            return nTStatus;
        }

        public string CreateContent()
        {
            string filePath = "C:\\Test\\incr-itvl-20241224T000004-00030.txt";
            string content;

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    content = reader.ReadToEnd();
                }
            }
            return content;
        }

    }
}
