using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToStopYakMovement : MonoBehaviour {

	[SerializeField]
	GameObject currentYak;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Yak" && other.gameObject != currentYak)
		{
			//Debug.Log("Hello world");
			if (other.gameObject.GetComponent<YakMovement>().Target == currentYak && currentYak.GetComponent<YakMovement>().MovementTimeCounter <= 0)
			{
				Debug.Log("Hello world");
				other.gameObject.GetComponent<YakMovement>().StopMoving();
			}
		}
	}
}
