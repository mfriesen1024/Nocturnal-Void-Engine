namespace NVCampaignEditor.Command.PrimaryCommands.DataManip.CPlayer
{
    public class PlayerMain : CommandBase
    {
        public PlayerMain()
        {
            Aliases = ["player"];
            Subcommands = [new View(), new Replace()];
        }

        protected override void Process(string[] argArray)
        {
            throw new ArgumentException("Invalid Command");
        }
        protected override void Help(bool chain = false)
        {
            Console.WriteLine("Player: tools for editing the player");
            Console.WriteLine("Usage: player <subcommand>");
            ListAliases();
            if (!chain)
            {
                ListSubcommands();
            }
        }
    }
}
