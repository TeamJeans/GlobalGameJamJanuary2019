using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : MonoBehaviour {

	[SerializeField]
	MeshRenderer meshRenderer;
	[SerializeField]
	Material buttonOnMaterial;
	[SerializeField]
	Material buttonOffMaterial;

	[SerializeField]
	Animator gateAnim;

	bool buttonState;

	// Use this for initialization
	void Start () {
		buttonState = false;
	}
	
	// Update is called once per frame
	void Update () {
			gateAnim.SetBool("ButtonState", buttonState);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			buttonState = true;
			meshRenderer.material = buttonOnMaterial;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			buttonState = false;
			meshRenderer.material = buttonOffMaterial;
		}
	}
}
