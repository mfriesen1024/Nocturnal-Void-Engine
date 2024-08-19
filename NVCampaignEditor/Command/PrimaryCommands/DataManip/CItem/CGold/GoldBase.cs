namespace NVCampaignEditor.Command.PrimaryCommands.DataManip.CItem.CGold
{
    public class GoldBase : CommandBase
    {
        public GoldBase()
        {
            Aliases = ["gold"];
            Subcommands = [new Replace(), new Create(), new Delete()];
        }

        protected override void Process(string[] argArray)
        {
            throw new ArgumentException("Invalid command.");
        }

        protected override void Help(bool chain = false)
        {
            Console.WriteLine("Gold: tools for editing gold");
            ListAliases();
            ListSubcommands();
        }
    }
}
