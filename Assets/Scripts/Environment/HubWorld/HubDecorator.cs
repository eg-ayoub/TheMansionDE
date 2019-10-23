using UnityEngine;
using System.Collections;
using Management.Serialization;
using UI.HUD;

namespace Environment.HubWorld
{
    public class HubDecorator : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(Decorate());
        }

        IEnumerator Decorate()
        {
            // * get times
            int[] times = SaveManager.FetchTimes();
            yield return null;

            // * decorate doors
            bool first = true;
            for (int i = 0; i < Constants.CHECKPOINT_COUNT; i++)
            {
                HubDoor decorator = transform.GetChild(0).GetChild(i).GetComponent<HubDoor>();
                if (times[i] == -1 && first)
                {
                    decorator.accessible = true;
                    decorator.finished = false;
                    first = false;
                }
                else if (times[i] == -1)
                {
                    decorator.accessible = false;
                }
                else
                {
                    decorator.accessible = true;
                    decorator.finished = true;
                }
                decorator.time = times[i];
                decorator.Init();
            }

            // * disable hud features that are not needed in HUB world
            HudScript.hud.EnterHub();

        }
    }
}