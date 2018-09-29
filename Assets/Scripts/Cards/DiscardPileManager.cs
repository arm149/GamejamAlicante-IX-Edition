using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DiscardPileManager : MonoBehaviour
{
  // Reference to discarded deck
  public Deck deck;

  // Reference to main deck
  public Transform drawPile;
  DrawPileManager dpm;

  // Reference to pile count
  public Transform pileCount;
  Text pileCountText;

  private void Start()
  {
    deck = new Deck();

    dpm = drawPile.GetComponent<DrawPileManager>();

    pileCountText = pileCount.GetComponent<Text>();
    pileCountText.text = deck.getDeckCount().ToString();

    StartCoroutine(ManageDiscardPile());
  }

  // If there are rooms, fill slots with cards
  IEnumerator ManageDiscardPile()
  {
    while (true)
    {
      yield return new WaitForSeconds(0.2f);

      // Draw pile empty, reshuffle
      if (dpm.deck.getDeckCount() == 0)
      {
        dpm.deck.setDeck(deck.getDeck()); // Copy discarded deck on draw pile
        deck.Clear(); // Clear this deck
        StartCoroutine(dpm.DrawCard()); // Update UI

        pileCountText.text = deck.getDeckCount().ToString();
      }
    }
  }

  public void UpdateUI()
  {
    pileCountText.text = deck.getDeckCount().ToString();
  }

  public void addCard(CardType cardType)
  {
    this.deck.addCard(cardType);
    StartCoroutine(dpm.DrawCard()); // Update UI
  }
}