using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateManager : MonoBehaviour {

	[SerializeField]
	GameObject openColliders;

	[SerializeField]
	GameObject closedColliders;

	[SerializeField]
	PuzzleButton button;

	// Use this for initialization
	void Start () {
		closedColliders.SetActive(true);
		openColliders.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (button.ButtonState)
		{
			closedColliders.SetActive(false);
			openColliders.SetActive(true);
		}
		else
		{
			closedColliders.SetActive(true);
			openColliders.SetActive(false);
		}
	}
}
