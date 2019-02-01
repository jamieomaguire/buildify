using buildify.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace buildify.Writers
{
    public class ProjectFolderWriter
    {
        public ProjectFolderWriter() { }

        public async Task Write(ProjectFolder folder)
        {
            Directory.CreateDirectory(folder.Path);
        }
    }
}
