//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Reflection;

[assembly: System.Reflection.AssemblyCompanyAttribute("odey abdalrahman")]
[assembly: System.Reflection.AssemblyConfigurationAttribute("Release")]
[assembly: System.Reflection.AssemblyDescriptionAttribute(@"
      This Nuget for facilitated  upload files (Image, PDF, Doc, Audio, Json) in an external domain Base on .Net 5.
      1. Write this code in Startup >>  ConfigureServices : 
             services.AddSingleton<IUpLoadFile, UpLoadFile>(); 
       2. Write this code in appsettings.json : 
            ""UploadFileSettings"": {  
              ""Domain"": ""yourdomain.com"",  
              ""Root"": ""root//www//files, 
               ""Separator"":""//"", 
               ""MaximumSize"": 5 
           } 
           ""Domain"": ""your domain name"".  
           ""Root"": ""your server root"" . 
           ""Separator"":""{for Windows //}, {for Linux \\}"".
           ""MaximumSize"":  number by MB.
 
        3. Inject IUpLoad in constructor: 
            private readonly IUpLoadFile UpLoadFile; 
            public testController(IUpLoadFile iUpLoadFile)  { UpLoadFile = upLoadFile; } 
        4. Use functions 
             I- UpLoadFile.UpLoad(); 
            II- UpLoadFile.UpLoad2(); 
           III- UpLoadFile.Delete();")]
[assembly: System.Reflection.AssemblyFileVersionAttribute("1.1.0.0")]
[assembly: System.Reflection.AssemblyInformationalVersionAttribute("1.1.0")]
[assembly: System.Reflection.AssemblyProductAttribute("UploadFiles.On.External.Domain")]
[assembly: System.Reflection.AssemblyTitleAttribute("UploadFiles.On.External.Domain")]
[assembly: System.Reflection.AssemblyVersionAttribute("1.1.0.0")]

// Generated by the MSBuild WriteCodeFragment class.

