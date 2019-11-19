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
                return i.Move(newPath);
            }
            catch (Exception e) {
                return new m_Bool(false,e.Message);
            }
        }
    }
}