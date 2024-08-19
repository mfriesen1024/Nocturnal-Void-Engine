using Nocturnal_Void;
using Nocturnal_Void.Entity.Items;
using Nocturnal_Void.Entity.Movable;
using Nocturnal_Void.FileSystem;
using Nocturnal_Void.MapConstructs;
using TZPRenderers.Text;

namespace NVCampaignEditor.Command.PrimaryCommands.DataManip.CMap.CEdit
{

    /// <summary>
    /// A class to deal with helpers for map editing.
    /// </summary>
    public static class EditHelpers
    {
        public static void Foes(int mapIndex)
        {
            Map map = FileManager.MapLoader.Maps[mapIndex];

            int fCount = CommandUtils.GetNumber("How many foes should be added?");

            // Range check count.
            if (fCount <= 0)
            {
                throw new InvalidOperationException($"Cannot create foe array of size {fCount}, it must be greater than 0!\n" +
                $"To make a room where foes don't appear to exist, create 1 foe out of bounds.");
            }
            Foe[] foes = new Foe[fCount];
            for (int i = 0; i < fCount; i++)
            {
                string[] foeArgs = CommandUtils.GetArgs($"Enter x, y and foeIndex (run \"foe list\" for a list of foes) separated by spaces.");
                Foe foe = FileManager.EntityLoader.Foes[int.Parse(foeArgs[2])];
                foe.SetPosition(new Vector2() { x = int.Parse(foeArgs[0]), y = int.Parse(foeArgs[1]) });
                foes[i] = foe;
            }
            map.foes = foes;

            FileManager.MapLoader.Maps[mapIndex] = map;
        }

        public static void Pickups(int mapIndex)
        {
            Map map = FileManager.MapLoader.Maps[mapIndex];

            int pCount = CommandUtils.GetNumber("How many pickups should be added?");

            // Range check count.
            if (pCount <= 0)
            {
                throw new InvalidOperationException($"Cannot create foe array of size {pCount}, it must be greater than 0!\n" +
                $"To make a room where pickups don't appear to exist, create 1 pickup out of bounds.");
            }
            Pickup[] pickups = new Pickup[pCount];
            for (int i = 0; i < pCount; i++)
            {
                string[] pickupArgs = CommandUtils.GetArgs($"Enter x, y, tile and itemIndex separated by spaces.\n" +
                    $"(run \"item list\" for a list of items)\n" +
                    $"tile is 3 characters:" +
                    "1: the character to be rendered.\n" +
                    "2: the foreground, as a hexidecimal int from 0-15\n" +
                    "3: the background, as a hexidecimal int from 0-15\n" +
                    "Example: \"tf0\" renders the character \"t\" with a white foreground and a black background.");

                Vector2 pos = new Vector2() { x = int.Parse(pickupArgs[0]), y = int.Parse(pickupArgs[1]) };
                RelativeRenderable renderable = CommandUtils.ConstructRenderable(pickupArgs[2]);
                Item item = FileManager.ItemLoader.AllItems[int.Parse(pickupArgs[3])];
                Pickup pickup = new Pickup(renderable, item, pos);
                pickups[i] = pickup;
            }
            map.pickups = pickups;

            FileManager.MapLoader.Maps[mapIndex] = map;
        }

        public static void Transitions(int mapIndex)
        {
            Map map = FileManager.MapLoader.Maps[mapIndex];

            int tCount = CommandUtils.GetNumber("How many transitions should be added?");

            // Range check count.
            if (tCount <= 0)
            {
                throw new InvalidOperationException($"Cannot create transition array of size {tCount}, it must be greater than 0!\n" +
                $"To make a room where transitions don't appear to exist, create 1 transition with a behavior of 0.");
            }
            Transition[] transitions = new Transition[tCount];
            for (int i = 0; i < tCount; i++)
            {
                string[] transitionArgs = CommandUtils.GetArgs($"Enter min x, max x, min y, max y and trigger function index.");
                Transition transition = new Transition(
                    int.Parse(transitionArgs[0]),
                    int.Parse(transitionArgs[1]),
                    int.Parse(transitionArgs[2]),
                    int.Parse(transitionArgs[3]))
                { Type = int.Parse(transitionArgs[4]) };
            }
            map.transitions = transitions;

            FileManager.MapLoader.Maps[mapIndex] = map;
        }
    }
}

