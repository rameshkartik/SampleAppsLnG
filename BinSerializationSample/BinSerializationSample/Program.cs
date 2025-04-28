// See https://aka.ms/new-console-template for more information
using SerializationLib;
using System.Runtime.InteropServices;
 using System;
 using System.Collections.Generic;
 using System.IO;
 using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

Console.WriteLine("Hello, World!");

//SerializationLib.Library<EmployeeModel> library = new SerializationLib.Library<EmployeeModel>();
////library.CreatePath();
//library.CreatePathDirectory();
////library.PerformSerialize();

ReqLib reqLib = new ReqLib();
var tst = reqLib.SendReq();
Console.ReadLine();

