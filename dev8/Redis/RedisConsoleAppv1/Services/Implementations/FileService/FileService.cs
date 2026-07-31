
using Abstractions.IFileService;

namespace Services.Implementations.FileServiceI {
    public class FileService : IFileService
    {
        private string _filePathToDataFolder;
        public string FileToString(string fileName)
        {
            try{
                string path = _filePathToDataFolder + fileName;
                string result = File.ReadAllText(path);
                return result;
            }
            catch (Exception ex){
                Console.WriteLine($"{ex.Message}");
                return "";
            }
        }

        public FileService
        (
            string filePathToDataFolder
        )
        {
            _filePathToDataFolder = filePathToDataFolder;
        }
    }
}
