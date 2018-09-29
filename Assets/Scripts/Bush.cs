using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
  //=====================================
  //List of variables needed for the game
  //=====================================
  public string mainMenu;       //first menu when the game loads
  public string creditsMenu;
  public List<string> levels;   //Array of names of the levels
  public int numberOfLevels;    //total number of levels
  public int currentLevel;      //currentLevel (by default first level = 0)

  // Use this for initialization
  void Start()
  {
    //Persistence across scenes
    DontDestroyOnLoad(gameObject);

    // Avoid duplicating bushes
    if (FindObjectsOfType(GetType()).Length > 1)
    {
      Destroy(gameObject);
      Debug.Log("Destroy duplicated bush");
    }

    currentLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
  }
}
