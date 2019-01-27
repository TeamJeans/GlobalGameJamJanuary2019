using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	[SerializeField]
	float timeToDoLevel;

	[SerializeField]
	int noOfYaksTotal;
	[SerializeField]
	int noOfYaksNeededToPass;
	int currentNoOfYaks = 0;
	public int CurrentNoOfYaks { get { return currentNoOfYaks; } set { currentNoOfYaks = value; } }

	[SerializeField]
	GameObject winScreen;

	[SerializeField]
	TextMeshProUGUI cowCounterText;

	[SerializeField]
	TextMeshProUGUI timerText;

	[SerializeField]
	float timerTime = 10f;

	[SerializeField]
	GameObject gameOverScreen;

	// Use this for initialization
	void Start () {
		cowCounterText.text = currentNoOfYaks + "/" + noOfYaksNeededToPass;
		winScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		cowCounterText.text = currentNoOfYaks + "/" + noOfYaksNeededToPass;
		timerText.text = (int)timerTime + "";

		if (currentNoOfYaks >= noOfYaksNeededToPass)
		{
			winScreen.SetActive(true);

			if (Input.GetButtonDown("ProControllerA"))
			{
				goToNextLevel();
			}
		}

		if (timerTime <= 0)
		{
			timerTime = 0;
			if (!winScreen.activeSelf)
			{
				gameOverScreen.SetActive(true);
			}
		}
		else
		{
			timerTime -= Time.deltaTime;
		}

		if (Input.GetButtonDown("ProControllerY"))
		{
			restartLevel();
		}
	}

	public void restartLevel()
	{
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}

	public void goToNextLevel()
	{
		SceneManager.LoadScene("Camp");
	}
}
