namespace NVCampaignEditor.Command.PrimaryCommands.DataManip.CFoe
{
    internal class FoeMain : CommandBase
    {
        public FoeMain()
        {
            Aliases = ["foe", "foes"];
            Subcommands = [new Create(), new Delete(), new List(), new Replace()];
        }

        protected override void Help(bool chain = false)
        {
            Console.WriteLine("Foe: tools for foes.\n" +
                "Usage: foe <subcommand>");
            // If this is not a chain execution, list subcommands.
            if (!chain) { ListSubcommands(); }
        }

        protected override void Process(string[] argArray)
        {
            throw new ArgumentException("Invalid command.");
        }
    }
}
