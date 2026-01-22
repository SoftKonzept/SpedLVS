namespace LvsScan.Portable.Interfaces
{
    public interface IFileService
    {
        void CreateFile(string fileText, string fileName, string filePath);
        //void CreateFile1(string fileText, enumFileStorageArt fileStorageArt);
        void CreateFile2(string fileText);
    }
}
