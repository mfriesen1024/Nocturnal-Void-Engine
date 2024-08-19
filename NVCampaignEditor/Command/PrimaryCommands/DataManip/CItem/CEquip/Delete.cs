using Nocturnal_Void.FileSystem;

namespace NVCampaignEditor.Command.PrimaryCommands.DataManip.CItem.CEquip
{
    internal class Delete : CommandBase
    {
        public Delete()
        {
            Aliases = ["delete", "del", "d"];
        }

        protected override void Process(string[] argArray)
        {
            var objs = FileManager.ItemLoader.Equip.ToList();
            objs.RemoveAt(int.Parse(argArray[0]));
            FileManager.ItemLoader.SetEquip(objs.ToArray());
        }
        protected override void Help(bool chain = false)
        {
            Console.WriteLine("Delete: deletes an equipment item.\n" +
                "Usage: item equip delete <index>");
            ListAliases();
        }
    }
}
