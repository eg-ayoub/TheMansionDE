using UnityEngine;

namespace UI.HUD
{
    public class HPDisplay : MonoBehaviour
    {
        public ImageIndicator[] indicators = new ImageIndicator[Constants.PLAYER_MAX_HP];
        private void Start()
        {
            for (int i = 0; i < Constants.PLAYER_MAX_HP; i++)
            {
                indicators[i] = transform.GetChild(i).GetComponent<ImageIndicator>();
            }
        }
        public void UpdateHP(int HP)
        {
            for (int i = 0; i < Constants.PLAYER_MAX_HP; i++)
            {
                if (HP >= i + 1)
                {
                    indicators[i].On();
                }
                else
                {
                    indicators[i].Off();
                }
            }
        }
    }
}