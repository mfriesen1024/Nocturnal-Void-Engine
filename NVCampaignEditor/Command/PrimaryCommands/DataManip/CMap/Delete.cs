using Nocturnal_Void.FileSystem;

namespace NVCampaignEditor.Command.PrimaryCommands.DataManip.CMap
{
    public class Delete : CommandBase
    {
        public Delete()
        {
            Aliases = ["delete", "del", "d"];
        }

        protected override void Process(string[] argArray)
        {
            int i = int.Parse(argArray[0]);

            var maps = FileManager.MapLoader.Maps.ToList();
            maps.RemoveAt(i);
            FileManager.MapLoader.SetMaps(maps.ToArray());
        }
        protected override void Help(bool chain = false)
        {
            Console.WriteLine("Delete: deletes a map.\n" +
                "Usage: map delete <index>");
            ListAliases();
        }
    }
}
