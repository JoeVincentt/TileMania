using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
      //Config
      [SerializeField] float runSpeed = 5f;
      [SerializeField] float jumpSpeed = 5f;
      [SerializeField] float climbSpeed = 5f;
      [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);

      //State
      bool isAlive = true;

      //Cached component reference
      Animator myAnimator;
      Rigidbody2D myRigidBody;
      Collider2D myBodyCollider2D;
      Collider2D myFeetCollider2D;
      float gravityScaleAtStart;

      private void Start()
      {
            myRigidBody = GetComponent<Rigidbody2D>();
            myAnimator = GetComponent<Animator>();
            myBodyCollider2D = GetComponent<CapsuleCollider2D>();
            myFeetCollider2D = GetComponent<BoxCollider2D>();
            gravityScaleAtStart = myRigidBody.gravityScale;
      }
      private void Update()
      {
            if (!isAlive) { return; }

            Run();
            Jump();
            ClimbLadder();
            FlipSprite();
            Die();
      }

      private void Run()
      {
            float controlThrow = Input.GetAxis("Horizontal"); //value is between -1 to +1
            Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
            myRigidBody.velocity = playerVelocity;

            bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
            myAnimator.SetBool("Running", playerHasHorizontalSpeed);
      }
      private void Jump()
      {
            if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

            if (Input.GetButtonDown("Jump"))
            {
                  Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
                  myRigidBody.velocity += jumpVelocityToAdd;
            }
      }
      private void ClimbLadder()
      {
            if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
            {
                  myAnimator.SetBool("Climbing", false);
                  myRigidBody.gravityScale = gravityScaleAtStart;
                  return;
            }

            //climbing on ladder
            float controlThrow = Input.GetAxis("Vertical");
            Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
            myRigidBody.velocity = climbVelocity;
            myRigidBody.gravityScale = 0f;

            //Set climbing animation
            bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
            myAnimator.SetBool("Climbing", playerHasVerticalSpeed);





      }
      private void FlipSprite()
      {
            bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

            if (playerHasHorizontalSpeed)
            {
                  transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1);
            }
      }
      private void Die()
      {
            if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
            {
                  isAlive = false;
                  myAnimator.SetTrigger("Dying");
                  GetComponent<Rigidbody2D>().velocity = deathKick;
                  FindObjectOfType<GameSession>().ProcessPlayerDeath();
            }
      }






}