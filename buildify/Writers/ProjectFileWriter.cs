using System;
using System.Collections.Generic;
using buildify.Models;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace buildify.Writers
{
    public class ProjectFileWriter
    {

        public ProjectFileWriter() { }

        public async Task Write(ProjectFile file)
        {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(file.Path, file.Name)))
            {
                await outputFile.WriteAsync(file.Content);
            }
        }
    }
}
