#r "nuget: SimpleExec, 7.0.0"
#r "nuget: Bullseye, 3.7.0"
using static SimpleExec.Command;
using static Bullseye.Targets;
using static System.Console;

var publishFilePath = "publish_winx86";

Target("clean-build-folder", () => {
    if( !Directory.Exists(publishFilePath))
        return;
    Directory.Delete(publishFilePath, true);
});

Target("publish", DependsOn("clean-build-folder"), () => Run("dotnet", "publish ../ -r win-x86 -o " + publishFilePath));

RunTargetsAndExit(new List<string>(){"clean-build-folder", "publish"});

WriteLine("Completed!");