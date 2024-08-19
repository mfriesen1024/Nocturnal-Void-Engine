using Nocturnal_Void;
using Nocturnal_Void.Entity.Movable;
using Nocturnal_Void.FileSystem;

namespace NVCampaignEditor.Command.PrimaryCommands.DataManip.CPlayer
{
    internal class Replace : CommandBase
    {
        public Replace()
        {
            Aliases = ["replace", "edit", "e", "r"];
        }

        protected override void Help(bool chain = false)
        {
            Console.WriteLine("Replace: Replaces the player by creating a new one via prompt editor.\n" +
                "Usage: player replace");
            ListAliases();
        }

        protected override void Process(string[] argArray)
        {
            int hp = CommandUtils.GetNumber("Enter a value for HP");
            int str = CommandUtils.GetNumber("Enter a value for str");
            int def = CommandUtils.GetNumber("Enter a value for def");
            string[] v2Args = CommandUtils.GetArgs("Enter a coordinate pair for a starting position.");
            Vector2 vector2 = new Vector2() { x = int.Parse(v2Args[0]), y = int.Parse(v2Args[1]) };
            string[] args = CommandUtils.GetArgs("Enter a tile as 3 chars:\n" +
                "1: the character to use.\n" +
                "2: the foreground colour.\n" +
                "3: the background colour.\n");
            var renderable = CommandUtils.ConstructRenderable(args[0]);

            FileManager.EntityLoader.SetPlayer(new Player("Player", hp, def, str, vector2, renderable));
        }
    }
}
