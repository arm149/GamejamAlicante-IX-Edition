using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public enum CardType { Jump, MeleeAttack, ShootAttack, Shield, Null };

public class Card : MonoBehaviour
{
  public Transform discardPile;
  public CardType cardType;

  // Position on the HUD
  [Range(1,4)]
  public int position;

  // Key to activate this card
  KeyCode keyCode;

  // Sprite path
  const string SPRITE_PATH = "Cards/";
  // Sprite assets name
  const string JUMP_ASSET = "jumpcard", MELEE_ATTACK_ASSET = "attackcard", SHOOT_ATTACK_ASSET = "HPFloopy", SHIELD_ASSET = "protectioncrad";

  // Reference to the player
  public GameObject player;
  PlayerActions playerActions;

  public Card()
  {
    cardType = CardType.Null;
  }

  private void Start()
  {
    cardType = CardType.Null;

    // Assigns the key to activate this card
    AssignKeyPosition(position);

    player = GameObject.FindGameObjectWithTag("Player");
    playerActions = player.GetComponent<PlayerActions>();
  }

  void Update()
  {
    if (Input.GetKeyDown(keyCode) && this.cardType != CardType.Null)
    {
      UseCard();
      Discard();
    }
  }

  // Returns the key to activate this card based on its position
  void AssignKeyPosition(int position)
  {
    if(position == 1)
    {
      keyCode = KeyCode.Alpha1;
    }
    else if(position == 2)
    {
      keyCode = KeyCode.Alpha2;
    }
    else if(position == 3)
    {
      keyCode = KeyCode.Alpha3;
    }
    else if(position == 4)
    {
      keyCode = KeyCode.Alpha4;
    }
  }

  // Use this card action
  void UseCard()
  {
    if(cardType == CardType.Jump)
    {
      playerActions.jumpCardTriggered = true;
    }
    else if(cardType == CardType.MeleeAttack)
    {
      // playerActions.attackCardTriggered = true;
    }
    else if(cardType == CardType.ShootAttack)
    {
      // player.ShootAttack
    }
    else if(cardType == CardType.Shield)
    {
      // playerActions.shieldCardTriggered = true;
    }
  }

  void Discard()
  {
    // Move card to discard pile
    discardPile.GetComponent<DiscardPileManager>().addCard(this.cardType);
    discardPile.GetComponent<DiscardPileManager>().UpdateUI();

    cardType = CardType.Null;
    GetComponent<Image>().sprite = null;
  }

  // Returns if this slot is NOT containing a card
  public bool isEmpty()
  {
    return cardType == CardType.Null;
  }

  // Assigns this slot a card
  public void Draw(CardType cardType)
  {
    this.cardType = cardType;

    AssignSprite();
  }

  // Assign sprite based on CardType
  void AssignSprite()
  {
    if (cardType == CardType.Jump)
    {
      GetComponent<Image>().sprite = Resources.Load<Sprite>(SPRITE_PATH + JUMP_ASSET);
    }
    else if(cardType == CardType.MeleeAttack)
    {
      GetComponent<Image>().sprite = Resources.Load<Sprite>(SPRITE_PATH + MELEE_ATTACK_ASSET);
    }
    else if (cardType == CardType.ShootAttack)
    {
      GetComponent<Image>().sprite = Resources.Load<Sprite>(SPRITE_PATH + SHOOT_ATTACK_ASSET);
    }
    else if (cardType == CardType.Shield)
    {
      GetComponent<Image>().sprite = Resources.Load<Sprite>(SPRITE_PATH + SHIELD_ASSET);
    }
  }
}
