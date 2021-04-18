#r "nuget: SimpleExec, 7.0.0"
using static SimpleExec.Command;
using static System.Console;

//Run("dotnet", "publish ../ -o publish");
Run("dotnet", "publish ../ -r win-x86 -o publish_winx86");



WriteLine("Completed!");