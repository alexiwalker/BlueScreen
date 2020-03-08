using System;
using System.IO;
using BlueScreenCategoriser;
using Utils;

namespace BlueScreenIO.IOInterface
{
    public static class FileRelocation
    {
        private static StorageInterface getInterface(StorageType t, string path) {
            switch (t) {
                case StorageType.DataBase:
                    return new DatabaseInterface(path);
                case StorageType.FileSystem:
                    return new FileSystemInterface(path);
                default:
                    throw new ArgumentOutOfRangeException(nameof(t), t, null);
            }
        }

        public static m_Bool RelocateFile(EpisodeInfo episodeInfo,string destination, StorageType type) {
            try {
                var path = episodeInfo.path;
                var extension = Path.GetExtension(episodeInfo.path);
                var newPath = $"{destination}\\{episodeInfo.toDirectory()}{extension}";
                
                var i = getInterface(type,path);
                Console.WriteLine($"moving {path} to {newPath}");
                var mb = i.Move(newPath);
                Console.WriteLine($"moved {path} to {newPath}");
                return mb;
            }
            catch (Exception e) {
                Console.WriteLine($"Error moving file");
                return new m_Bool(false,e.Message);
            }
        }
    }
}