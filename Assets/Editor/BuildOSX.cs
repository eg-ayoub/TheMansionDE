using UnityEditor;
using UnityEngine;

public class BuildOSX : BuildScenes
{
    static void build()
    {
        string pathToDeploy = "Builds/" + Application.version + "/OSX/TheMansionDE.app";

        BuildPipeline.BuildPlayer(scenes, pathToDeploy, BuildTarget.StandaloneOSX, BuildOptions.None);
    }
}