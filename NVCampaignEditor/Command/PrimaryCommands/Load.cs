using Nocturnal_Void.FileSystem;

namespace NVCampaignEditor.Command.PrimaryCommands
{
    /// <summary>
    /// Load data from files.
    /// </summary>
    public class Load : CommandBase
    {
        public Load()
        {
            Aliases = ["load"];
        }

        protected override void Help(bool chain = false)
        {
            Console.WriteLine("Load: Loads data from the relative path specified.\n" +
                "Usage: load <path>");
        }

        protected override void Process(string[] argArray)
        {
            // Load using the first argument.
            FileManager.Load(argArray[0]);
        }
    }
}
