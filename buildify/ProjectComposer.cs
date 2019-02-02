using buildify.Factories;
using buildify.Writers;
using System.Threading.Tasks;

namespace buildify
{
    /// <summary>
    /// Responsible for composing the project template.
    /// </summary>
    public class ProjectComposer
    {
        private readonly ProjectFileFactory fileFactory;
        private readonly ProjectFolderFactory folderFactory;
        private readonly ProjectFileWriter fileWriter;
        private readonly ProjectFolderWriter folderWriter;
        private readonly string projectPath;
        private readonly string projectName;
        private string InnerProjectPath
        {
            get
            {
                return $"{projectPath}\\{projectName}";
            }
        }

        public ProjectComposer(
            ProjectFileFactory fileFactory,
            ProjectFolderFactory folderFactory,
            ProjectFileWriter fileWriter,
            ProjectFolderWriter folderWriter,
            string projectPath,
            string projectName)
        {
            this.fileFactory = fileFactory;
            this.folderFactory = folderFactory;
            this.fileWriter = fileWriter;
            this.folderWriter = folderWriter;
            this.projectPath = projectPath;
            this.projectName = projectName;
        }

        /// <summary>
        /// Composes the project by creating all the required files and directories.
        /// </summary>
        /// <param name="projectPath">The project file path.</param>
        /// <returns></returns>
        public async Task Compose()
        {
            // Folders
            await CreateRootDirectory();
            await CreateDevDirectory();
            CreateProdDirectory();
            CreateScssOutputDirectory();

            // Files
            CreateIndexHtmlFile();
            CreatePackageJsonFile();
            CreateGulpfileJsFile();
            CreateMainScssFile();
        }

        #region Directories
        /// <summary>
        /// Create the project root directory.
        /// </summary>
        private async Task CreateRootDirectory()
        {
            await folderWriter.Write(folderFactory.RootFolder(projectPath));
        }

        /// <summary>
        /// Create the dev directory within the project root directory.
        /// </summary>
        private async Task CreateDevDirectory()
        {
            await folderWriter.Write(folderFactory.DevFolder(InnerProjectPath));
        }

        /// <summary>
        /// Create the prod directory within the project root directory.
        /// </summary>
        private async Task CreateProdDirectory()
        {
            await folderWriter.Write(folderFactory.ProdFolder(InnerProjectPath));
        }

        /// <summary>
        /// Create the scss-output directory within the dev directory.
        /// </summary>
        private async Task CreateScssOutputDirectory()
        {
            await folderWriter.Write(folderFactory.ScssOutputFolder($"{InnerProjectPath}\\dev"));
        }
        #endregion

        #region Files
        /// <summary>
        /// Create the index.html file within the project root directory.
        /// </summary>
        private async Task CreateIndexHtmlFile()
        {
            await fileWriter.Write(await fileFactory.IndexHtml(InnerProjectPath));
        }

        /// <summary>
        /// Create the Gulpfile.js file within the project root directory.
        /// </summary>
        private async Task CreateGulpfileJsFile()
        {
            await fileWriter.Write(await fileFactory.GulpFileJs($"{InnerProjectPath}"));
        }

        /// <summary>
        /// Create the main.scss file within the dev directory.
        /// </summary>
        private async Task CreateMainScssFile()
        {
            await fileWriter.Write(await fileFactory.MainScss($"{InnerProjectPath}\\dev"));
        }

        /// <summary>
        /// Create the package.json file within the project root directory.
        /// </summary>
        private async Task CreatePackageJsonFile()
        {
            await fileWriter.Write(await fileFactory.PackageJson(InnerProjectPath));
        }
        #endregion
    }
}
