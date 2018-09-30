using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public AudioSource source;
    public AudioClip jump;

    //Force of the jump
    public float JumpForce = 10;

  //Is player jumping?
  bool jumping = false;
  //Is player attacking?
  bool attacking = false;
    //Is player covering?
    bool covering = false;
  //Has the player damaged someone at the current attack?
  bool damaged = false;

  //Check if we have landed
  bool checkLanding = true;

  //RigidBody2D component of the player
  private Rigidbody2D rigidbody;

  //CircleCollider2d component of the player
  private CircleCollider2D circlecollider;

  //Collider for the attack
  public BoxCollider2D AttackTrigger;

  //CD between an attack starts and ends
  public float AttackCD = 0.3f;

  //Current time until the attack ends
  float CurrentAttackTimer = 0.0f;

  //Health of the player
  public int CurrentHealth;

  //Maximum health of the player
  public int MaximumHealth = 3;

  //Attack power of the character
  public int AttackPower = 1;

  //Animator of the player
  public Animator animator;

    public float shieldCD = 1f;

    private float currentShieldCD = 0f;

  // HUD extension
  public bool jumpCardTriggered = false, attackCardTriggered = false, shieldCardTriggered = false;
  public MenuManager menuManager;

  // Use this for initialization
  void Start()
  {
    rigidbody = GetComponent<Rigidbody2D>();
    circlecollider = GetComponent<CircleCollider2D>();
    CurrentHealth = MaximumHealth;
    AttackTrigger.enabled = false;

    menuManager = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuManager>();
  }

  // Update is called once per frame
  void Update()
  {
    //Check if we jump (We can double/triple/etc jump)
    if (jumpCardTriggered)
    {
      jumpCardTriggered = false;
      jumping = true;
      checkLanding = false;
      animator.SetBool("IsJumping", true);

      source.PlayOneShot(jump, 1F);
    }
    //Check if we attack (and we currently are not attacking
    if (attackCardTriggered  && !attacking && !covering)
    {
      attackCardTriggered = false;
      attacking = true;
      damaged = false;
      CurrentAttackTimer = AttackCD;
      AttackTrigger.enabled = true;

      animator.SetBool("IsAttacking", true);
    }
    if(shieldCardTriggered && !attacking && !covering)
    {
            covering = true;
            shieldCardTriggered = false;
            currentShieldCD = shieldCD;

            animator.SetBool("IsShielding", true);
    }

        if(covering)
        {
            currentShieldCD -= Time.deltaTime;
            if(currentShieldCD < 0f)
            {
                covering = false;
                animator.SetBool("IsShielding", false);
            }
        }

    //Update in case we are attacking
    if (attacking)
    {
      //Check the time until the attack ends
      CurrentAttackTimer -= Time.deltaTime;
      if (CurrentAttackTimer < 0.0f)
      {
        //Deactivate all the attack conditions
        AttackTrigger.enabled = false;
        attacking = false;
        animator.SetBool("IsAttacking", false);
        damaged = true;
      }
    }


    if (Input.GetKeyDown(KeyCode.Escape))
    {
      menuManager.PauseGame();
    }
    else if (Input.GetKeyDown(KeyCode.P))
    {
      menuManager.ContinueFromGame();
    }
  }

  //Check if we have landed in the ground
  private void OnCollisionEnter2D(Collision2D collision)
  {
    //Check if we have jumped (otherwise, we are colliding because we are already on the ground
    if (!checkLanding)
    {
      checkLanding = true;
      animator.SetBool("IsJumping", false);
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Enemy")
    {
      if (!damaged)
      {
        //Make damage to the target
        damaged = true;
        collision.gameObject.GetComponent<EnemyBehaviour>().ReceiveDamage(AttackPower);
      }
    }
  }

  private void OnTriggerStay2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Enemy")
    {
      if (!damaged)
      {
        //Make damage to the target
        damaged = true;
        collision.gameObject.GetComponent<EnemyBehaviour>().ReceiveDamage(AttackPower);
      }
    }
  }

  private void FixedUpdate()
  {
    //Check if we are already jumping
    if (jumping)
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
  public void ReceiveDamage(int dmg)
  {
    //Check if the damage received is positive
    if (dmg > 0 && !covering)
    {
      //Make damage to the player
      CurrentHealth -= dmg;
      //Check if the player has died
      if (CurrentHealth <= 0)
      {
        menuManager.DefeatMenu();
        Destroy(gameObject);
      }
    }
  }
}