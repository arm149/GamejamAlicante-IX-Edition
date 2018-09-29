using UnityEngine;

public class PauseMenuControler : MonoBehaviour
{
  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.C))
    {
      GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MenuManager>().ContinueFromGame();
    }
    else if (Input.GetKeyDown(KeyCode.M))
    {
      GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MenuManager>().MainMenu();
    }
    else if (Input.GetKeyDown(KeyCode.Q))
    {
      GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MenuManager>().QuitGame();
    }
  }
}