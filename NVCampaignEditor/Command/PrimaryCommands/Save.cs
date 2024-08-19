using Nocturnal_Void.FileSystem;

namespace NVCampaignEditor.Command.PrimaryCommands
{
    /// <summary>
    /// Save all data.
    /// </summary>
    public class Save : CommandBase
    {
        public Save()
        {
            Aliases = ["save"];
        }

        protected override void Help(bool chain = false)
        {
            Console.WriteLine("Save: Saves all data to the specified relative path, creating files and folders as needed.\n" +
                "Usage: save <path>");
        }

        protected override void Process(string[] argArray)
        {
            // Save using the first argument.
            FileManager.Save(argArray[0]);
        }
    }
}
