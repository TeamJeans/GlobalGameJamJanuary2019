using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {

	[SerializeField]
	int noOfYaksTotal;
	[SerializeField]
	int noOfYaksNeededToPass;
	int currentNoOfYaks = 0;
	public int CurrentNoOfYaks { get { return currentNoOfYaks; } set { currentNoOfYaks = value; } }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
