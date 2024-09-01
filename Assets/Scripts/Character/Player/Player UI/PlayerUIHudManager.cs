using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YG {
    public class PlayerUIHudManager : MonoBehaviour
    {
        [SerializeField] UI_StatBar staminaBar;

        public void SetNewStaminaValue(float oldVal, float newVal)
        {
            staminaBar.SetStat(Mathf.RoundToInt(newVal));
        }

        public void SetMaxStaminaValue(float maxVal)
        {
            staminaBar.SetMaxStat(Mathf.RoundToInt(maxVal));
        }
    }
}