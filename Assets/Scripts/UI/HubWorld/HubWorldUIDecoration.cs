using UnityEngine;
using UnityEngine.UI;
using UI.HUD;
namespace UI.HubWorld
{
    public class HubWorldUIDecoration : MonoBehaviour
    {
        public GameObject[] doors;

        private void Start()
        {
            doors = new GameObject[]{
            transform.GetChild(0).gameObject,
            transform.GetChild(1).gameObject,
            transform.GetChild(2).gameObject
        };
        }

        public void DecorateDoor(int index, int time, int collectibles)
        {
            doors[index].transform.GetChild(0).GetComponent<Text>().text = TimerDisplay.HumanTime(time);
            doors[index].transform.GetChild(1).GetComponent<Text>().text = "" + collectibles;
        }

        public void RemoveDecoration(int index)
        {
            doors[index].SetActive(false);
        }
    }
}