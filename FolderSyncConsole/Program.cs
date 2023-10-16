using System.Numerics;

namespace FolderSyncConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 4)
            {
                Console.WriteLine("Usage: FolderSynchronizer.exe <sourceFolderPath> <replicaFolderPath> <Interval> <logFilePath>");
                return;
            }

            string sourceFolderPath = args[0];
            string replicaFolderPath = args[1];
            int interval = 5000;
            int.TryParse(args[2], out interval);
            string logFilePath = args[3];

            File.WriteAllText(logFilePath, "");

            Console.WriteLine($"Synchronization started. Source: {sourceFolderPath}, Replica: {replicaFolderPath}");

            while (true)
            {
                SynchronizeFolders(sourceFolderPath, replicaFolderPath, logFilePath);
                Thread.Sleep(interval);

            }
           
        }
        static void SynchronizeFolders(string sourceFolderPath, string replicaFolderPath, string logFilePath)
        {
            try
            {
                if (!Directory.Exists(replicaFolderPath))
                {
                    Directory.CreateDirectory(replicaFolderPath);
                    Log($"Created replica folder: {replicaFolderPath}", logFilePath);
                }

                foreach (string sourceFile in Directory.GetFiles(sourceFolderPath))
                {
                    string fileName = Path.GetFileName(sourceFile);
                    string replicaFilePath = Path.Combine(replicaFolderPath, fileName);

                    File.Copy(sourceFile, replicaFilePath, true);

                    Log($"Copied file: {fileName}", logFilePath);
                }

                foreach (string replicaFile in Directory.GetFiles(replicaFolderPath))
                {
                    string fileName = Path.GetFileName(replicaFile);
                    string sourceFilePath = Path.Combine(sourceFolderPath, fileName);

                    if (!File.Exists(sourceFilePath))
                    {
                        File.Delete(replicaFile);
                        Log($"Removed file: {fileName}", logFilePath);
                    }
                }

                Log("Synchronization completed.", logFilePath);
            }
            catch (Exception ex)
            {
                Log($"Error during synchronization: {ex.Message} + source: {sourceFolderPath} + replica: {replicaFolderPath}", logFilePath);
            }
        }

        static void Log(string message, string logFilePath)
        {
            Console.WriteLine(message);
            File.AppendAllText(logFilePath, $"{DateTime.Now}: {message}\n");
        }

    }
}