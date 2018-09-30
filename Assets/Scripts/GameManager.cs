using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public MenuManager menuManager;

  private void Start()
  {
    menuManager = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuManager>();
  }

  void Update()
  {
    CheckVictory();
  }

  private void CheckVictory()
  {
    GameObject[] g = GameObject.FindGameObjectsWithTag("Enemy");
    Debug.Log("Entra: " + g.Length);
    if (g.Length == 0)
    {
      menuManager.VictoryMenu();
      Debug.Log("Hemos ganado");
    }
  }
}
