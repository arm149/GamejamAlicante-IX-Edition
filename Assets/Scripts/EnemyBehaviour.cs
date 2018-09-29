using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    //Life the enemy starts with (also acts as Max Life)
    public int StartingHealth = 1;

    //Damage of the attack
    public int AttackPower = 1;

    //Current health of the enemy
    private int CurrentHealth;

    //Is the enemy attacking?
    private bool attacking = false;

    //Attack range of the enemy
    public float AttackRange = 1.0f;

    //Speed of the enemy
    public float MoveSpeed = 40f;

    //Time between attacks
    public float AttackCD = 0.3f;

    //Time until the current attack finishes
    private float CurrentAttackCD = 0.0f;

    //Pointer to the player
    private GameObject player;

    //Rigidbody of the player
    private Rigidbody2D rigidbody;

    //Has the attack damaged yet?
    private bool damaged = false;

    //Area of the attack hitbox
    public BoxCollider2D AttackTrigger;

	// Use this for initialization
	void Start () {
        CurrentHealth = StartingHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        AttackTrigger.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Fixed Update is called a fixed amount of times every second
    private void FixedUpdate()
    {
        //Evaluate the distance to the player
        float d = Vector3.Distance(player.transform.position, gameObject.transform.position);
        
        if(d > AttackRange)
        {
            //Move
            Move();
        }
        else
        {
            //Attack
            Attack();
        }
    }

    //Move Script
    private void Move()
    {
        if(player.transform.position.x > gameObject.transform.position.x)
        {
            //Move to the right
            rigidbody.velocity = new Vector2(MoveSpeed * Time.fixedDeltaTime * 5f, rigidbody.velocity.y);
        }
        else
        {
            //Move to the left
            rigidbody.velocity = new Vector2(MoveSpeed * Time.fixedDeltaTime * 5f * -1f, rigidbody.velocity.y);
        }
    }

    private void Attack()
    {
        //Check if we are attacking
        if(!attacking)
        {
            attacking = true;
            AttackTrigger.enabled = true;
            CurrentAttackCD = AttackCD;
            damaged = false;
        }
        else
        {
            CurrentAttackCD -= Time.fixedDeltaTime;
            if(CurrentAttackCD <= 0.0f)
            {
                attacking = false;
                AttackTrigger.enabled = false;
                damaged = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!damaged)
            {
                //Make damage to the target
                damaged = true;
                collision.gameObject.GetComponent<PlayerActions>().ReceiveDamage(AttackPower);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!damaged)
            {
                //Make damage to the target
                damaged = true;
                collision.gameObject.GetComponent<PlayerActions>().ReceiveDamage(AttackPower);
            }
        }
    }

    public void ReceiveDamage(int dmg)
    {
        //Check the dmg is a positive value different from zero
        if(dmg > 0)
        {
            CurrentHealth -= dmg;
            //Check if we have died
            if (CurrentHealth <= 0)
            {
                //Destroy him!
                Destroy(gameObject);
            }
        }
    }
}
