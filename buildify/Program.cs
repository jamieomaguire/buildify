using buildify.Factories;
using buildify.Models;
using buildify.Writers;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildify
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Creating project...");

            // Set a variable to the Documents path.
            string projectName = args.Any() ? args[0] : "buildify-ui-template";
            string projectPath = $"{Environment.CurrentDirectory}";
            string innerProjectPath = $"{projectPath}{Path.DirectorySeparatorChar}{projectName}";

            var fileWriter = new ProjectFileWriter();
            var folderWriter = new ProjectFolderWriter();
            var fileFactory = new ProjectFileFactory();
            var folderFactory = new ProjectFolderFactory(projectName);

            var composer = new ProjectComposer(
                fileFactory,
                folderFactory,
                fileWriter,
                folderWriter,
                projectPath,
                projectName);

            await composer.Compose();
            Console.WriteLine($"Project created at: {innerProjectPath}");
        }
    }
}
