using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YG {
    public class UI_StatBar : MonoBehaviour
    {
        private Slider slider;
        // VARIABLE tO SCALE THE SIZE/CAPACITY OF THE BAR DEPENDING ON STATS
        // SECONDARY BAR BEHIND MAIN BAR, SHOWS HOW MUCH IS DEPLETED BY AN ACTION/DAMAGE

        protected virtual void Awake()
        {
            slider = GetComponent<Slider>();
        }

        public virtual void SetStat(int newVal)
        {
            slider.value = newVal;
        }

        public virtual void SetMaxStat(int maxVal)
        {
            slider.maxValue = maxVal;
            slider.value = maxVal;
        }
    }
}