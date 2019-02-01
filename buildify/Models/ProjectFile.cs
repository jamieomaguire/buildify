using System;
using System.Collections.Generic;
using System.Text;

namespace buildify.Models
{
    public sealed class ProjectFile
    {
        public ProjectFile(string name, string extension, string path, string content)
        {
            fileName = name;
            fileExtension = extension;
            Path = path;
            Content = content;
        }

        private readonly string fileName;
        private readonly string fileExtension;

        public string Name
        {
            get
            {
                return $"{fileName}.{fileExtension}";
            }
        }

        public string Path { get; }
        public string Content { get; }
    }
}
