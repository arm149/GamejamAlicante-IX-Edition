using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckVictory();
	}

    private void CheckVictory()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("Entra: " + g.Length);
        if(g.Length == 0)
        {
            //We win
            Debug.Log("Hemos ganado");
        }
    }
}
