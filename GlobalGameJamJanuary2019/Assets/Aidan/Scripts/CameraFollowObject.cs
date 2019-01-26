using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour {

	[SerializeField]
	Transform father;
	[SerializeField]
	Transform son;

	[SerializeField]
	Transform mainCameraTransform;

	float distBetween;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = (father.position + son.position)/ 2f;
		distBetween = Vector3.Distance(father.position, transform.position);

		// Change the offset of the camera depending on where the players are
		transform.position =  new Vector3(transform.position.x, distBetween * 2, transform.position.z);
	}
}
