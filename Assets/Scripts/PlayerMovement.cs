using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    //Speed of the player
    public float Speed = 40f;

    //RigidBody component
    private Rigidbody2D rigidbody;

    //Transform component
    private Transform t;

    //Horizontal axis input
    float horizontalMove = 0f;

    //Check whether the character is facing the right or the left of the screen
    bool RightFacing = true;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        t = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        horizontalMove = Input.GetAxis("Horizontal");
	}

    private void FixedUpdate()
    {
        Move(horizontalMove * Time.fixedDeltaTime * Speed);
    }

    //Move function
    void Move(float MoveSpeed)
    {
        //Add a force to the player to move it
        rigidbody.velocity = new Vector2(MoveSpeed * 5f, rigidbody.velocity.y);
        //Check if the movement is in the opposite direction of the current player face, to flip him
        if((RightFacing && MoveSpeed < 0) || (!RightFacing && MoveSpeed > 0))
        {
            Flip();
        }
    }

    //Function for flipping the character
    void Flip()
    {
        RightFacing = !RightFacing;

        //Multiply the x scale with -1 to flip the character
        Vector3 Scale = t.localScale;
        Scale.x = Scale.x * -1;
        t.localScale = Scale;
    }
}
