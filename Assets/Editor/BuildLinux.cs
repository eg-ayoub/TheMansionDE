using UnityEditor;
using UnityEngine;
public class BuildLinux : BuildScenes
{
    static void build()
    {
        string pathToDeploy = "Builds/" + Application.version + "/Linux/TheMansionDE.x86_64";

        BuildPipeline.BuildPlayer(scenes, pathToDeploy, BuildTarget.StandaloneLinux64, BuildOptions.None);
    }
}