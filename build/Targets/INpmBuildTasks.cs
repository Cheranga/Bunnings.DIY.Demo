using Nuke.Common;
using Nuke.Common.Tools.Npm;

namespace _build.Targets;

/// <summary>
/// Tasks relate to `Npm`
/// </summary>
public interface INpmBuildTasks : INukeBuild
{
    Target NpmInstall =>
        _ =>
            _.Executes(() =>
            {
                NpmTasks.NpmInstall();
            });
}
