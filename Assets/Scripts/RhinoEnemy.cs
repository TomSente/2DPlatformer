using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoEnemy : MonoBehaviour
{
    private float health=100;
    Animator animator;
    [SerializeField] private float chargeSpeed;

    [SerializeField] private LayerMask Ground;


    private Rigidbody2D rigidbody2D;

    private Collider2D collider;

    private enum MovementState {idle,charge,hit}

    private MovementState state;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(agroPlayer())
        {
            Charge();
        }
        UpdateAnimationState();
    }
    public void Charge()
    {
        rigidbody2D.velocity=-transform.right*chargeSpeed;
    }
    

    private bool agroPlayer()
    {
        Debug.DrawRay(transform.position,new Vector2(-10f,0),Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 10f, Ground);
        if (hit.collider.gameObject.CompareTag("Player")) 
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("trigger");
            if(other.gameObject.CompareTag("Surface"))
            {
                rigidbody2D.velocity = new Vector2(4,4);
                animator.SetTrigger("WallHit");
            }
            if(other.gameObject.CompareTag("Player"))
            {
                animator.SetTrigger("PlayerHit");
            }
    }

     private void UpdateAnimationState()
    {
        if(rigidbody2D.velocity.x!=0)
        {
            state = MovementState.charge;
        }
        else
        {
            state = MovementState.idle;
        }
        animator.SetInteger("State",(int)state);

    }


}
