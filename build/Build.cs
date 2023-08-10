using _build;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.ProjectModel;

[GitHubActions(
    "bunnings-diy",
    GitHubActionsImage.UbuntuLatest,
    On = new[] { GitHubActionsTrigger.PullRequest },
    InvokedTargets = new[] { nameof(IDotnetBuildTasks.Publish) }
)]
sealed class Build : NukeBuild, IDotnetBuildTasks, IFormatCodeTasks, INpmBuildTasks, IOasLintTask
{
    public static int Main() => Execute<Build>(x => (x as IDotnetBuildTasks).Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild
        ? Configuration.Debug 
        : Configuration.Release;

    [Solution(GenerateProjects = true)]
    readonly Solution Solution;
}
