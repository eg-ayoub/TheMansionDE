using UnityEngine;
using System.Collections;
using Management.Serialization;
using UI.HUD;
using UI.HubWorld;

namespace Environment.HubWorld
{
    public class HubDecorator : MonoBehaviour
    {

        HubWorldUIDecoration uIDecoration;

        private void Start()
        {
            uIDecoration = FindObjectOfType<HubWorldUIDecoration>();
            StartCoroutine(Decorate());
        }

        IEnumerator Decorate()
        {
            // * get times
            SaveBlob blob = SaveManager.FetchTimes();
            int[] times = blob.SaveTimes;
            int[] collectibles = blob.SaveCollectibles;
            yield return null;

            // * decorate UI under doors

            for (int i = 0; i < times.Length; i++)
            {
                if (times[i] != -1)
                {
                    uIDecoration.DecorateDoor(i, times[i], collectibles[i]);
                }
                else
                {
                    uIDecoration.RemoveDecoration(i);
                }
            }

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