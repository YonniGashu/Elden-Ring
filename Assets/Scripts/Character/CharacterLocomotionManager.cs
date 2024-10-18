using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace YG
{
    public class CharacterLocomotionManager : MonoBehaviour
    {
        CharacterManager character;

        [Header("Ground & Jumping Check")]
        [SerializeField] protected float gravityForce = -5.55f;
        [SerializeField] LayerMask groundLayer;
        [SerializeField] float groundCheckSphereRadius = 0.3f;
        [SerializeField] protected Vector3 yVelocity; // THE FORCE THE CHAR IS PULLED UP OR DOWN (FALL VS JUMP)
        [SerializeField] protected float groundedYVelocity = -20; // THE FORCE THAT IS APPLIED DOWNWARD ON CHAR TO KEEP THEM GROUNDED
        [SerializeField] protected float fallStartVelocity = -5; // THE FORCE THAT CHAR BEGINS TO FALL AT, RISES AS THEY FALL
        protected bool fallingVelocityHasBeenSet = false;
        protected float inAirTimer = 0;


        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        protected virtual void Update()
        {
            HandleGroundCheck();
            if (character.isGrounded) {
                // IF WE'RE NOT JUMPING, THEN APPLY JUST THE GROUNDED VELOCITY  
                if (yVelocity.y < 0) {
                    inAirTimer = 0;
                    fallingVelocityHasBeenSet = false;
                    yVelocity.y = groundedYVelocity;
                }
            } else {
                    // IF WE'RE NOT JUMPING, AND OUR FALLING VELOCITY HASNT BEEN SET YET
                    if (!character.isJumping && !fallingVelocityHasBeenSet) {
                        fallingVelocityHasBeenSet = true;
                        yVelocity.y = fallStartVelocity;
                    }
                    inAirTimer += Time.deltaTime;
                    character.animator.SetFloat("InAirTimer", inAirTimer);
                    yVelocity.y += gravityForce  * Time.deltaTime;
                }
                
                character.characterController.Move(yVelocity * Time.deltaTime);
        }
        
        protected void HandleGroundCheck() {
            character.isGrounded = Physics.CheckSphere(character.transform.position, groundCheckSphereRadius, groundLayer);
        }

        // DRAWS THE GROUND SPHERE
        protected void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(character.transform.position, groundCheckSphereRadius);
        }
    }
}