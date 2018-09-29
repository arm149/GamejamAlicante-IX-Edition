using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
  //============================
  // INITIAL MENU
  //============================
  int selector;
  int selectorOld;
  public int SELECTOR_COUNT;
  GameObject espada1, espada2, espada3, espada4;

  //===============
  //Sound variables
  //===============
  private AudioSource audioSource;
  public AudioClip[] audioSounds;

  // Use this for initialization
  void Start()
  {
    //============================
    // INITIAL MENU
    //============================
    selector = 0;

    espada1 = gameObject.transform.Find("Panel").transform.Find("Selector1").gameObject;
    espada2 = gameObject.transform.Find("Panel").transform.Find("Selector2").gameObject;
    espada3 = gameObject.transform.Find("Panel").transform.Find("Selector3").gameObject;
    espada4 = gameObject.transform.Find("Panel").transform.Find("Selector4").gameObject;

    espada1.GetComponent<Image>().enabled = true;
    espada2.GetComponent<Image>().enabled = false;
    espada3.GetComponent<Image>().enabled = false;
    espada4.GetComponent<Image>().enabled = false;

    audioSource = GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void Update()
  {
    // Check if it's active
    if (GetComponent<Canvas>().isActiveAndEnabled)
    {
      //Go down
      if (Input.GetKeyDown(KeyCode.S))
      {
        selector++;
        if (selector >= SELECTOR_COUNT)
        {
          selector = 0;
        }
      }

      //Go up
      if (Input.GetKeyDown(KeyCode.W))
      {
        selector--;
        if (selector < 0)
        {
          selector = SELECTOR_COUNT - 1;
        }
      }

      //Press button
      /*if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
      {
        switch (selector)
        {
          case 0:
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MenuManager>().StartGame();
            break;
          case 1:
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MenuManager>().ContinueFromMenu();
            break;
          default:
            break;
        }
      }*/

      //Avoid entering in each update
      if (selector != selectorOld)
      {
        switch (selector)
        {
          case 0:
            ShowSelector1();
            break;
          case 1:
            ShowSelector2();
            break;
          case 2:
            ShowSelector3();
            break;
          case 3:
            ShowSelector4();
            break;
          default:
            break;
        }
      }

      //assing old selector
      selectorOld = selector;
    }
  }

  //===============================
  // MENU BUTTONS
  //===============================
  public void ShowSelector1()
  {
    espada1.GetComponent<Image>().enabled = true;
    espada2.GetComponent<Image>().enabled = false;
    espada3.GetComponent<Image>().enabled = false;
    espada4.GetComponent<Image>().enabled = false;

    selector = 0;

    PlayRandomSound();
  }

  public void ShowSelector2()
  {
    espada1.GetComponent<Image>().enabled = false;
    espada2.GetComponent<Image>().enabled = true;
    espada3.GetComponent<Image>().enabled = false;
    espada4.GetComponent<Image>().enabled = false;

    selector = 1;

    PlayRandomSound();
  }

  public void ShowSelector3()
  {
    espada1.GetComponent<Image>().enabled = false;
    espada2.GetComponent<Image>().enabled = false;
    espada3.GetComponent<Image>().enabled = true;
    espada4.GetComponent<Image>().enabled = false;

    selector = 2;

    PlayRandomSound();
  }

  public void ShowSelector4()
  {
    espada1.GetComponent<Image>().enabled = false;
    espada2.GetComponent<Image>().enabled = false;
    espada3.GetComponent<Image>().enabled = false;
    espada4.GetComponent<Image>().enabled = true;

    selector = 3;

    PlayRandomSound();
  }

  //Play a ricochet out of the list
  public void PlayRandomSound()
  {
    //Void guardian
    if (audioSource == null || audioSounds.Length <= 0)
      return;

    //Select music clip
    int index = Random.Range(0, audioSounds.Length);

    //Play music clip *** CALCULATE DISTANCE VOLUME VALUE ***
    audioSource.PlayOneShot(audioSounds[index], 1.0f);
  }
}