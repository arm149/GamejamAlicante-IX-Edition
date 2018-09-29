using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using System;

public class MenuManager : MonoBehaviour
{
  public Bush bush; //Container of useful information for the level load

  //Animator component for the fade in/out transition
  //public Animator FadeAnimator;

  //Different canvas of the scene
  public Canvas p;   //Pause canvas
  public Canvas v;   //Victory canvas
  public Canvas d;   //Defeat canvas

  // Use this for initialization
  void Start()
  {
    //============================
    //Get the clock running just in case the game fucked up
    Time.timeScale = 1.0f;
    //============================

    //Get information bush from the scene
    bush = GameObject.FindGameObjectWithTag("Bush").GetComponent<Bush>();

    //Hide any canvas on the scene save for the HUD
    GameObject p_object = GameObject.Find("PauseCanvas");
    if (p_object != null)
      p = p_object.GetComponent<Canvas>();

    GameObject v_object = GameObject.Find("VictoryCanvas");
    if (v_object != null)
      v = v_object.GetComponent<Canvas>();

    GameObject d_object = GameObject.Find("DefeatCanvas");
    if (d_object != null)
      d = d_object.GetComponent<Canvas>();
      
    if (p != null)
      p.enabled = false;
    
    if (v != null)
      v.enabled = false;

    if (d != null)
      d.enabled = false;
  }

  //Init Game
  public void StartGame()
  {
    //Load the first level
    UnityEngine.SceneManagement.SceneManager.LoadScene(bush.levels[0]);

    //Restore time
    Time.timeScale = 1.0f;
  }

  public void QuitGame()
  {
    //Quit the game
    Application.Quit();
  }

  public void NextLevel()
  {
    //Set current level to the next one
    bush.currentLevel++;

    //Restore time
    Time.timeScale = 1.0f;

    //Start the fade out transition to the next level
    //FadeAnimator.SetTrigger("FadeOut");
  }

  public void OnFadeComplete()
  {
    //Load the next level to the current one
    UnityEngine.SceneManagement.SceneManager.LoadScene(bush.levels[bush.currentLevel]);
  }

  public void ContinueFromMenu()
  {
    //Restore time
    Time.timeScale = 1.0f;

    //Load the current one
    UnityEngine.SceneManagement.SceneManager.LoadScene(bush.levels[bush.currentLevel]);
  }

  public void ContinueFromGame()
  {
    //Continue time
    Time.timeScale = 1.0f;
    
    //Hide any canvas
    if (p != null)
      p.enabled = false;
    if (v != null)
      v.enabled = false;
    if (d != null)
      d.enabled = false;
  }

  // Main menu when the game loads
  public void MainMenu()
  {
    //Load main menu scene
    UnityEngine.SceneManagement.SceneManager.LoadScene(bush.mainMenu);
    Time.timeScale = 1.0f;
  }

  // Credits menu
  public void CreditsMenu()
  {
    //Load credits menu scene
    UnityEngine.SceneManagement.SceneManager.LoadScene(bush.creditsMenu);
    Time.timeScale = 1.0f;
  }

  public void PauseGame()
  {
    //Show canvas
    if (p != null)
    {
      p.enabled = true;
    }

    //Stop time
    Time.timeScale = 0.0f;
  }

  public void WinGame()
  {
    //Show canvas		
    if (v != null)
      v.enabled = true;

    //Stop time
    Time.timeScale = 0.0f;
  }

  public void LoseGame()
  {
    //Show canvas
    if (d != null)
      d.enabled = true;

    //Stop time
    Time.timeScale = 0.0f;
  }
}
