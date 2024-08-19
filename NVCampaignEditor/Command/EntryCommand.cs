using NVCampaignEditor.Command.PrimaryCommands;
using NVCampaignEditor.Command.PrimaryCommands.DataManip.CFoe;
using NVCampaignEditor.Command.PrimaryCommands.DataManip.CItem;
using NVCampaignEditor.Command.PrimaryCommands.DataManip.CMap;
using NVCampaignEditor.Command.PrimaryCommands.DataManip.CPlayer;

namespace NVCampaignEditor.Command
{
    internal class EntryCommand : CommandBase
    {
        public EntryCommand()
        {
            Subcommands =
            [
                new Exit(),
                new Initialize(),
                new Load(),
                new Save(),
                new FoeMain(),
                new ItemMain(),
                new MapMain(),
                new PlayerMain()
            ];
        }

        protected override void Help(bool chain = false)
        {
            ListSubcommands();
            Console.WriteLine();
            Console.WriteLine("use help as a subcommand to get further help for each command.");
        }

        protected override void Process(string[] argArray)
        {
            throw new ArgumentException("Invalid command.");
        }
    }
}
