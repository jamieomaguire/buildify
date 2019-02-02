using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using buildify.Models;

namespace buildify.Factories
{
    /// <summary>
    /// Responsible for creating new instances of <see cref="ProjectFile"/>.
    /// </summary>
    public class ProjectFileFactory
    {
        public async Task<ProjectFile> IndexHtml(string path)
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

            return new ProjectFile("index", "html", path, sb.ToString());
        }

        public async Task<ProjectFile> MainScss(string path)
        {
            string scss = "body { display: grid; }";

            return new ProjectFile("main", "scss", path, scss);
        }

        public async Task<ProjectFile> PackageJson(string path)
        {
            var sb = new StringBuilder();

            string name = "buildify-ui-boilerplate";
            string description = "A boilerplate project for a gulp and PostCSS powered static HTML page";
            string author = "Jamie Maguire";
            string gulpVersion = "4.0.0";
            string gulpSassVersion = "4.0.2";
            string autoprefixerVersion = "9.4.7";
            string gulpPostCssVersion = "8.0.0";

            sb.AppendLine("{");
            sb.AppendLine($"  \"name\": \"{name}\",");
            sb.AppendLine("  \"version\": \"1.0.0\",");
            sb.AppendLine($"  \"description\": \"{description}\",");
            sb.AppendLine($"  \"author\": \"{ author}\",");
            sb.AppendLine("  \"license\": \"ISC\",");
            sb.AppendLine("  \"devDependencies\": {");
            sb.AppendLine($"    \"gulp\": \"{gulpVersion}\",");
            sb.AppendLine($"    \"gulp-sass\": \"{gulpSassVersion}\",");
            sb.AppendLine($"    \"gulp-postcss\": \"{gulpPostCssVersion}\",");
            sb.AppendLine($"    \"autoprefixer\": \"{autoprefixerVersion}\"");
            sb.AppendLine("  }");
            sb.AppendLine("}");

            return new ProjectFile("package", "json", path, sb.ToString());
        }

        public async Task<ProjectFile> GulpFileJs(string path)
        {
            var sb = new StringBuilder();

            sb.AppendLine("var gulp = require('gulp'),");
            sb.AppendLine("    postcss = require('gulp-postcss'),");
            sb.AppendLine("    sass = require('gulp-sass');");
            sb.AppendLine();
            sb.AppendLine("// Default task - runs SCSS compilation and then autoprefixer");
            sb.AppendLine("gulp.task('default', function() {");
            sb.AppendLine("    gulp.watch('dev/**/*.scss', gulp.series('scss'))");
            sb.AppendLine("    gulp.watch('dev/**/*.css', gulp.series('styles'))");
            sb.AppendLine("});");
            sb.AppendLine("");
            sb.AppendLine("// Compiles SCSS to CSS");
            sb.AppendLine("gulp.task('scss', function() {");
            sb.AppendLine("    gulp.src('dev/**/*.scss')");
            sb.AppendLine("        .pipe(sass().on('error', sass.logError))");
            sb.AppendLine("        .pipe(gulp.dest('./dev/scss-output/'))");
            sb.AppendLine("});");
            sb.AppendLine();
            sb.AppendLine("// Array to store PostCSS plugins");
            sb.AppendLine("var processors = [");
            sb.AppendLine("    require('autoprefixer')({ browsers: ['last 4 versions'], grid: 'autoplace' })");
            sb.AppendLine("];");
            sb.AppendLine();
            sb.AppendLine("// Apply processors to the CSS (Autoprefixer etc)");
            sb.AppendLine("gulp.task('styles', function() {");
            sb.AppendLine("    return gulp.src('./dev/scss-output/main.css')");
            sb.AppendLine("        .pipe(postcss(processors))");
            sb.AppendLine("		   .pipe(gulp.dest('prod'));");
            sb.AppendLine("});");

            return new ProjectFile("Gulpfile", "js", path, sb.ToString());
        }
    }
}
