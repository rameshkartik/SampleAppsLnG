// See https://aka.ms/new-console-template for more information
using Config2AppSettings;
string path = @"C:\SourceCode\Phase3\Git\RFMesh\LandisGyr.NMS.SBS.InboundMessageProcessor.Host\Web.config";
string appSettingsJsonpath = @"C:\appsettings.new.json";
Console.WriteLine("Hello, World!");

ConverterLibrary converterLibrary = new ConverterLibrary(path, appSettingsJsonpath);

converterLibrary.Convert2AppSettings();

Console.WriteLine();

