using Nocturnal_Void;
using Nocturnal_Void.Entity.Movable;
using Nocturnal_Void.FileSystem;
using TZPRenderers.Text;

namespace NVCampaignEditor.Command.PrimaryCommands.DataManip.CFoe
{
    public class Create : CommandBase
    {
        public Create()
        {
            Aliases = ["create", "c", "add", "a"];
        }

        protected override void Process(string[] argArray)
        {
            List<Foe> foes = FileManager.EntityLoader.Foes.ToList();
            Foe foe;

            if (argArray.Length >= 6)
            {
                string name = argArray[0];
                int maxHp = int.Parse(argArray[1]);
                int def = int.Parse(argArray[2]);
                int str = int.Parse(argArray[3]);
                int itemIndex = int.Parse(argArray[4]);
                string tileString = argArray[5];

                RelativeRenderable renderable = CommandUtils.ConstructRenderable(tileString);

                foe = new Foe(name, maxHp, def, str, itemIndex, Vector2.Zero, renderable);
            }
            else { foe = AskPlayer(); }

            foes.Add(foe);
            FileManager.EntityLoader.SetFoes(foes.ToArray());
            Console.WriteLine($"Added foe with name {foe.name}");
        }

        public static Foe AskPlayer()
        {
            // Ask the user for details, because it'll be confusing otherwise.
            Console.WriteLine("Enter a name.");
            string name = Console.ReadLine();
            Console.WriteLine("Enter max hp.");
            int maxHP = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter defence.");
            int def = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter strength.");
            int str = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter item index.");
            int itemIndex = int.Parse(Console.ReadLine());
            // Use vector2.zero for pos, it should be set via map. Silly me.

            // Parse a renderable.
            Console.WriteLine("Enter a tile as 3 characters.\n" +
                "1: the character to be rendered.\n" +
                "2: the foreground, as a hexidecimal int from 0-15\n" +
                "3: the background, as a hexidecimal int from 0-15\n" +
                "Example: \"tf0\" renders the character \"t\" with a white foreground and a black background.");
            string tileString = Console.ReadLine();
            RelativeRenderable renderable = CommandUtils.ConstructRenderable(tileString);

            Foe foe = new Foe(name, maxHP, def, str, itemIndex, Vector2.Zero, renderable);
            return foe;
        }

        protected override void Help(bool chain = false)
        {
            Console.WriteLine("Create: creates a new foe using either arguments, or a prompt reader.\n" +
                "Usage: foe create <name> <hp> <def> <str> <itemIndex> <tile>\n" +
                "Alternate usage: foe create");
            ListAliases();
        }
    }
}
