using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineTrigger : MonoBehaviour {

	[SerializeField]
	GameManager gm;
	[SerializeField]
	GameObject endFollowObject;

	AudioManager am;

	// Use this for initialization
	void Start () {
		am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Yak")
		{
			Debug.Log("Cow has been added!");
			other.gameObject.GetComponent<YakMovement>().Target = endFollowObject;
			am.PlaySound("CowCounter");
			gm.CurrentNoOfYaks++;
		}
	}
}
