using Nocturnal_Void.Entity.Movable;
using Nocturnal_Void.FileSystem;

namespace NVCampaignEditor.Command.PrimaryCommands.DataManip.CFoe
{
    public class List : CommandBase
    {
        public List()
        {
            Aliases = ["list", "l"];
        }

        protected override void Help(bool chain = false)
        {
            Console.WriteLine("List: Lists the objects to which this subcommand applies.\n" +
                "Usage: foe list");
            ListAliases();
        }

        protected override void Process(string[] argArray)
        {
            Foe[] foes = FileManager.EntityLoader.Foes;
            foreach (Foe foe in foes)
            {
                Console.WriteLine($"{foe.name} hp: {foe.statMan.MaxHP}, def: {foe.def}, str: {foe.str}");
            }
        }
    }
}
