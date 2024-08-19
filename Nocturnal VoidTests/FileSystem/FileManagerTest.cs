namespace Nocturnal_Void.FileSystem.Tests
{
    [TestClass()]
    public class FileManagerTest
    {
        public void LoadTest()
        {
            FileManager.Load("TestFolder");
        }

        public void SaveTest()
        {
            FileManager.Init();
            FileManager.Save("TestFolder");
        }

        [TestMethod()]
        public void PrimaryLoadTest()
        {
            SaveTest();
            LoadTest();
        }
    }
}