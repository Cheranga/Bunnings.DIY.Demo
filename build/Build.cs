using _build;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.ProjectModel;

[GitHubActions(
    "bunnings-diy",
    GitHubActionsImage.UbuntuLatest,
    AutoGenerate = true,
    On = new[] { GitHubActionsTrigger.PullRequest, GitHubActionsTrigger.Push },
    InvokedTargets = new[] { nameof(IBuildPipeline.Build) }
)]
sealed class Build : NukeBuild, IBuildPipeline
{
    public static int Main() => Execute<Build>(x => (x as IBuildPipeline).Build);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild
        ? Configuration.Debug
        : Configuration.Release;

    [Solution(GenerateProjects = true)]
    readonly Solution Solution;

    public Project PublishProject() => Solution.Bunnings_DIY_OrderProcessor;
}
