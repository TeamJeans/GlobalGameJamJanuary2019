using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

	// Use this for initialization
	void Start () {
		cowCounterText.text = currentNoOfYaks + "/" + noOfYaksNeededToPass;
		winScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		cowCounterText.text = currentNoOfYaks + "/" + noOfYaksNeededToPass;

		if (currentNoOfYaks >= noOfYaksNeededToPass)
		{
			winScreen.SetActive(true);
		}
	}
}
