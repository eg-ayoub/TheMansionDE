using UnityEditor;
using UnityEngine;
public class BuildWindows : BuildScenes
{
    static void build()
    {
        string pathToDeploy = "Builds/" + Application.version + "/Windows/TheMansionDE.exe";

        BuildPipeline.BuildPlayer(scenes, pathToDeploy, BuildTarget.StandaloneWindows64, BuildOptions.None);
    }
}