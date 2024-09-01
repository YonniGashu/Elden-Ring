using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YG {
    public class PlayerUIHudManager : MonoBehaviour
    {
        [SerializeField] UI_StatBar staminaBar;

        public void SetNewStaminaValue(int oldVal, int newVal)
        {
            staminaBar.SetStat(newVal);
        }

        public void SetMaxStaminaValue(int maxVal)
        {
            staminaBar.SetMaxStat(maxVal);
        }
    }
}