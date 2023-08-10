using Nuke.Common;
using Nuke.Common.Tools.DotNet;

/// <summary>
/// Defines the standard `dotnet` tasks
/// </summary>
public interface IDotnetBuildTasks : INukeBuild
{
    Target Clean => _ => _.Executes(() => DotNetTasks.DotNetClean());

    Target Restore => _ => _.Executes(() => DotNetTasks.DotNetRestore());

    Target ToolRestore => _ => _.Executes(() => DotNetTasks.DotNetToolRestore());

    Target Compile => _ => _.Executes(() => DotNetTasks.DotNetBuild());

    Target Test => _ => _.Executes(() => DotNetTasks.DotNetTest());
}
