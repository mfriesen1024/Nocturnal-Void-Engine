namespace NVCampaignEditor.Command.PrimaryCommands.DataManip.CItem.CConsumable
{
    internal class ConsumableBase : CommandBase
    {
        public ConsumableBase()
        {
            Aliases = ["consumable", "c"];
            Subcommands = [new Replace(), new Create(), new Delete()];
        }

        protected override void Help(bool chain = false)
        {
            Console.WriteLine("Consumable: tools for editing consumables");
            ListAliases();
            ListSubcommands();
        }

        protected override void Process(string[] argArray)
        {
            throw new ArgumentException("Invalid command.");
        }
    }
}
