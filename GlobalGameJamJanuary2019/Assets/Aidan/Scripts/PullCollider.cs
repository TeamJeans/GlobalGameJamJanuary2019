using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullCollider : MonoBehaviour {

	[SerializeField]
	GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log(other);
		// If any yaks are within the collider then make the closest one follow the player that called it
		if (other.gameObject.tag == "Yak" && this.gameObject != other.gameObject)
		{
			other.gameObject.GetComponent<YakMovement>().Target = player;
			other.gameObject.GetComponent<YakMovement>().StartFollowingTarget();
		}
	}
}
