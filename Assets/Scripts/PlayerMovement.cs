using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    //Speed of the player
    public float Speed = 40f;

    //RigidBody component
    private Rigidbody2D rigidbody;

    //Horizontal axis input
    float horizontalMove = 0f;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        horizontalMove = Input.GetAxis("Horizontal");
	}

    private void FixedUpdate()
    {
        Move(horizontalMove * Time.fixedDeltaTime);
    }

    void Move(float MoveSpeed)
    {
        Debug.Log("Entrando: "+ horizontalMove);
        rigidbody.velocity = new Vector2(MoveSpeed * 50f, rigidbody.velocity.y);
    }
}
