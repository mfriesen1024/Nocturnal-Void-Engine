using Nocturnal_Void.FileSystem;
using Nocturnal_Void.MapConstructs;
using NVCampaignEditor.Util;
using TZPRenderers.Text;

namespace NVCampaignEditor.Command.PrimaryCommands.DataManip.CMap.CCompiler
{
    internal class TemplateCompiler : CommandBase
    {
        public TemplateCompiler()
        {
            Aliases = ["compile"];
        }

        protected override void Help(bool chain = false)
        {
            Console.WriteLine("Compile: compiles a tile array to the specified map from text files.\n" +
                "Usage: compile <index>");
        }

        protected override void Process(string[] argArray)
        {
            int mapIndex = int.Parse(argArray[0]);
            Map map = FileManager.MapLoader.Maps[mapIndex];
            CFile[] files = CompileMain.GetFiles(mapIndex);

            // Get string arrays.
            string[] chars, fg, bg, haz;
            chars = files[0].ReadStrings();
            fg = files[1].ReadStrings();
            bg = files[2].ReadStrings();
            haz = files[3].ReadStrings();

            // Get bounds.
            int x = chars[0].Length;
            int y = chars.Length;

            // I have no idea if this is right or not.
            RPGTile[,] tiles = new RPGTile[y, x];

            // 2d for loop.
            for (int yIndex = 0; yIndex < y; yIndex++)
            {
                for (int xIndex = 0; xIndex < x; xIndex++)
                {
                    RPGTile t = tiles[yIndex, xIndex];
                    if (t != null)
                    {
                        t.character = chars[yIndex][xIndex];
                        t.fgColor = (ConsoleColor)StringUtils.HexParse(fg[yIndex][xIndex]);
                        t.bgColor = (ConsoleColor)StringUtils.HexParse(bg[yIndex][xIndex]);
                        t.hazardID = (byte)StringUtils.HexParse(haz[yIndex][xIndex]);
                    }
                    else
                    {
                        t = new RPGTile(
                            chars[yIndex][xIndex],
                            (ConsoleColor)StringUtils.HexParse(fg[yIndex][xIndex]),
                            (ConsoleColor)StringUtils.HexParse(bg[yIndex][xIndex]),
                            (byte)StringUtils.HexParse(haz[yIndex][xIndex]));
                    }
                    // Not sure if I need to reassign, but do it anyway.
                    tiles[yIndex, xIndex] = t;
                }
            }

            if (map.renderable != null) { map.renderable.tiles = tiles; }
            else { map.renderable = new RelativeRenderable(tiles); }
            map.tiles = tiles;

            // I probably dont need to do this, but whatever.
            FileManager.MapLoader.Maps[mapIndex] = map;
        }
    }
}
