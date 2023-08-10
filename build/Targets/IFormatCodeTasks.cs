using Nuke.Common;
using Nuke.Common.Tools.DotNet;

namespace _build.Targets;

/// <summary>
/// Provides a task for formatting the C# code using `CSharpier`
/// </summary>
public interface IFormatCodeTasks : INukeBuild
{
    Target Format =>
        _ =>
            _.TryDependsOn<IDotnetBuildTasks>(x => x.ToolRestore)
                .Executes(() =>
                {
                    DotNetTasks.DotNet("csharpier .");
                });

    Target CheckFormat =>
        _ =>
            _.TryDependsOn<IDotnetBuildTasks>(x => x.ToolRestore)
                .Executes(() =>
                {
                    DotNetTasks.DotNet("csharpier . --check");
                });
}
