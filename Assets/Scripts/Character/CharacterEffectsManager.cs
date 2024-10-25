using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YG {
    public class CharacterEffectsManager : MonoBehaviour
    {
        // PROCESSING INSTANT EFFECTS ( DAMAGE, HEALING )

        // PROCESS TIMED EFFECTS ( POISON, BUILD UPS )

        // PROCESS STATIC EFFECTS ( BUFFS FROM ITEMS/TALISMANS, ETC )
        CharacterManager character;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        public virtual void ProcessInstantEffect(InstantCharacterEffect effect) {
            effect.ProcessEffect(character);
        }
    }
}