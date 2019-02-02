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

            var fileWriter = new ProjectFileWriter();
            var folderWriter = new ProjectFolderWriter();
            var fileFactory = new ProjectFileFactory();
            var folderFactory = new ProjectFolderFactory(projectName);

            //await CreateProjectFolder(projectName, projectPath, folderWriter);

            //WriteGulpFileJs(innerProjectPath, fileWriter);

            //WriteIndexHtml(innerProjectPath, fileWriter);

            //WriteStyleScss(innerProjectPath, fileWriter, folderWriter);

            //CreateCssDirectory(innerProjectPath, folderWriter);

            //WritePackageJson(innerProjectPath, fileWriter);

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

        public static async Task CreateProjectFolder(string folderName, string docPath, ProjectFolderWriter folderWriter)
        {
            var folder = new ProjectFolder(folderName, docPath);
            await folderWriter.Write(folder);
        }

        public static async Task CreateCssDirectory(string docPath, ProjectFolderWriter folderWriter)
        {
            var cssFolder = new ProjectFolder("css", docPath);
            await folderWriter.Write(cssFolder);
        }

        public static async Task WriteGulpFileJs(string docPath, ProjectFileWriter fileWriter)
        {
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
            await fileWriter.Write(gulpFileJs);
        }

        public static async Task WriteIndexHtml(string docPath, ProjectFileWriter fileWriter)
        {
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
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            var indexHtmlFile = new ProjectFile("index", "html", docPath, sb.ToString());
            await fileWriter.Write(indexHtmlFile);
        }

        public static async Task WriteStyleScss(string docPath, ProjectFileWriter fileWriter, ProjectFolderWriter folderWriter)
        {
            // create the folder
            var sassFolder = new ProjectFolder("sass", docPath);
            await folderWriter.Write(sassFolder);

            // create the SCSS file with placeholder style
            string scssContent = "body { display: grid; }";

            var scssFile = new ProjectFile("style", "scss", $"{docPath}\\{sassFolder.Name}", scssContent);
            await fileWriter.Write(scssFile);
        }

        public static async Task WritePackageJson(string docPath, ProjectFileWriter fileWriter)
        {
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
