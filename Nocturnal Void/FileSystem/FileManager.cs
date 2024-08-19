using Nocturnal_Void.Entity.Items;
using Nocturnal_Void.Entity.Movable;
using Nocturnal_Void.FileSystem.Loaders;
using Nocturnal_Void.MapConstructs;
using TZPRenderers.Text;
using File = Nocturnal_Void.FileSystem.Util.File;

namespace Nocturnal_Void.FileSystem
{
    public static class FileManager
    {
        private static File workingDir = new File("temp");
        public static ItemLoader ItemLoader { get; private set; } = new ItemLoader("items.bin");
        public static EntityLoader EntityLoader { get; private set; } = new EntityLoader("entities.bin");
        public static MapLoader MapLoader { get; private set; } = new MapLoader("maps");
        public static File WorkingDir { get => workingDir; }

        public static void Load(string path)
        {
            File dir = new File(path);
            workingDir = dir;

            ItemLoader.Load(dir);
            EntityLoader.Load(dir);
            MapLoader.Load(dir);
        }

        public static void Save(string path)
        {
            File dir = new File(path);
            workingDir = dir;

            ItemLoader.Save(dir);
            EntityLoader.Save(dir);
            MapLoader.Save(dir);
        }

        /// <summary>
        /// Initializes the system with sample data.
        /// </summary>
        public static void Init()
        {
            // Default items.
            RPGTile[,] tiles = { { new RPGTile('d', ConsoleColor.White, ConsoleColor.Black) } };
            RelativeRenderable renderable = new RelativeRenderable(tiles);

            // Item init.
            Consumable[] consumables = [new Consumable(0, 1)];
            ItemLoader.SetConsumables(consumables);
            Equipment[] equip = [new Equipment(0, 1)];
            ItemLoader.SetEquip(equip);
            Gold goldObj = new Gold(1);
            Gold[] goldObjs = [goldObj];
            ItemLoader.SetGold(goldObjs);
            Pickup[] pickups = [new Pickup(renderable, goldObj, Vector2.Zero)];
            ItemLoader.SetPickups(pickups);

            // Entity init.
            EntityLoader.SetPlayer(new Player("Player", 100, 0, 10, Vector2.Zero, renderable));
            Foe[] foes = [new Foe("SampleFoe", 10, 5, 2, 2, Vector2.Zero, renderable)];
            EntityLoader.SetFoes(foes);

            // Map init.
            Map map = new Map()
            {
                tiles = tiles,
                renderable = renderable,
                foes = foes,
                pickups = pickups,
                transitions = [new Transition(0, 0, 0, 0) { Type = 0 }]
            };
            MapLoader.SetMaps([map]);
        }
    }
}
