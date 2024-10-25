using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YG {
    public class WorldCharacterEffectsManager : MonoBehaviour
    {
        public static WorldCharacterEffectsManager instance;

        [SerializeField] List<InstantCharacterEffect> instantEffects;

        void Awake()
        {
            if (instance == null) 
            {
                instance = this;
            } 
            else 
            {
                Destroy(gameObject);
            }
            GenerateEffectsIDs();
        }
    
        private void GenerateEffectsIDs() 
        {
            for (int i = 0; i < instantEffects.Count; i++) 
            {
                instantEffects[i].instantEffectID = i;
            }
        }
    }
}