using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

	[SerializeField]
	int damageToGive;

	[SerializeField]
	float knockBackForce;
	[SerializeField]
	float knockBackTime;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			Vector3 knockBackDir = other.transform.position - transform.position;
			knockBackDir = knockBackDir.normalized;
			other.gameObject.GetComponent<HealthManager>().damage(damageToGive, knockBackDir, knockBackForce, knockBackTime);
		}
	}
}
