using UnityEngine;
using InputManagement;
using Management;
using UnityEngine.Experimental.Rendering.Universal;
using Player;

namespace Environment.HubWorld
{
    public class HubDoor : GameplayObject
    {
        public bool mansionOfMadness;

        public Sprite accessibleSprite;
        public Sprite inaccessibleSprite;
        public Sprite finishedSprite;
        SpriteRenderer sprite;
        SpriteRenderer indicator;
        Light2D mLight;


        public bool accessible;

        public bool finished;

        public int time;

        public int targetLevel;

        public void Start()
        {
            sprite = GetComponent<SpriteRenderer>();
            mLight = GetComponent<Light2D>();
            indicator = transform.GetChild(0).GetComponentInChildren<SpriteRenderer>();
        }

        public void Init()
        {
            if (accessible)
            {
                sprite.sprite = accessibleSprite;
                if (finished)
                {
                    sprite.sprite = finishedSprite;
                }
            }
            else
            {
                sprite.sprite = inaccessibleSprite;
                mLight.enabled = false;
            }

        }
        private void Update()
        {
            if (accessible)
            {
                float distance = transform.position.x - PlayerInstanciationScript.playerTransform.position.x;
                distance = Mathf.Abs(distance);
                distance = Mathf.Clamp(distance, 100, Constants.INDICATOR_THRESHOLD_DISTANCE);
                distance = Mathf.InverseLerp(100, Constants.INDICATOR_THRESHOLD_DISTANCE, distance);
                indicator.color = new Color(1, 1, 1, 1 - distance);
            }
            else
            {
                indicator.color = Color.clear;
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !paused && accessible)
            {
                if (KeyMapper.GetButtonDown("Start"))
                {
                    GameManagerScript.gameManager.Enter(targetLevel, mansionOfMadness);
                }
            }
        }
    }
}