using System;
using System.Collections.Generic;
using System.Text;

namespace buildify.Models
{
    public sealed class ProjectFolder
    {
        public ProjectFolder(string name, string path)
        {
            Name = name;
            folderPath = path;
        }

        private readonly string folderPath;
        public string Name { get; }

        public string Path
        {
            get
            {
                return $"{folderPath}\\{Name}";
            }
        }
    }
}
