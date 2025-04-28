// See https://aka.ms/new-console-template for more information

using System.Runtime.InteropServices;

var basePath = "/Users/david";
var filename = "myfile.txt";
var finalpath = string.Empty;

var fullPath = Path.Combine(basePath, filename);

//var OSLinux =  RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
//string unixPath = "/usr/local/bin/example.txt";
//string windowsPath = Path.Combine(unixPath.Split('/'));
//Console.WriteLine(OSLinux);
//Console.WriteLine(windowsPath);


var OSDetail = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
if( OSDetail == true)
{
    //string ts2Path = "\\\\TST44-WIN-WEB\\DATAEXTRACT/temp/Batch_20250309_23_35-20250309_23_40_1.tmp.Water_Register";
    //string ts2Path = "D:\\LandisGyr\\Data\\Incremental_Extracts/temp/Batch_20250309_23_30-20250309_23_35_1.tmp";

    //string ts2Path = "\\\\TST44-WIN-WEB\\Scheduled_Extracts/temp/incr-itvl-20250227T015002-00010";
    // string ts2Path = "\\\\TST44-WIN-WEB\\Scheduled_Extracts/temp\\incr-itvl-20250227T015002-00019";
    //string ts2Path = "/app/smb\\Interval_Extracts\\intervals.txt";
    //string ts2Path = "\\app\\smb\\Interval_Extracts\\";
    string ts2Path = "\\app\\smb/incr-per_20250423T074324_Gas.txt";

    string res = Path.Combine(ts2Path.Split('\\'));
    if(ts2Path.IndexOf("\\") == 0)
    {
        res = "/" + res;
    }

    var aa = string.Format("{0}{1}{2}", res, "/", "temp");

    Console.WriteLine("Windows Path : {0}", ts2Path);
    Console.WriteLine("OS Running : {0}", OSDetail);
    Console.WriteLine("Linux Path : {0}", res);
}










// Windows Result: C:\base\path\myfile.txt
// Mac/Linux Result: /Users/david/myfile.txt


//var ts2filePath = "/TST44-WIN-WEB/DATAEXTRACT/temp/Batch_20250309_23_35-20250309_23_40_1.tmp.Water_Register";

//var lastIndex = ts2filePath.LastIndexOf("/");
//lastIndex = lastIndex + 1;

//if (lastIndex >= 0)
//{
//    var removedString = ts2filePath.Substring(lastIndex, ts2filePath.Length - lastIndex);
//    ts2filePath = ts2filePath.Substring(0, lastIndex);
//    finalpath =Path.Combine(ts2filePath, removedString);
//}

//Console.WriteLine("Hello, World!");
//Console.WriteLine(fullPath);
//Console.WriteLine(finalpath);
Console.ReadLine();
