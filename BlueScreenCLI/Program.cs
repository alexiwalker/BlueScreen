using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Actions;
using BlueScreenIO;


namespace BlueScreenCLI
{
    internal class Program
    {
        const StorageType _storageType = StorageType.FileSystem;

        const string FileName = "args.bsf";
        static readonly string ExePath = Assembly.GetEntryAssembly()?.Location;
        static readonly string AppPath = Path.GetDirectoryName(ExePath);
        static readonly string FilePath = $"{AppPath}{Path.DirectorySeparatorChar}{FileName}";
        public static async Task Main(string[] args)
        {
            string source;
            string destination;

            
            if (args.Contains("-s"))
            {
                var argsToSave = new List<string>();
                try
                {
                    foreach (var arg in args)
                    {    
                        Console.WriteLine(arg);
                        if (arg == "-s") continue;
                        argsToSave.Add(arg);
                    }

                    var fileContentsToWrite = $"{argsToSave[0]},{argsToSave[1]}";
                    Console.WriteLine(fileContentsToWrite);
                    File.WriteAllText(FilePath,fileContentsToWrite);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error:  {e.Message}. Exiting");
                    return;
                }
                Console.WriteLine("Parameters saved");
                return;
            }
            
            if (args.Contains("-l"))
            {
                if (!File.Exists(FilePath))
                {
                    Console.WriteLine("tried to load settings but args.bsf does not exist");
                }

                var argsToLoad = File.ReadAllText(FilePath);

                try
                {
                    var loadedArgs = argsToLoad.Split(',');
                    args = new string[2];
                    args[0] = loadedArgs[0];
                    args[1] = loadedArgs[1];
                    
                    Console.WriteLine($"Moving files from: {args[0]} to {args[1]}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}. Exiting");
                    return;
                }
                
            }

            try
            {
                source = args[0];
                destination = args[1];

                if (!Directory.Exists(source))
                {
                    throw new IOException($"Source does not exist: {source}");
                }

                if (!Directory.Exists(destination))
                {
                    Console.WriteLine($"Destination folder does not exist: {destination}. Create it?");
                    var confirmation = Console.ReadLine();
                    if (confirmation!=null && (confirmation.ToLower() == "y" || confirmation.ToLower() == "yes"))
                        Directory.CreateDirectory(destination);
                    else
                    {
                        Console.WriteLine("Folder not created. Exiting.");
                        return;
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter arguments: 'C:\\SourceFolder' 'C:\\DestinationFolder''");
                return;
            }

            try
            {
                var n = await Library.BuildFromSourceAsync(source, destination, _storageType);
                Console.WriteLine($"Moved {n} Files");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}