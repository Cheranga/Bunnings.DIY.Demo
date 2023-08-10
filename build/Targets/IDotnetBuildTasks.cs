using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;

namespace _build.Targets;

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

    Project PublishProject();

    Target Publish =>
        _ =>
            _.Description("Publish")
                .DependsOn(FileCopy)
                .Produces(RootDirectory / "artifacts" / "funcapp")
                .Executes(() =>
                {
                    var version = GitHubActions.Instance.GitHubEvent.TryGetValue(
                        "tag_value",
                        out var tag
                    )
                        ? tag.ToString()
                        : "funcapp";

                    return DotNetTasks.DotNetPublish(
                        settings =>
                            settings
                                .SetConfiguration(Configuration.Release)
                                .SetProject(PublishProject())
                                .SetOutput(RootDirectory / "artifacts" / version)
                    );
                });

    Target FileCopy =>
        _ =>
            _.Description("Copy Templates")
                .Produces(RootDirectory / "artifacts" / "deploy")
                .Executes(() =>
                {
                    var templatePath = RootDirectory / ".github" / "templates";
                    var targetPath = RootDirectory / "artifacts" / "deploy";
                    FileSystemTasks.CopyDirectoryRecursively(templatePath, targetPath);
                });
}
