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

var publishPath = "publish_winx86";

////////////////////////////////////////////////////////////////////////////////
// OPTIONS
////////////////////////////////////////////////////////////////////////////////

public sealed class Options
{
    [Option('e', "environment", Required = false, Default = "Development")]
    public string Environment { get; set; }

    [Option('t', "target", Required = false, Default = "build-solution")]
    public string Target { get; set; }
}

Options options;

////////////////////////////////////////////////////////////////////////////////
// TARGETS
////////////////////////////////////////////////////////////////////////////////

Target("clean-solution", () => {
    if( !Directory.Exists(publishPath))
        return;
    Directory.Delete(publishPath, true);
});

Target("build-solution", DependsOn("clean-solution"), () =>{
    Run("dotnet", $"publish ../ -c Release -r win-x86 -o {publishPath} /p:EnvironmentName={options.Environment}");
});

////////////////////////////////////////////////////////////////////////////////
// EXECUTION
////////////////////////////////////////////////////////////////////////////////

Parser.Default.ParseArguments<Options>(Args).WithParsed<Options>(o =>
{
    options = o;
    WriteLine("Environment: " + options.Environment);
    WriteLine("Target: " + options.Target);

    RunTargetsAndExit(new List<string>(){options.Target});
});