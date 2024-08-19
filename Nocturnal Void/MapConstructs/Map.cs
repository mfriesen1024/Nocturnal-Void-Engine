using Nocturnal_Void.Entity.Items;
using Nocturnal_Void.Entity.Movable;
using Nocturnal_Void.FileSystem;
using TZPRenderers.Text;

namespace Nocturnal_Void.MapConstructs
{
    public class Map
    {
        protected const int boundInfoLength = 32;

        public RelativeRenderable renderable;

        public RPGTile[,] tiles;

        public Foe[] foes;

        public Pickup[] pickups;

        public Transition[] transitions;

        public static explicit operator Map(byte[] bytes)
        {
            var list = bytes.ToList();

            // First, lets define some bounds.
            int tilesX = BitConverter.ToInt32(bytes, 0);
            int tilesY = BitConverter.ToInt32(bytes, 4);
            int tileReqBytes = tilesX * tilesY * 3; // This is how many bytes we need for tile array.
            int fStart = BitConverter.ToInt32(bytes, 8);
            int fEnd = BitConverter.ToInt32(bytes, 12);
            int pStart = BitConverter.ToInt32(bytes, 16);
            int pEnd = BitConverter.ToInt32(bytes, 20);
            int tStart = BitConverter.ToInt32(bytes, 24);
            int tEnd = BitConverter.ToInt32(bytes, 28);

            RPGTile[,] tiles = new RPGTile[tilesX, tilesY];

            // x/y might need switching here. idk, havent tested.
            for (int x = 0; x < tilesX; x++)
            {
                for (int y = 0; y < tilesY; y++)
                {
                    int startIndex = x * tilesY * 3 + y * 3;

                    // add the constant to deal with bound definitions.
                    byte[] tileData = list.GetRange(startIndex + boundInfoLength, 3).ToArray();

                    tiles[x, y] = (RPGTile)tileData;
                }
            }

            // Create renderable.
            RelativeRenderable renderable = new RelativeRenderable(tiles);

            List<Foe> foeList = new List<Foe>();
            // Create foe array.
            for (int i = fStart; i < fEnd; i += 12)
            {
                // Coords
                int x = BitConverter.ToInt32(bytes, i);
                int y = BitConverter.ToInt32(bytes, i + 4);
                // foe index
                int foeIndex = BitConverter.ToInt32(bytes, i + 8);

                Foe foe = (Foe)FileManager.EntityLoader.Foes[foeIndex].Clone();
                foe.SetPosition(new Vector2() { y = y, x = x });
                foeList.Add(foe);
            }

            List<Pickup> pickups = new List<Pickup>();
            // Create pickup array.
            for (int i = pStart; i < pEnd; i += Pickup.requiredBytes)
            {
                // This needs to be generated during map generation instead of pickup generation.
                pickups.Add((Pickup)list.GetRange(i, Pickup.requiredBytes).ToArray());
            }

            List<Transition> transitions = new List<Transition>();
            for (int i = tStart; i < tEnd; i += Transition.reqBytes)
            {
                transitions.Add((Transition)list.GetRange(i, Transition.reqBytes).ToArray());
            }

            Map map = new Map() { renderable = renderable, tiles = tiles, foes = foeList.ToArray(), pickups = pickups.ToArray(), transitions = transitions.ToArray() };

            return map;
        }

        public static explicit operator byte[](Map map)
        {
            List<byte> list = new List<byte>();

            // These x/y might be backwards idk.
            int xRange = map.renderable.tiles.GetLength(0);
            int yRange = map.renderable.tiles.GetLength(1);
            list.AddRange(BitConverter.GetBytes(xRange));
            list.AddRange(BitConverter.GetBytes(yRange));

            // figure out foe start/end. Add boundInfoLength to tilebytes to deal with our index definitions.
            int tileBytesLength = map.renderable.tiles.Length * 3;
            int fStart = tileBytesLength + boundInfoLength;
            int fEnd = fStart + map.foes.Length * 12;
            list.AddRange(BitConverter.GetBytes(fStart));
            list.AddRange(BitConverter.GetBytes(fEnd));

            // start/end for pickups. add 1 bc we dont want to use the same byte for 2 different things.
            int pStart = fEnd + 1;
            int pEnd = map.pickups.Length * Pickup.requiredBytes;
            list.AddRange(BitConverter.GetBytes(pStart));
            list.AddRange(BitConverter.GetBytes(pEnd));

            // start/end for transitions.
            int tStart = pEnd + 1;
            int tEnd = map.transitions.Length * Transition.reqBytes;
            list.AddRange(BitConverter.GetBytes(tStart));
            list.AddRange(BitConverter.GetBytes(tEnd));

            // add tiles.
            foreach (RPGTile t in map.renderable.tiles)
            {
                list.AddRange((byte[])t);
            }

            // foe list to check if a given foe is present.
            var foes = FileManager.EntityLoader.Foes.ToList();
            foreach (Foe foe in map.foes)
            {
                if (Foe.ArrayContainsTypeEqual(foes.ToArray(), foe, out int index)) { }
                else { throw new InvalidOperationException("Somehow a foe doesnt exist."); }

                Vector2 pos = foe.location;

                byte[] bytes = [.. (byte[])pos, .. BitConverter.GetBytes(index)];
                list.AddRange(bytes);
            }

            // pickup list
            foreach (Pickup pickup in map.pickups)
            {
                list.AddRange((byte[])pickup);
            }

            return list.ToArray();
        }
    }
}
