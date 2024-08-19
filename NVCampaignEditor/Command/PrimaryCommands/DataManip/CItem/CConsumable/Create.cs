﻿using Nocturnal_Void.Entity.Items;
using Nocturnal_Void.FileSystem;

namespace NVCampaignEditor.Command.PrimaryCommands.DataManip.CItem.CConsumable
{
    public class Create : CommandBase
    {
        public Create()
        {
            Aliases = ["create", "c"];
        }

        protected override void Process(string[] argArray)
        {
            var objs = FileManager.ItemLoader.Consumables.ToList();

            if (argArray.Length < 2) { objs.Add(AskUser()); }
            else { objs.Add(new Consumable((Consumable.ConsumableType)int.Parse(argArray[0]), int.Parse(argArray[1]))); }

            FileManager.ItemLoader.SetConsumables(objs.ToArray());
        }

        public static Consumable AskUser()
        {
            Console.WriteLine($"Currently only heal type is supported. Please enter \"0\"");
            int typeInt = int.Parse(Console.ReadLine());
            Console.WriteLine($"Enter the value for the item.");
            int value = int.Parse(Console.ReadLine());
            return new Consumable((Consumable.ConsumableType)typeInt, value);
        }

        protected override void Help(bool chain = false)
        {
            Console.WriteLine("Create: creates an object.\n" +
                "Usage: create <type> <value>\n" +
                "Alternate usage: create");
            ListAliases();
        }
    }
}
