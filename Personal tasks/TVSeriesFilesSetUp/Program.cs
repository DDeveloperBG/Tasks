namespace TVSeriesFilesSetUp
{
    using System.Text.RegularExpressions;

    public class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter folder path:");
            string folderPath = Console.ReadLine();

            SetUpSeriesEpisodes(folderPath);
        }

        static void SetUpSeriesEpisodes(string folderPath, string fileType = "mkv")
        {
            string[] filePaths = Directory.GetFiles(folderPath, "*." + fileType);
            foreach (var filePath in filePaths)
            {
                string fileName = ExtractFileName(filePath, fileType);
                string episodeText = ExtractSeasonAndEpisodeText(fileName);

                string newName = filePath.Replace(fileName, episodeText);

                // In one or different folders
                // If you want it in different set it false
                bool inOneFolder = true;
                if (inOneFolder)
                {
                    newName = GetNewNameForSeasonFolder(filePath, newName);

                    CreateFolder(newName);
                }

                File.Move(filePath, newName);
            }
        }

        static string ExtractSeasonAndEpisodeText(string fileName)
        {
            Regex seasonAndEpisodeTextReg = new Regex(@"S\d+.*E\d+");

            return seasonAndEpisodeTextReg.Match(fileName).Value.Replace(".", "");
        }

        static string ExtractFileName(string file, string fileType)
        {
            int fileNameStartInd = file.LastIndexOf("\\") + 1;
            int fileNameEndInd = file.IndexOf("." + fileType);

            return file[fileNameStartInd..fileNameEndInd];
        }

        static string GetNewNameForSeasonFolder(string file, string newName)
        {
            Regex seasonReg = new Regex(@"S\d+");

            string season = seasonReg.Match(newName).Value;

            int fileNameStartInd = file.LastIndexOf("\\") + 1;

            return newName.Insert(fileNameStartInd, season + "\\");
        }

        static void CreateFolder(string newName)
        {
            int folderNameEndIndex = newName.LastIndexOf("\\");
            string newFolderPath = newName[..folderNameEndIndex];

            if (!Directory.Exists(newFolderPath))
            {
                Directory.CreateDirectory(newFolderPath);
            }
        }
    }
}