using System;
using Nuke.Common;

namespace _build;

/// <summary>
/// Contains the tasks which will be carried out in a build pipeline
/// </summary>
public interface IBuildPipeline : IDotnetBuildTasks, IFormatCodeTasks, INpmBuildTasks, IOasLintTask
{
    Target Build =>
        _ =>
            _.Description("Build")
                .DependsOn(CheckFormat)
                .DependsOn(LintOas)
                .DependsOn(Test)
                .Executes(() =>
                {
                    Console.WriteLine("Build Pipeline");
                });
}
