using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using BlueScreenCategoriser;
using BlueScreenIO;
using BlueScreenIO.IOInterface;
using Utils;

namespace Actions {
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class Library {
		private string fileName;

		public bool IndexSource(string source) {
			return true;
		}

		public bool IndexDestination(string destination) {
			return true;
		}

		bool IndexFromLocation(string location) {
			return false;
		}

		public static int BuildFromSource(string source, string destination) {
			var n = 0;

			var allFiles = Directory.GetFiles(source, "*.*", SearchOption.AllDirectories);
			foreach (var f in allFiles) {
				var episodeInfo = FileCategorizer.GetFileInfo(f);
				Console.WriteLine(f);
				if (!episodeInfo)
					continue;

				var mBool = FileRelocation.RelocateFile(episodeInfo, destination, StorageType.FileSystem);

				if (mBool)
					n++;
			}

			return n;
		}

        public static async Task<int> BuildFromSourceAsync(string source, string destination, StorageType storagetype)
        {
            var allFiles = Directory.GetFiles(source, "*.*", SearchOption.AllDirectories);
            List<Task<m_Bool>> tasks = new List<Task<m_Bool>>();
            foreach (string file in allFiles)
            {
                var episodeInfo = FileCategorizer.GetFileInfo(file);
                if (!episodeInfo)
                    continue;
                tasks.Add(Task.Run(()=>FileRelocation.RelocateFile(episodeInfo,destination,storagetype)));
            }

            var results = await Task.WhenAll(tasks);
            int c = 0;
            foreach (var result in results)
            {
                if (result)
                    c++;
            }


            return c;
        }

        public static List<string> BuildFromSourceWithOutput(string source, string destination) {
			var list = new List<string>();
			var allFiles = Directory.GetFiles(source, "*.*", SearchOption.AllDirectories);

			foreach (var f in allFiles) {
				var episodeInfo = FileCategorizer.GetFileInfo(f);

				if (!episodeInfo)
					continue;

				var mBool = FileRelocation.RelocateFile(episodeInfo, destination, StorageType.FileSystem);

				if (mBool)
					list.Add(mBool.Message());
			}
			return list;
		}
	}
}