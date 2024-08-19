using NVCampaignEditor.Command.PrimaryCommands.DataManip.CItem.CConsumable;
using NVCampaignEditor.Command.PrimaryCommands.DataManip.CItem.CEquip;
using NVCampaignEditor.Command.PrimaryCommands.DataManip.CItem.CGold;

namespace NVCampaignEditor.Command.PrimaryCommands.DataManip.CItem
{
    public class ItemMain : CommandBase
    {
        public ItemMain()
        {
            Aliases = ["item", "items", "i"];
            Subcommands = [new ConsumableBase(), new EquipmentBase(), new GoldBase(), new ListAll()];
        }

        protected override void Process(string[] argArray)
        {
            throw new NotImplementedException();
        }
        protected override void Help(bool chain = false)
        {
            Console.WriteLine("Item: tools for editing items");
            Console.WriteLine("Usage: item <subcommand>");
            ListAliases();
            if (!chain)
            {
                ListSubcommands();
            }
        }
    }
}
