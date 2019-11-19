using System;
using System.IO;
using BlueScreenCategoriser;
using BlueScreenIO;
using BlueScreenIO.IOInterface;

namespace BlueScreenCLI {
	internal class Program {
		public static StorageType _storageType = StorageType.FileSystem;

		public static void Main(string[] args) {
			string source;
			string destination;
			
			try {
				 source = args[0];
				 destination = args[1];
			}
			catch (Exception) {
				Console.WriteLine("Please enter arguments: 'C:\\SourceFolder' 'C:\\DestinationFolder''");
				return;
			}

			var allFiles = Directory.GetFiles(source, "*.*", SearchOption.AllDirectories);

			foreach (var f in allFiles) {
				var episodeInfo = FileCategorizer.GetFileInfo(f);

				if (!episodeInfo) {
					Console.WriteLine($"Unable to extract episode details from path provided, or invalid extension for: {f}");
					continue;
				}

				var mBool = FileRelocation.RelocateFile(episodeInfo, destination, _storageType);

				if (!mBool)
					Console.WriteLine($"move not successful for: {f}. Error: {mBool} ");
			}
		}
	}
}