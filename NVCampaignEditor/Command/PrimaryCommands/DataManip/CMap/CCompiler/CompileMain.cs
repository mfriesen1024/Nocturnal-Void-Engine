using Nocturnal_Void.FileSystem;
using File = Nocturnal_Void.FileSystem.Util.File;

namespace NVCampaignEditor.Command.PrimaryCommands.DataManip.CMap.CCompiler
{
    /// <summary>
    /// This should compile a map from text files into the editor to be saved as .bin
    /// </summary>
    public class CompileMain : CommandBase
    {
        public CompileMain()
        {
            Aliases = ["compiler"];
            Subcommands = [new TemplateCompiler(), new TemplateGenerator()];
        }

        protected override void Process(string[] argArray)
        {
            throw new ArgumentException("Invalid command.");
        }

        public static File[] GetFiles(int i)
        {
            // Figure out what our working directory should be.
            File dir = new File(FileManager.WorkingDir, "maps");

            File chars, fg, bg, haz;
            chars = new File(dir, $"map{i}char.txt");
            fg = new File(dir, $"map{i}fg.txt");
            bg = new File(dir, $"map{i}bg.txt");
            haz = new File(dir, $"map{i}haz.txt");

            return [chars, fg, bg, haz];
        }

        protected override void Help(bool chain = false)
        {
            Console.WriteLine("Compiler: compiles and decompiles a map to/from text files.");
            // don't list aliases, because we only have one name.
            ListSubcommands();
        }
    }
}
