using Nocturnal_Void.FileSystem;
using Nocturnal_Void.MapConstructs;
using NVCampaignEditor.Command.PrimaryCommands.DataManip.CMap.CCompiler;
using NVCampaignEditor.Command.PrimaryCommands.DataManip.CMap.CEdit;

namespace NVCampaignEditor.Command.PrimaryCommands.DataManip.CMap
{
    public class Create : CommandBase
    {
        public Create()
        {
            Aliases = ["create", "c"];
        }

        protected override void Process(string[] argArray)
        {
            var maps = FileManager.MapLoader.Maps.ToList();
            maps.Add(new Map());
            FileManager.MapLoader.SetMaps(maps.ToArray());

            int mapIndex = maps.Count - 1;

            EditHelpers.Foes(mapIndex);
            EditHelpers.Pickups(mapIndex);
            EditHelpers.Transitions(mapIndex);
            new TemplateGenerator().Invoke([mapIndex.ToString(), .. argArray]);
            new TemplateCompiler().Invoke([mapIndex.ToString(), .. argArray]);

            Console.WriteLine($"Template text files were created for the tile array. Please populate them and run \"map compiler compile\" before saving.");
        }

        protected override void Help(bool chain = false)
        {
            Console.WriteLine("Create: creates an object using a prompt editor.\n" +
                "Usage: create");
            ListAliases();
        }
    }
}
