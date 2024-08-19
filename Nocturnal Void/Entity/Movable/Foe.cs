﻿using Nocturnal_Void.Entity.Items;
using Nocturnal_Void.FileSystem;
using Nocturnal_Void.Managers;
using Nocturnal_Void.MapConstructs;
using TZPRenderers.Text;

namespace Nocturnal_Void.Entity.Movable
{
    /// <summary>
    /// Represents an opponent.
    /// </summary>
    public class Foe : MobBase
    {
        // Do loot things here.
        public Item loot;
        public delegate void DeathCallback(Item loot);
        public DeathCallback OnDeath = (ignored) => { }; // Assign later.

        public Foe(string name, int hp, int def, int str, int lootItemIndex, Vector2 location, RelativeRenderable renderable) : base(name, hp, def, str, location, renderable)
        {
            // Value setting

            // Loot and delegation
            loot = FileManager.ItemLoader.AllItems[lootItemIndex];
            statMan.OnDeath += delegate { OnDeath(loot); };
        }

        public Foe() : base()
        {
        }

        void Init()
        {
            statMan.OnDeath += StatmanDeathCallable;
        }

        void StatmanDeathCallable()
        {
            OnDeath(loot);
        }

        public override MobBase Clone()
        {
            Foe foe = (Foe)MemberwiseClone();
            foe.statMan = statMan.Clone();

            return foe;
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This should be a temporary operator.
        /// </summary>
        /// <param name="bytes"></param>
        public static explicit operator Foe(byte[] bytes)
        {
            List<byte> list = bytes.ToList();

            // Construct the name.
            byte nameLength = bytes[0];
            char[] chars = new char[nameLength];
            for (int i = 1; i < nameLength + 1; i++)
            {
                chars[i - 1] = (char)bytes[i];
            }
            string name = new string(chars);

            // Construct basic stats, adding 1 to start index due to our namelength byte.
            int def = BitConverter.ToInt32(bytes, 1 + nameLength);
            int str = BitConverter.ToInt32(bytes, 5 + nameLength);
            int hp = BitConverter.ToInt32(bytes, 9 + nameLength);
            StatManager statMan = new StatManager(hp);

            // Fetch loot.
            int lootIndex = BitConverter.ToInt32(bytes, 13 + nameLength);
            Item loot = FileManager.ItemLoader.AllItems[lootIndex];

            // Construct renderable.
            // Add 0 because we dont want to eat disc space for a value we never use.
            var tileBytes = list.GetRange(17 + nameLength, 2); tileBytes.Add(0);
            RPGTile[,] tileArray = new RPGTile[,] { { (RPGTile)tileBytes.ToArray() } };
            RelativeRenderable renderable = new RelativeRenderable(tileArray);

            // Finally construct the foe itself.
            Foe foe = new Foe() { name = name, def = def, str = str, statMan = statMan, renderable = renderable, loot = loot };
            foe.Init(); // This is to force delegation.
            return foe;
        }

        public static explicit operator byte[](Foe foe)
        {
            List<byte> list = new List<byte>();

            // Deconstruct name
            list.Add((byte)foe.name.Length); // Add the name length so the inverse operation works.
            foreach (char c in foe.name) { list.Add((byte)c); }

            // Deconstruct stats
            list.AddRange(BitConverter.GetBytes(foe.def));
            list.AddRange(BitConverter.GetBytes(foe.str));
            list.AddRange(BitConverter.GetBytes(foe.statMan.MaxHP));

            // Deconstruct loot
            int index = FileManager.ItemLoader.AllItems.ToList().IndexOf(foe.loot);
            list.AddRange(BitConverter.GetBytes(index));

            var tileBytes = ((byte[])(RPGTile)foe.renderable.tiles[0, 0]).ToList();
            list.AddRange(tileBytes.GetRange(0, 2)); // Only store the first 2 bytes to not waste space.

            return list.ToArray();
        }

        public bool TypeEqual(Foe foe)
        {
            if (foe == null) return false;
            if (name != foe.name || def != foe.def || str != foe.str) { return false; }
            return true;
        }

        public static bool ArrayContainsTypeEqual(Foe[] foes, Foe compare) { return ArrayContainsTypeEqual(foes, compare, out int ignored); }
        public static bool ArrayContainsTypeEqual(Foe[] foes, Foe compare, out int indexOf)
        {
            bool b = false;
            indexOf = -1;
            for (int i = 0; i < foes.Length; i++)
            {
                if (foes[i].TypeEqual(compare)) { b = true; indexOf = i; break; }
            }
            return b;
        }
    }
}
