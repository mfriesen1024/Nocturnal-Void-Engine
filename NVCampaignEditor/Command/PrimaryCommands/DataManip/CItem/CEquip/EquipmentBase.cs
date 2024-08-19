namespace NVCampaignEditor.Command.PrimaryCommands.DataManip.CItem.CEquip
{
    internal class EquipmentBase : CommandBase
    {
        public EquipmentBase()
        {
            Aliases = ["equipment", "equip", "e"];
            Subcommands = [new Replace(), new Create(), new Delete()];
        }

        protected override void Process(string[] argArray)
        {
            throw new ArgumentException("Invalid command.");
        }

        protected override void Help(bool chain = false)
        {
            Console.WriteLine("Equipment: tools for editing equipment");
            ListAliases();
            ListSubcommands();
        }
    }
}
