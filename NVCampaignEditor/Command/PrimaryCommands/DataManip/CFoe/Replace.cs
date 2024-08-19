﻿using Nocturnal_Void.FileSystem;

namespace NVCampaignEditor.Command.PrimaryCommands.DataManip.CFoe
{
    internal class Replace : CommandBase
    {
        public Replace()
        {
            Aliases = ["replace", "r"];
        }

        protected override void Help(bool chain = false)
        {
            Console.WriteLine("Replace: Replaces an object by creating a new one via prompt editor.\n" +
                "Usage: foe replace <index>");
            ListSubcommands();
        }

        protected override void Process(string[] argArray)
        {
            int index = int.Parse(argArray[0]);

            // I'm not doing argument management. Nope, not again.
            var foes = FileManager.EntityLoader.Foes.ToList();
            foes.RemoveAt(index);
            foes.Insert(index, Create.AskPlayer());
            FileManager.EntityLoader.SetFoes(foes.ToArray());
        }
    }
}
