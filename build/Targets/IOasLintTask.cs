using Nuke.Common;
using Nuke.Common.Tools.Npm;

namespace _build.Targets;

/// <summary>
/// Provides OAS linting via NPM spectral
/// </summary>
public interface IOasLintTask : INukeBuild
{
    Target LintOas =>
        _ =>
            _.TryDependsOn<INpmBuildTasks>(x => x.NpmInstall)
                .Executes(() =>
                {
                    NpmTasks.Npm("run lintOas");
                });
}
