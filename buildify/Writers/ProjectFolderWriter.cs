using buildify.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace buildify.Writers
{
    /// <summary>
    /// Writes a project folder to a given path.
    /// </summary>
    public class ProjectFolderWriter
    {
        public async Task Write(ProjectFolder folder)
        {
            Directory.CreateDirectory(folder.Path);
        }
    }
}
