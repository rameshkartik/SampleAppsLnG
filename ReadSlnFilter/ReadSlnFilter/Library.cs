using System.Text.RegularExpressions;

namespace ReadSlnFilter
{
    public class Library
    {
        public void ReadSlnFile()
        {
            string solutionFilePath = @"C:\\SourceCode\\Tool\\ReadSlnFilter\\ReadSlnFilter\\Hunt.EPIC.WebService.slnf";

            

            if (File.Exists(solutionFilePath))
            {
                string solutionContent = File.ReadAllText(solutionFilePath);

              var data =  Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(solutionContent);


                foreach (string project in data?.solution?.projects)
                {
                    //append the ccbuild path
                    //find your csproj
                    //load your csproj
                    //find the dependencies - project reference
                    //check the dep with data?.solution?.projects
                    Console.ReadLine();
                    //Console.WriteLine($"Project Name: {projectName}, Project ID: {projectId}");
                }
            }
            else
            {
                Console.WriteLine("Solution file not found.");
            }
        }
    }
}
