using Nocturnal_Void.FileSystem;

namespace NVCampaignEditor.Command.PrimaryCommands
{
    /// <summary>
    /// Initializes the loader
    /// </summary>
    internal class Initialize : CommandBase
    {
        public Initialize()
        {
            Aliases = ["initialize", "init"];
        }

        protected override void Help(bool chain = false)
        {
            Console.WriteLine("Init: Initializes the editor with sample data so errors aren't thrown.");
            ListAliases();
        }

        protected override void Process(string[] argArray)
        {
            FileManager.Init();
        }
    }
}
