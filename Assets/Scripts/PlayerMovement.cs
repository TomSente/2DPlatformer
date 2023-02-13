using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool isFacingRight = true;
    private bool isWallJumping;
    private float wallJumpingDirection;
    [SerializeField] private float wallJumpingTime;
    [SerializeField] private Vector2 wallJumpingPower;
    private bool canDoubleJump;
    private bool canJump;
    private Rigidbody2D rb;
    private Animator animator;
    private float dirX;
    private SpriteRenderer sprite;

    private MovementState state;

    private bool isWallSliding;
    [SerializeField] private float wallSlidingSpeed =2f;
    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private float movementSpeed =7f;
    [SerializeField] private float jumpForce =14f;
    [SerializeField] private LayerMask jumpableGround;

    private BoxCollider2D coll;

    private enum MovementState {idle,run,jump,doublejump,fall,shoot,wallsliding}

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        ResetJumps();
        // Debug.Log("iswalljump"+isWallJumping);
        // Debug.Log("doublejump"+canDoubleJump);
        // Debug.Log("jump"+canJump);
        dirX = Input.GetAxisRaw("Horizontal");
        if(!isWallJumping)
        {
            MoveLeftOrRight();
            Jump();
        }
        if(!isWallSliding)
        {
            DoubleJump();
        }
        Flip();
        WallSlide();
        WallJump();
        UpdateAnimationState();
    }

    private void MoveLeftOrRight()
    {
        rb.velocity = new Vector2(dirX * movementSpeed,rb.velocity.y);
    }

    private void Jump()
    {

         if(Input.GetButtonDown("Jump") && canJump)
            {
                jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x,jumpForce);
                canJump=false;
            }
    }

    private void DoubleJump()
    {

        if(!IsGrounded()&&Input.GetButtonDown("Jump")&&!isWallSliding&&canDoubleJump)
            {
                jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x,jumpForce);
                canDoubleJump=false;
            }
    }

    private void ResetJumps()
    {
        if(IsGrounded())
        {
            canJump = true;
            canDoubleJump = true;
        }
    }

    // Checks if there is a wall either right or left next to Collider
    private bool IsWalled()
    {
        Vector2 facingVector;
        if(isFacingRight)
        {
             facingVector = Vector2.right;
        }
        else
        {
            facingVector = Vector2.left;
        }
        
        return Physics2D.BoxCast(coll.bounds.center,coll.bounds.size,0f,facingVector,0.1f,jumpableGround);
    }

    //Checks if can WallSlide and gives new velocity to rb with slowdown on y-axis
    private void WallSlide()
    {
        if(IsWalled() && !IsGrounded() && dirX!=0f)
        {
            isWallSliding=true;
            rb.velocity = new Vector2(rb.velocity.x,Mathf.Clamp(rb.velocity.y,-wallSlidingSpeed,float.MaxValue));
        }
        else
        {
            isWallSliding=false;
        }
    }

    // 
    private void WallJump()
    {
        if(isWallSliding)
        {
            isWallJumping = false;
            if(isFacingRight)
            {
                wallJumpingDirection =-transform.localScale.x;
            }
            else
            {
                wallJumpingDirection =transform.localScale.x;
            }
            CancelInvoke(nameof(StopWallJumping));

        }
        if(Input.GetButtonDown("Jump") && isWallSliding)
        {
            isWallJumping=true;
            rb.velocity = new Vector2(wallJumpingDirection*wallJumpingPower.x,wallJumpingPower.y);
            Invoke(nameof(StopWallJumping),wallJumpingTime);

        }
    }

    private void StopWallJumping()
    {
        isWallJumping=false;
    }


    private void Flip()
    {
        if((isFacingRight && rb.velocity.x <0f)|| (!isFacingRight && rb.velocity.x > 0f))
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0,180,0);
        }
    }

    private bool IsGrounded()
    {
        Debug.DrawRay(transform.position,Vector2.down,Color.red);
        // return Physics2D.BoxCast(coll.bounds.center,coll.bounds.size,0f,Vector2.down,0.1f,jumpableGround);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, jumpableGround);
        if (hit.collider != null) 
        {
            return true;
        }
        return false;
    }

    private void UpdateAnimationState()
    {
        if(dirX > 0f)
        {
            state = MovementState.run;
            
        }
        else if(dirX < 0f)
        {
            state = MovementState.run;
            
        }
        else
        {
            state = MovementState.idle;
        }

        if(rb.velocity.y > 0.1f)
        {
            if(canDoubleJump||isWallJumping)
            {
                state=MovementState.jump;
            }
            else
            {
                state = MovementState.doublejump;
            }
        }
        else if (rb.velocity.y < -0.1f)
        {
            if(isWallSliding)
            {
                state = MovementState.wallsliding;
            }
            else
            {
                state = MovementState.fall;
            }
        }
        animator.SetInteger("State",(int)state);
    }
}
