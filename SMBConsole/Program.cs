using SMBLibrary;
using SMBLibrary.Client;
using SMBLibrary.SMB2;
using System.Net.NetworkInformation;


SMBWriteLibrary.Library library = new SMBWriteLibrary.Library();


SMB2Client client = new SMB2Client();

var status = library.CreateNetworkConnection("10.104.115.130", client, "smbtest", "Landis123");

if (status == NTStatus.STATUS_SUCCESS) //Login Success
{
    var writeStatus = library.CreateFileAndWrite(status, client, "DATAEXTRACT", "incr-itvl-20241224T000004-00032.txt");

    if (writeStatus == NTStatus.STATUS_SUCCESS) { Console.WriteLine("File Created"); }
}


//library.CreateConnection();
//Console.WriteLine("Connection Created");

Console.ReadLine();