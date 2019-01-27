using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour {

	[SerializeField]
	float moveSpeed;
	[SerializeField]
	float jumpForce;
	[SerializeField]
	float gravityScale;
	[SerializeField]
	Animator anim;
	[SerializeField]
	Transform pivot;
	[SerializeField]
	float rotateSpeed;
	[SerializeField]
	GameObject playerModel;
	float knockBackCounter = 0;

	CharacterController controller;
	Vector3 moveDir;

	[SerializeField]
	Collider pullCollider;
	[SerializeField]
	float pullColliderActiveTime = 5.0f;
	float pullColliderTimeCounter = 0f;

	[SerializeField]
	Collider pushCollider;
	[SerializeField]
	float pushColliderActiveTime = 5.0f;
	float pushColliderTimeCounter = 0f;


	// Use this for initialization
	void Start()
	{
		controller = GetComponent<CharacterController>();
		pullCollider.enabled = false;
		pushCollider.enabled = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (knockBackCounter <= 0)
		{
			// Calculate the direction the player is moving in
			float yStore = moveDir.y;
			moveDir = (transform.forward * Input.GetAxis("ProControllerRightJoystickVertical") * moveSpeed) + (transform.right * Input.GetAxis("ProControllerRightJoystickHorizontal") * moveSpeed);
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
		if (Input.GetAxis("ProControllerRightJoystickHorizontal") != 0 || Input.GetAxis("ProControllerRightJoystickVertical") != 0)
		{
			transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
			Quaternion newRotation = Quaternion.LookRotation(new Vector3(-moveDir.x, 0f, -moveDir.z));
			playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
		}

		// Animation variables
		//anim.SetBool("isGrounded", controller.isGrounded);
		anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("ProControllerRightJoystickVertical"))) + (Mathf.Abs(Input.GetAxis("ProControllerRightJoystickHorizontal"))));

		// Using the pull mechanic
		if (Input.GetButtonDown("ProControllerZR"))
		{
			Debug.Log("ZR Pressed!");
			activatePullCommand();
		}

		if (pullColliderTimeCounter <= 0)
		{
			pullCollider.enabled = false;
		}
		else
		{
			pullColliderTimeCounter -= Time.deltaTime;
		}

		// Using the push mechanic
		if (Input.GetButtonDown("ProControllerR"))
		{
			Debug.Log("R Pressed!");
			activatePushCommand();
		}

		if (pushColliderTimeCounter <= 0)
		{
			pushCollider.enabled = false;
		}
		else
		{
			pushColliderTimeCounter -= Time.deltaTime;
		}
	}

	public void knockBack(Vector3 knockBackDir, float knockBackForce, float knockBackTime)
	{
		knockBackCounter = knockBackTime;

		moveDir = knockBackDir * knockBackForce;
		moveDir.y = knockBackForce;
	}

	void activatePullCommand()
	{
		pullCollider.enabled = true;
		pullColliderTimeCounter = pullColliderActiveTime;
	}

	void activatePushCommand()
	{
		pushCollider.enabled = true;
		pushColliderTimeCounter = pushColliderActiveTime;
	}
}
