using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YG {
    public class CharacterStatsManager : MonoBehaviour
    {
        CharacterManager character;

        [Header("Stamina Regeneration")]
        [SerializeField] float staminaRegernationAmount = 2;
        private float staminaRegenerationTimer = 0;
        private float staminaTickTimer = 0;
        [SerializeField] float staminaRegenerationDelay = 2;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }


        public int CalculateStaminaBasedOnEnduranceLevel(int enduranceLvl)
        {
            int stamina = 0;

            // Create an equation to determine stamina

            stamina = enduranceLvl * 10;

            return stamina;
        }

        public virtual void RegenerateStamina()
        {
            if (!character.IsOwner) { return; }

            if (character.characterNetworkManager.isSprinting.Value) { return; }

            if (character.isPerformingAction) { return; }

            staminaRegenerationTimer += Time.deltaTime;

            if (staminaRegenerationTimer >= staminaRegenerationDelay)
            {
                if (character.characterNetworkManager.currentStamina.Value < character.characterNetworkManager.maxStamina.Value)
                {
                    staminaTickTimer += Time.deltaTime;

                    if (staminaTickTimer >= 0.1)
                    {
                        staminaTickTimer = 0;
                        character.characterNetworkManager.currentStamina.Value += staminaRegernationAmount;
                    }
                }
            }
        }

        public virtual void ResetStaminaRegenTimer(float prevStamAmount, float currentStamAmount)
        {
            if (currentStamAmount < prevStamAmount)
            {
                staminaRegenerationTimer = 0;
            }
        }
    }
}