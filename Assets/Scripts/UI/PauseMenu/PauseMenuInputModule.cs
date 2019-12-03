using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using InputManagement;
using System.Collections;

namespace UI.PauseMenu
{
    public class PauseMenuInputModule : BaseInputModule
    {
        public bool active;
        int canMove;

        public GameObject[] buttons;

        int selectedButton;
        public override void Process()
        {
            if (active)
            {
                bool up = KeyMapper.GetAxis("Vertical") > 0;
                bool down = KeyMapper.GetAxis("Vertical") < 0;

                bool select = KeyMapper.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Return);

                if (canMove >= 20)
                {
                    if (up)
                    {
                        selectedButton = (selectedButton + buttons.Length - 1) % buttons.Length;
                        canMove = 0;
                    }
                    if (down)
                    {
                        selectedButton = (selectedButton + buttons.Length + 1) % buttons.Length;
                        canMove = 0;
                    }
                }
                else
                {
                    canMove++;
                }

                eventSystem.SetSelectedGameObject(buttons[selectedButton]);

                if (select)
                {
                    buttons[selectedButton].GetComponent<Button>().onClick.Invoke();
                }
            }
        }

        public void Active()
        {
            active = true;
        }

        public void Inactive()
        {
            active = false;
        }
    }
}