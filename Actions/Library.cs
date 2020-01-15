using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BlueScreenCategoriser;
using BlueScreenIO;
using BlueScreenIO.IOInterface;
using BlueScreenIO.Storage;

namespace Actions {
	public class Library {
		private string fileName;

		public bool IndexSource(string source) {
			return true;
		}

		public bool IndexDestination(string destination) {
			return true;
		}

		private bool indexFromLocation(string location) {
			return false;
		}

		public static int BuildFromSource(string source, string destination) {
			var n = 0;

			var allFiles = Directory.GetFiles(source, "*.*", SearchOption.AllDirectories);

			foreach (var f in allFiles) {
				var episodeInfo = FileCategorizer.GetFileInfo(f);

				if (!episodeInfo)
					continue;

				var mBool = FileRelocation.RelocateFile(episodeInfo, destination, StorageType.FileSystem);

				if (mBool)
					n++;
			}

			return n;
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