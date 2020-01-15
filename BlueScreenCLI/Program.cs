using System;
using System.IO;
using Actions;
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

			var n = Library.BuildFromSource(source, destination).ToString();
			
			Console.WriteLine($"Moved {n} Files");
		}
	}
}