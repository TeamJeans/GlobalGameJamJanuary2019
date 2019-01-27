using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {

    public Animator animator;
    public GameObject dialogue;

	// Update is called once per frame
	void Update ()
    {
        if (dialogue.GetComponent<Dialogue>().endScene)
        {
            animator.SetTrigger("FadeOut");
        }
	}

    public void OnFadeComplete()
    {
        switch (dialogue.GetComponent<Dialogue>().currentNight)
        {
            case 1:
                {
                    SceneManager.LoadScene(1);
                    break;
                }
            case 2:
                {
                    SceneManager.LoadScene(2);
                    break;
                }
            case 3:
                {
                    SceneManager.LoadScene(3);
                    break;
                }
            case 4:
                {
                    SceneManager.LoadScene(4);
                    break;
                }
            case 5:
                {
                    SceneManager.LoadScene(5);
                    break;
                }
            default:
                break;
        }
        
    }
}
