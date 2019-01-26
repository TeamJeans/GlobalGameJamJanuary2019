using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	float moveSpeed;
	[SerializeField]
	float jumpForce;
	[SerializeField]
	float gravityScale;
	//[SerializeField]
	//Animator anim;
	[SerializeField]
	Transform pivot;
	[SerializeField]
	float rotateSpeed;
	//[SerializeField]
	//GameObject playerModel;
	float knockBackCounter = 0;

	CharacterController controller;
	Vector3 moveDir;
	

	// Use this for initialization
	void Start ()
	{
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (knockBackCounter <= 0)
		{
			// Calculate the direction the player is moving in
			float yStore = moveDir.y;
			moveDir = (transform.forward * Input.GetAxis("Vertical") * moveSpeed) + (transform.right * Input.GetAxis("Horizontal") * moveSpeed);
			moveDir = moveDir.normalized * moveSpeed;
			moveDir.y = yStore;

			// Jump
			if (controller.isGrounded)
			{
				moveDir.y = 0;
				if (Input.GetButtonDown("ProControllerX"))
				{
					moveDir.y = jumpForce;
				}
			}
		}
		else
		{
			knockBackCounter -= Time.deltaTime;
		}

		// Apply gravity
		moveDir.y += (Physics.gravity.y * gravityScale * Time.deltaTime);

		// Move the player
		controller.Move(moveDir * Time.deltaTime);

		// Move the player in different directions depending on the camera's look direction
		if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
		{
			transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y,0f);
			Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDir.x, 0f, moveDir.z));
			//playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
		}

		// Animation variables
		//anim.SetBool("isGrounded", controller.isGrounded);
		//anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical"))) + (Mathf.Abs(Input.GetAxis("Horizontal"))));
	}

	public void knockBack(Vector3 knockBackDir, float knockBackForce, float knockBackTime)
	{
		knockBackCounter = knockBackTime;

		moveDir = knockBackDir * knockBackForce;
		moveDir.y = knockBackForce;
	}
}
