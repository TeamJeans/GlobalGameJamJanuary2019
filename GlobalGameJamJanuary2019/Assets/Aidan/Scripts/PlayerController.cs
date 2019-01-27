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

	AudioManager am;
	bool soundPlaying = false;

	[SerializeField]
	GameObject pullSpeechBubble;
	[SerializeField]
	GameObject pushSpeechBubble;
	[SerializeField]
	float speechBubbleTime = 1.0f;
	float speechBubbleTimeCounter = 0;

	// Use this for initialization
	void Start ()
	{
		controller = GetComponent<CharacterController>();
		pullCollider.enabled = false;
		pushCollider.enabled = false;

		pullSpeechBubble.SetActive(false);
		pushSpeechBubble.SetActive(false);

		am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (knockBackCounter <= 0)
		{
			// Calculate the direction the player is moving in
			float yStore = moveDir.y;
			moveDir = (transform.forward * Input.GetAxis("XboxLeftJSVertical") * moveSpeed) + (transform.right * Input.GetAxis("XboxLeftJSHorizontal") * moveSpeed);
			moveDir = moveDir.normalized * moveSpeed;
			moveDir.y = yStore;
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
		if (Input.GetAxis("XboxLeftJSHorizontal") != 0 || Input.GetAxis("XboxLeftJSVertical") != 0)
		{
			transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y,0f);
			Quaternion newRotation = Quaternion.LookRotation(new Vector3(-moveDir.x, 0f, -moveDir.z));
			playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
		}

		// Animation variables
		//anim.SetBool("isGrounded", controller.isGrounded);
		anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("XboxLeftJSVertical"))) + (Mathf.Abs(Input.GetAxis("XboxLeftJSHorizontal"))));
		if ((Mathf.Abs(Input.GetAxis("XboxLeftJSVertical"))) + (Mathf.Abs(Input.GetAxis("XboxLeftJSHorizontal"))) > 0.1f)
		{
			if (!soundPlaying)
			{
				soundPlaying = true;
				am.PlaySound("HorseSound");
			}
		}
		else
		{
			soundPlaying = false;
			am.StopSound("HorseSound");
		}

		// Using the pull mechanic
		if (Input.GetAxis("XboxLT") != 0)
		{
			Debug.Log("LT Pressed!");
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
		if (Input.GetButtonDown("XboxLB"))
		{
			Debug.Log("LB Pressed!");
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

		if (speechBubbleTimeCounter <= 0)
		{
			pullSpeechBubble.SetActive(false);
			pushSpeechBubble.SetActive(false);
		}
		else
		{
			speechBubbleTimeCounter -= Time.deltaTime;
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
		am.PlaySound("BluePull");

		speechBubbleTimeCounter = speechBubbleTime;
		pullSpeechBubble.SetActive(true);
		pushSpeechBubble.SetActive(false);
	}

	void activatePushCommand()
	{
		pushCollider.enabled = true;
		pushColliderTimeCounter = pushColliderActiveTime;
		am.PlaySound("BluePush");

		speechBubbleTimeCounter = speechBubbleTime;
		pullSpeechBubble.SetActive(false);
		pushSpeechBubble.SetActive(true);
	}
}
