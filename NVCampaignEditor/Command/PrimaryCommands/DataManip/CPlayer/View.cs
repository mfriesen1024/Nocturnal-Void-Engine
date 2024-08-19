using Nocturnal_Void.Entity.Movable;
using Nocturnal_Void.FileSystem;

namespace NVCampaignEditor.Command.PrimaryCommands.DataManip.CPlayer
{
    internal class View : CommandBase
    {
        public View()
        {
            Aliases = ["list", "l", "view", "v"];
        }

        protected override void Help(bool chain = false)
        {
            Console.WriteLine("View: Shows the player's stats.");
            ListAliases();
        }

        protected override void Process(string[] argArray)
        {
            Player p = FileManager.EntityLoader.Player;
            Console.WriteLine($"Name: {Player.name}, hp: {p.statMan.MaxHP}, def: {p.def}, str: {p.str}");
        }
    }
}
