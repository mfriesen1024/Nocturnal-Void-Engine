namespace NVCampaignEditor.Command.PrimaryCommands.DataManip.CMap.CCompiler
{
    internal class TemplateGenerator : CommandBase
    {
        public TemplateGenerator()
        {
            Aliases = ["create", "generate", "gen", "g"];
        }

        protected override void Help(bool chain = false)
        {
            Console.WriteLine("Generate: Creates a tile array template for the specified map with the given size (1,1 if empty).\n" +
                "Usage: generate <index> [<x> <y>]");
            ListAliases();
        }

        protected override void Process(string[] argArray)
        {
            // Index for filenames
            int mapIndex = int.Parse(argArray[0]);
            // bounds for array.
            int x = 1, y = 1;

            // If the user specified a size, use it.
            if (argArray.Length > 2)
            {
                x = int.Parse(argArray[1]);
                y = int.Parse(argArray[2]);
            }

            // Generate a string array to use as template data. Fill it with the character '0'
            string[] template;
            var charList = new List<char>();
            var stringList = new List<string>();
            for (int i = 0; i < x; i++) { charList.Add('0'); }
            for (int i = 0; i < y; i++) { stringList.Add(new string(charList.ToArray())); }
            template = stringList.ToArray();

            // Generate and fill the files themselves.
            CFile[] files = CompileMain.GetFiles(mapIndex);
            foreach (CFile file in files)
            {
                file.WriteLines(template);
            }
        }
    }
}
