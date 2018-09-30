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

    //CD Between attacks, to avoid enemies too powerful
    public float CDBetweenAttacks = 0.5f;

    //Current CD between the attacks of the enemy
    private float CurrentCDBetweenAttacks;

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

    //Is the enemy facing right?
    private bool FacingRight = true;

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
            
            if(!FacingRight)
            {
                Flip();
            }
        }
        else
        {
            //Move to the left
            rigidbody.velocity = new Vector2(MoveSpeed * Time.fixedDeltaTime * 5f * -1f, rigidbody.velocity.y);
            if(FacingRight)
            {
                Flip();
            }
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
            CurrentCDBetweenAttacks = CDBetweenAttacks;
            damaged = false;
        }
        else
        {
            CurrentAttackCD -= Time.fixedDeltaTime;
            if(CurrentAttackCD <= 0.0f)
            {
                AttackTrigger.enabled = false;
                CurrentCDBetweenAttacks -= Time.fixedDeltaTime;
                if(CurrentCDBetweenAttacks <= 0.0f)
                {
                    attacking = false;
                    damaged = true;
                }
            }
        }
    }
    
    //For the case of the player entering the trigger of an enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check, so we only damage the player
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

    //For the case of the player staying on the trigger of an enemy
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Check, so we only damage the player
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

    //Flip the enemy
    public void Flip()
    {
        FacingRight = !FacingRight;

        //Invert it's scale on the x transform in order to flip it
        Vector3 Scale = gameObject.transform.localScale;
        Scale.x = Scale.x * -1;
        gameObject.transform.localScale = Scale;
    }
}
