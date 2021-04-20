#r "nuget: SimpleExec, 7.0.0"
#r "nuget: CommandLineParser, 2.8.0"
#r "nuget: Bullseye, 3.7.0"

using System.IO;
using CommandLine;
using static SimpleExec.Command;
using static Bullseye.Targets;
using static System.Console;

////////////////////////////////////////////////////////////////////////////////
// PREPARATION
////////////////////////////////////////////////////////////////////////////////

var artifactsDir = "artifacts";
var publishDir = $"{artifactsDir}/publish_winx86";
var msbuildPath = @"C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\msbuild.exe";


////////////////////////////////////////////////////////////////////////////////
// OPTIONS
////////////////////////////////////////////////////////////////////////////////

public sealed class Options
{
    [Option('t', "target", Required = false, Default = "build-msi")]
    public string Target { get; set; }
}

Options options;

////////////////////////////////////////////////////////////////////////////////
// TARGETS
////////////////////////////////////////////////////////////////////////////////

Target("clean-solution", () => {
    if( Directory.Exists(artifactsDir))
        Directory.Delete(artifactsDir, recursive: true);
});

Target("build-solution", DependsOn("clean-solution"), () => {
    Run("dotnet", $@"publish ..\ -c Release -r win-x86 -o {publishDir}");
});

Target("run-unit-tests", DependsOn("build-solution"), () => {
    Run("dotnet", $"test ..\\ -r artifacts --logger:\"xunit\" --no-build");
});

Target("build-msi", DependsOn("run-unit-tests"), () =>{
    Run(msbuildPath, @"..\InstallerWixExample\");
});

////////////////////////////////////////////////////////////////////////////////
// EXECUTION
////////////////////////////////////////////////////////////////////////////////

Parser.Default.ParseArguments<Options>(Args).WithParsed<Options>(o =>
{
    options = o;
    WriteLine("Target: " + options.Target);

    RunTargetsAndExit(new List<string>(){options.Target});
});