using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public class BuildPlayer : MonoBehaviour
{
    [MenuItem("Build/Build Windows")]
    public static void BuildForWindows()
    {
        BuildPlayerOptions build = new BuildPlayerOptions();
        build.scenes = new[] {"Assets/Scenes/Menu", "Assets/Scenes/ShopScene" };
        build.locationPathName = "WinBuild";
        build.target = BuildTarget.StandaloneWindows;
        build.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(build);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
            Debug.Log("Build succeeded " + summary.totalSize + " bytes");

        if (summary.result == BuildResult.Failed)
            Debug.Log("Build failed");
    }

    /*[MenuItem("Build/iOS")]
    public static void BuildForAndroid()
    {

    }*/
}
