using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class menuButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StaticValueHolder.CurrentNight = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetAxis("ProControllerA") != 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }	
	}
}
