using Nocturnal_Void.MapConstructs;
using NVCampaignEditor.Util;
using TZPRenderers.Text;

namespace NVCampaignEditor.Command
{
    internal static class CommandUtils
    {

        public static string[] GetArgs(string prompt)
        {
            Console.WriteLine(prompt);
            return StringUtils.ParseArgs(Console.ReadLine());
        }

        public static int GetNumber(string prompt)
        {
            Console.WriteLine(prompt);
            return int.Parse(Console.ReadLine());
        }
        public static RelativeRenderable ConstructRenderable(string tileString)
        {
            char c = tileString[0];
            ConsoleColor fg = (ConsoleColor)StringUtils.HexParse(tileString[1]);
            ConsoleColor bg = (ConsoleColor)StringUtils.HexParse(tileString[2]);
            RPGTile[,] tile = { { new RPGTile() { character = c, fgColor = fg, bgColor = bg, hazardID = 0 } } };
            RelativeRenderable renderable = new RelativeRenderable(tile);
            return renderable;
        }
    }
}