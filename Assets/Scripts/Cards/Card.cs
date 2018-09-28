using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public enum CardType { Jump, MeleeAttack, ShootAttack, Shield, Null };

public class Card : MonoBehaviour
{
  public Transform canvas;
  public CardType cardType;
  public Sprite sprite;

  // Position on the HUD
  [Range(1,4)]
  public int position;

  public Card()
  {
    cardType = CardType.Null;
  }

  private void Start()
  {
    cardType = CardType.Null;
    //GetComponent<Image>().sprite = sprite;
  }

  void Update()
  {
    if (Input.GetKey(GetKeyPosition(position)))
    {
      UseCard(this.cardType);
      Discard();
    }
  }

  // Returns the key to activate this card based on its position
  KeyCode GetKeyPosition(int position)
  {
    KeyCode keyCode = KeyCode.Z;

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
    return keyCode;
  }

  // Use this card action
  void UseCard(CardType cardType)
  {
    if(cardType == CardType.Jump)
    {
      // player.jump
    }
    else if(cardType == CardType.MeleeAttack)
    {
      // player.meleeAttack
    }
  }

  void Discard()
  {
    cardType = CardType.Null;
    GetComponent<Image>().sprite = null;
    // discardPile.add(this.cardType);
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
    GetComponent<Image>().sprite = sprite;
  }
}
