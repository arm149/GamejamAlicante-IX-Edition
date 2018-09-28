using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardManager : MonoBehaviour
{
  const int CARD_SLOTS = 4;

  // Referemce to main deck
  public Deck deck;
  
  // Reference to transform objects
  public Transform[] cardTransforms;

  // Reference to card scripts
  private Card[] cardSlots;

  private void Start()
  {
    deck = GetComponent<Deck>().getDeck();
    deck.Shuffle();

    cardSlots = new Card[CARD_SLOTS];

    for (int i = 0; i < CARD_SLOTS; i++)
    {
      cardSlots[i] = new Card();

      cardSlots[i] = cardTransforms[i].GetComponent<Card>();
    }

    StartCoroutine(DrawCard());
  }

  // If there are rooms, fill slots with cards
  IEnumerator DrawCard()
  {
    while (true)
    {
      for (int i = 0; i < CARD_SLOTS; i++)
      {
        yield return new WaitForSeconds(0.25f);

        if (cardSlots[i].isEmpty())
        {
          cardSlots[i].Draw(deck.getLastCard());
        }
      }
    }
  }
}