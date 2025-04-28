using ProdConfigFormatAddressLib;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string[] searchKeys = { "ApplicationControlEvents", "DefaultControlEvents" };

FormatAddress formatAddress = new();
//formatAddress.FetchAddressFromConfig("C:\\SourceCode\\Samples\\ProdConfigFormatAddress\\ProdConfigFormatAddressLib\\Web.sbscollectormsg.config", "CCAPPSERVER");
//formatAddress.FetchAddressFromConfig("C:\\SourceCode\\Samples\\ProdConfigFormatAddress\\ProdConfigFormatAddressLib\\Web.sbsnmsevents.config", "CCAPPSERVER");
formatAddress.FetchAddressFromConfig("C:\\SourceCode\\Samples\\ProdConfigFormatAddress\\ProdConfigFormatAddressLib\\Web.sbsrplmsg.config", "CCAPPSERVER");


