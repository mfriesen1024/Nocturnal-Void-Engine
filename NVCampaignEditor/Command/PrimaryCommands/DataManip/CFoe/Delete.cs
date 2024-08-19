using Nocturnal_Void.FileSystem;

namespace NVCampaignEditor.Command.PrimaryCommands.DataManip.CFoe
{
    public class Delete : CommandBase
    {
        public Delete()
        {
            Aliases = ["delete", "del", "d"];
        }

        protected override void Help(bool chain = false)
        {
            Console.WriteLine("Delete: deletes a foe.\n" +
                "Usage: foe delete <index>");
            ListAliases();
        }

        protected override void Process(string[] argArray)
        {
            int i = int.Parse(argArray[0]);

            var foes = FileManager.EntityLoader.Foes.ToList();
            foes.RemoveAt(i);
        }
    }
}
