using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineTrigger : MonoBehaviour {

	[SerializeField]
	GameManager gm;
	[SerializeField]
	GameObject endFollowObject;

	[SerializeField]
	GameObject father;
	[SerializeField]
	GameObject son;

	bool sonCrossed = false;
	bool fatherCrossed = false;

	AudioManager am;

	// Use this for initialization
	void Start () {
		am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (sonCrossed && fatherCrossed)
		{
			gm.BothCrossed = true;
		}
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

		if (other.gameObject.tag == "Player")
		{
			if (other.gameObject == son)
			{
				sonCrossed = true;
			}

			if (other.gameObject == father)
			{
				fatherCrossed = true;
			}
		}
	}
}
