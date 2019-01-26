using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerYakFollow : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Yak" && this.gameObject != other.gameObject)
		{
			Debug.Log(other);

			// Checks if the collider has a target
			if (other.gameObject.GetComponent<YakMovement>().Target != this.gameObject)
			{
				// If the collider is stationary, then this object is moving towards it, therefore set the colliders target to be this object
				if (other.gameObject.GetComponent<YakMovement>().Stationary)
				{
					Debug.Log("1" + other);
					other.gameObject.GetComponent<YakMovement>().Target = this.gameObject;
					other.gameObject.GetComponent<YakMovement>().StartFollowingTarget();
					return;
				}
				// If this object is stationary then the collider is moving towards it so set this objects target to be the collider
				else if(this.gameObject.GetComponent<YakMovement>().Stationary)
				{
					Debug.Log("2" + other);
					Debug.Log("The problem: " + other.gameObject.GetComponent<YakMovement>().Target + this.gameObject);
					this.gameObject.GetComponent<YakMovement>().Target = other.gameObject;
					this.gameObject.GetComponent<YakMovement>().StartFollowingTarget();
					return;
				}
				// If this object and the collider are moving following a target
				else if(other.gameObject.GetComponent<YakMovement>().FollowingTarget)
				{
					Debug.Log("3" + other);
					this.gameObject.GetComponent<YakMovement>().Target = other.gameObject;
					this.gameObject.GetComponent<YakMovement>().StartFollowingTarget();
					return;
				}
				// If this object and the collider are moving running from a target
				else if(other.gameObject.GetComponent<YakMovement>().RunFromTarget)
				{
					Debug.Log("5" + other);
					//Debug.Log("Yak is Running");
					this.gameObject.GetComponent<YakMovement>().Target = other.gameObject;
					this.gameObject.GetComponent<YakMovement>().StartFollowingTarget();
					return;
				}
			}
		}
	}
}
