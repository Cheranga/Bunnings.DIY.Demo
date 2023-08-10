using _build;
using Nuke.Common;
using Nuke.Common.ProjectModel;

sealed class Build : NukeBuild, IDotnetBuildTasks, IFormatCodeTask
{
    public static int Main() => Execute<Build>(x => (x as IDotnetBuildTasks).Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild
        ? Configuration.Debug
        : Configuration.Release;

    [Solution(GenerateProjects = true)]
    readonly Solution Solution;
}
