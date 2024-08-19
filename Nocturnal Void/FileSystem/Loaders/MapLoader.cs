using Nocturnal_Void.MapConstructs;
using CFile = Nocturnal_Void.FileSystem.Util.File;

namespace Nocturnal_Void.FileSystem.Loaders
{
    /// <summary>
    /// A loader that constructs/stores/saves maps.
    /// </summary>
    public class MapLoader : LoaderBase
    {
        private int mapCount;
        private Map[] maps;
        public MapLoader(string fName) : base(fName)
        {
        }

        public Map[] Maps { get => maps; }
        public int MapCount { get => mapCount; }

        /// <summary>
        /// Loads an array of Map from individual files.
        /// </summary>
        /// <param name="path">The directory in which the files are located.</param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Load(CFile path)
        {
            path = new CFile(path, fName);
            CFile indexFile = new CFile(path, ".mapinfo");

            mapCount = BitConverter.ToInt32(indexFile.ReadBytes());

            CFile[] mapFiles = new CFile[mapCount];
            maps = new Map[mapCount];
            for (int i = 0; i < mapCount; i++)
            {
                mapFiles[i] = new CFile(path, $"map{i}.map.bin");
                maps[i] = (Map)mapFiles[i].ReadBytes();
            }
        }

        public override void Save(CFile path)
        {
            path = new CFile(path, fName);
            CFile indexFile = new CFile(path, ".mapinfo");
            indexFile.WriteBytes(BitConverter.GetBytes(maps.Length));

            for (int i = 0; i < mapCount; i++)
            {
                CFile file = new CFile(path, $"map{i}.map.bin");
                file.WriteBytes((byte[])maps[i]);
            }
        }

        public void SetMaps(Map[] maps)
        {
            this.maps = maps;
            mapCount = maps.Length;
        }
    }
}
