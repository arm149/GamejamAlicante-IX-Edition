using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {
    //Force of the jump
    public float JumpForce = 10;

    //Is player jumping?
    bool jumping = false;

    //Check if we have landed
    bool checkLanding = true;

    //RigidBody2D component of the player
    private Rigidbody2D rigidbody;

    //CircleCollider2d component of the player
    private CircleCollider2D circlecollider;

    //Health of the player
    private int CurrentHealth;

    //Maximum health of the player
    public int MaximumHealth = 3;

    //Animator of the player
    public Animator animator;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        circlecollider = GetComponent<CircleCollider2D>();
        CurrentHealth = MaximumHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump"))
        {
            jumping = true;
            checkLanding = false;
            animator.SetBool("IsJumping", true);
        }
	}

    //Check if we have landed in the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check if we have jumped (otherwise, we are colliding because we are already on the ground
        if(!checkLanding)
        {
            checkLanding = true;
            animator.SetBool("IsJumping", false);
        }
    }

    private void FixedUpdate()
    {
        //Check if we are already jumping
        if(jumping)
        {
            Jump();
            jumping = false;
        }
    }

    //Jump Action
    void Jump()
    {
        //Add a vertical force to the player's rigidbody
        rigidbody.AddForce(new Vector2(0f, JumpForce));
    }

    //Function to make damage to the player
    void ReceiveDamage(int dmg)
    {
        //Check if the damage received is positive
        if(dmg > 0)
        {
            //Make damage to the player
            CurrentHealth -= dmg;
            //Check if the player has died
            if (CurrentHealth <= 0)
            {
                //Activate death state
            }
        }
    }
}
