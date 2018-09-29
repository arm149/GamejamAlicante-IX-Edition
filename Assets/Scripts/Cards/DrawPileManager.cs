using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DrawPileManager : MonoBehaviour
{
  const int CARD_SLOTS = 4;

  // Reference to main deck
  public Deck deck;
  
  // Reference to transform objects
  public Transform[] cardTransforms;

  // Reference to pile count
  public Transform pileCount;

  // Reference to discard pile
  public Transform discardPile;

  // Reference to card scripts
  private Card[] cardSlots;

  // Sprite path
  const string SPRITE_PATH = "Cards/";
  // Sprite assets name
  const string JUMP_ASSET = "jumpcard", MELEE_ATTACK_ASSET = "attackcard", SHOOT_ATTACK_ASSET = "HPFloopy", SHIELD_ASSET = "protectioncrad";

  private void Start()
  {
    deck = new Deck(GetComponent<Deck>().getDeck());
    deck.Shuffle();
    pileCount.GetComponent<Text>().text = deck.getDeckCount().ToString();

    cardSlots = new Card[CARD_SLOTS];

    for (int i = 0; i < CARD_SLOTS; i++)
    {
      cardSlots[i] = new Card();

      cardSlots[i] = cardTransforms[i].GetComponent<Card>();
    }

    StartCoroutine(DrawCard());
  }

  // If there are rooms, fill slots with cards
  public IEnumerator DrawCard()
  {
    for (int i = 0; i < CARD_SLOTS; i++)
    {
      yield return new WaitForSeconds(0.15f);

      // If there are free slots, fill them
      if (cardSlots[i].isEmpty())
      {
        // If there are cards to draw
        if (deck.getLastCard() != CardType.Null)
        {
          cardSlots[i].Draw(deck.getLastCard());
          deck.RemoveLastCard();
        }
        // Put discard pile on draw pile
        else
        {
          this.deck.setDeck(discardPile.GetComponent<DiscardPileManager>().deck.getDeck());
          discardPile.GetComponent<DiscardPileManager>().deck.Clear();
        }
      }
      pileCount.GetComponent<Text>().text = deck.getDeckCount().ToString();

      // Show next card
      if (deck.getLastCard() != CardType.Null)
      {
        CardType cardType = deck.getLastCard();
        if (cardType == CardType.Jump)
        {
          GetComponent<Image>().sprite = Resources.Load<Sprite>(SPRITE_PATH + JUMP_ASSET);
        }
        else if (cardType == CardType.MeleeAttack)
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
  }
}