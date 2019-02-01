using buildify.Models;
using buildify.Writers;
using System;
using System.IO;
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
            string projectName = args[0];
            string projectPath = $"{Environment.CurrentDirectory}";
            string innerProjectPath = $"{projectPath}\\{projectName}";

            await CreateProjectFolder(projectName, projectPath);

            WriteGulpFileJs(innerProjectPath);

            WriteIndexHtml(innerProjectPath);

            WriteStyleScss(innerProjectPath);

            CreateCssDirectory(innerProjectPath);

            WritePackageJson(innerProjectPath);

            Console.WriteLine($"Project created at: {innerProjectPath}");
        }

        public static async Task CreateProjectFolder(string folderName, string docPath)
        {
            var writer = new ProjectFolderWriter();
            var folder = new ProjectFolder(folderName, docPath);
            await writer.Write(folder);
        }

        public static async Task CreateCssDirectory(string docPath)
        {
            var cssFolder = new ProjectFolder("css", docPath);
            var writer = new ProjectFolderWriter();
            await writer.Write(cssFolder);
        }

        public static async Task WriteGulpFileJs(string docPath)
        {
            var writer = new ProjectFileWriter();
            var sb = new StringBuilder();

            sb.AppendLine("var gulp = require('gulp');");
            sb.AppendLine("var sass = require('gulp-sass');");
            sb.AppendLine();
            sb.AppendLine("gulp.task('styles', function() {");
            sb.AppendLine("    gulp.src('sass/**/*.scss')");
            sb.AppendLine("        .pipe(sass().on('error', sass.logError))");
            sb.AppendLine("        .pipe(gulp.dest('./css/'))");
            sb.AppendLine("});");
            sb.AppendLine();
            sb.AppendLine("// Watch task");
            sb.AppendLine("gulp.task('default', function() {");
            sb.AppendLine("    gulp.watch('sass/**/*.scss', gulp.series('styles'));");
            sb.AppendLine("});");


            var gulpFileJs = new ProjectFile("Gulpfile", "js", docPath, sb.ToString());
            await writer.Write(gulpFileJs);
        }

        public static async Task WriteIndexHtml(string docPath)
        {
            var writer = new ProjectFileWriter();
            var sb = new StringBuilder();

            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang=\"en\">");
            sb.AppendLine("<head>");
            sb.AppendLine("  <meta charset=\"UTF-8\">");
            sb.AppendLine("  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
            sb.AppendLine("  <meta http-equiv=\"X-UA-Compatible\" content=\"ie=edge\">");
            sb.AppendLine("  <title>Document</title>");
            sb.AppendLine("	<link rel=\"stylesheet\" type=\"text/css\" href=\"./css/style.css\"/>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("  TEST");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            var indexHtmlFile = new ProjectFile("index", "html", docPath, sb.ToString());
            await writer.Write(indexHtmlFile);
        }

        public static async Task WriteStyleScss(string docPath)
        {
            // file and folder writers
            var fileWriter = new ProjectFileWriter();
            var folderWriter = new ProjectFolderWriter();

            // create the folder
            var sassFolder = new ProjectFolder("sass", docPath);
            await folderWriter.Write(sassFolder);

            // create the SCSS file
            string scssContent = "body { background-color: lightgreen; }";
            var scssFile = new ProjectFile("style", "scss", $"{docPath}\\{sassFolder.Name}", scssContent);

            await fileWriter.Write(scssFile);
        }

        public static async Task WritePackageJson(string docPath)
        {
            var fileWriter = new ProjectFileWriter();
            var sb = new StringBuilder();

            string name = "ui-boilerplate";
            string description = "A boilerplate project for a gulp and SCSS powered static HTML page";
            string author = "Jamie Maguire";
            string gulpVersion = "4.0.0";
            string gulpSassVersion = "4.0.2";

            sb.AppendLine("{");
            sb.AppendLine($"  \"name\": \"{name}\",");
            sb.AppendLine("  \"version\": \"1.0.0\",");
            sb.AppendLine($"  \"description\": \"{description}\",");
            sb.AppendLine($"  \"author\": \"{ author}\",");
            sb.AppendLine("  \"license\": \"ISC\",");
            sb.AppendLine("  \"devDependencies\": {");
            sb.AppendLine($"    \"gulp\": \"{gulpVersion}\",");
            sb.AppendLine($"    \"gulp-sass\": \"{gulpSassVersion}\"");
            sb.AppendLine("  }");
            sb.AppendLine("}");

            var packageJsonFile = new ProjectFile("package", "json", docPath, sb.ToString());

            await fileWriter.Write(packageJsonFile);
        }
    }
}
