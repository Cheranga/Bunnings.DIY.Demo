using System.Reflection.Metadata;
using Nuke.Common;
using Nuke.Common.IO;
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

    Target Publish =>
        _ =>
            _.Description("Publish")
                .DependsOn(FileCopy)
                .Produces(RootDirectory / "artifacts" / "funcapp")
                .Executes(() =>
                {
                    return DotNetTasks.DotNetPublish(
                        settings =>
                            settings
                                .SetConfiguration(Configuration.Release)
                                .SetOutput(RootDirectory / "artifacts" / "funcapp")
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
