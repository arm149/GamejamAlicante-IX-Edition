using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deck : MonoBehaviour
{
  [SerializeField]
  List<CardType> deck;

  // Reorder list randomly
  public void Shuffle()
  {
    for (int i = 0; i < this.deck.Count; i++)
    {
      CardType temp = deck[i];
      int randomIndex = Random.Range(i, deck.Count);
      deck[i] = deck[randomIndex];
      deck[randomIndex] = temp;
    }
  }

  public void addCard(CardType card)
  {
    deck.Add(card);
  }

  public CardType getLastCard()
  {
    if (deck.Count > 0)
    {
      return deck[deck.Count - 1];
    }
    else
    {
      return CardType.Null;
    }
  }

  public void RemoveLastCard()
  {
    deck.RemoveAt(deck.Count - 1);
  }

  public void RemoveFromIndexCard(int index)
  {
    try
    {
      deck.RemoveAt(index);
    }
    catch (System.IndexOutOfRangeException ex)
    {
      System.ArgumentException argEx = new System.ArgumentException("Index is out of range", "index", ex);
      throw argEx;
    }
  }

  public Deck getDeck()
  {
    return this;
  }
}
