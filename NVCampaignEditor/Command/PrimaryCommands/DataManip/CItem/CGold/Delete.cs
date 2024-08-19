﻿using Nocturnal_Void.FileSystem;

namespace NVCampaignEditor.Command.PrimaryCommands.DataManip.CItem.CGold
{
    internal class Delete : CommandBase
    {
        public Delete()
        {
            Aliases = ["delete", "del", "d"];
        }

        protected override void Process(string[] argArray)
        {
            var objs = FileManager.ItemLoader.GoldItems.ToList();
            objs.RemoveAt(int.Parse(argArray[0]));
            FileManager.ItemLoader.SetGold(objs.ToArray());
        }

        protected override void Help(bool chain = false)
        {
            Console.WriteLine("Delete: deletes a gold item.\n" +
                "Usage: item gold delete <index>");
            ListAliases();
        }
    }
}
