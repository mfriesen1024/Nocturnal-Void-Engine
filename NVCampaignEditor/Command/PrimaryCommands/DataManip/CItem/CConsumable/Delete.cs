using Nocturnal_Void.FileSystem;

namespace NVCampaignEditor.Command.PrimaryCommands.DataManip.CItem.CConsumable
{
    public class Delete : CommandBase
    {
        public Delete()
        {
            Aliases = ["delete", "del", "d"];
        }

        protected override void Process(string[] argArray)
        {
            var objs = FileManager.ItemLoader.Consumables.ToList();
            objs.RemoveAt(int.Parse(argArray[0]));
            FileManager.ItemLoader.SetConsumables(objs.ToArray());
        }
        protected override void Help(bool chain = false)
        {
            Console.WriteLine("Delete: deletes a consumable.\n" +
                "Usage: item consumable delete <index>");
            ListAliases();
        }
    }
}
