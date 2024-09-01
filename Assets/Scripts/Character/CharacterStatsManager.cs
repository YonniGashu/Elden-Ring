using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YG {
    public class CharacterStatsManager : MonoBehaviour
    {
        public int CalculateStaminaBasedOnEnduranceLevel(int enduranceLvl)
        {
            int stamina = 0;

            // Create an equation to determine stamina

            stamina = enduranceLvl * 10;

            return stamina;
        }
    }
}