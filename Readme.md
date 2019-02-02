# Buildify
Buildify is a cross-platform command line tool for quickly spinning up simple static front-end project templates.

Templates are loaded with Gulp, SCSS compilation and Autoprefixer (With the CSS Grid polyfill enabled). Everything you need to start building HTML and CSS only UI components.

There is no JavaScript included in the template because there are already a ton of CLIs out there for various frameworks. 

## To Run  (Windows):
1. CD into the project via the command-line
2. Run a dotnet publish command that targets your desired OS. Such as:

```
dotnet publish -c Release -r win10-x64
```

3. Add the exe path to your PATH system environment variable.
4. Run from any directory in the command line:
```
C:\code> buildify awesome-project
```

5. CD into the newly generated project
6. Run the following commands:
```
npm install
gulp
```
7. Begin coding! As you make changes to main.scss Gulp will watch for changes and run the appropriate tasks.