using UnityEngine;
using Management;
using System.Collections;
public class DebugManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Unpause());
    }

    IEnumerator Unpause()
    {
        for (int _ = 0; _ < 10; _++)
        {
            yield return null;
        }
        GameManagerScript.gameManager.ToggleGamePaused();
    }


}