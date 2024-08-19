using NVCampaignEditor.Command.PrimaryCommands.DataManip.CMap.CCompiler;
using NVCampaignEditor.Command.PrimaryCommands.DataManip.CMap.CEdit;

namespace NVCampaignEditor.Command.PrimaryCommands.DataManip.CMap
{
    internal class MapMain : CommandBase
    {
        public MapMain()
        {
            Aliases = ["map"];
            Subcommands = [new Create(), new Delete(), new EditMain(), new CompileMain()];
        }

        protected override void Process(string[] argArray)
        {
            throw new ArgumentException("Invalid command.");
        }
        protected override void Help(bool chain = false)
        {
            Console.WriteLine("Map: tools for editing maps");
            Console.WriteLine("Usage: map <subcommand>");
            ListAliases();
            if (!chain)
            {
                ListSubcommands();
            }
        }
    }
}
