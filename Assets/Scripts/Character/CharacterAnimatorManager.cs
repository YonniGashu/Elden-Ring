using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YG
{
    public class CharacterAnimatorManager : MonoBehaviour
    {
        CharacterManager character;

        float vertical;
        float horizontal;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();   
        }

        public void UpdateAnimatorMovementParameters(float horizontalValue, float verticalValue)
        {
            character.animator.SetFloat("Horizontal", horizontalValue, 0.1f, Time.deltaTime);
            character.animator.SetFloat("Vertical", verticalValue, 0.1f, Time.deltaTime);
        }

        public virtual void PlayActionAnimation(
            string animationName,
            bool isPerformingAction,
            bool applyRootMotion = true,
            bool canRotate = false,
            bool canMove = false)
        {
            character.animator.applyRootMotion = applyRootMotion;
            character.animator.CrossFade(animationName, 0.2f);
            // CAN BE USED TO STOP CHARACTER FROM ATTEMPTING ANOTHER ACTION
            character.isPerformingAction = isPerformingAction;
            character.canRotate = canRotate;
            character.canMove = canMove;
        }
    }
}
