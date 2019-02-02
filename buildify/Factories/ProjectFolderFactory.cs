using buildify.Models;

namespace buildify.Factories
{
    /// <summary>
    /// Responsible for creating new instances of <see cref="ProjectFolder"/>.
    /// </summary>
    public class ProjectFolderFactory
    {
        private readonly string projectName;

        public ProjectFolderFactory(string projectName)
        {
            this.projectName = projectName;
        }

        public ProjectFolder RootFolder(string path)
        {
            return new ProjectFolder(projectName, path);
        }

        public ProjectFolder DevFolder(string path)
        {
            return new ProjectFolder("dev", path);
        }

        public ProjectFolder ProdFolder(string path)
        {
            return new ProjectFolder("prod", path);
        }

        public ProjectFolder ScssOutputFolder(string path)
        {
            return new ProjectFolder("scss-output", path);
        }
    }
}