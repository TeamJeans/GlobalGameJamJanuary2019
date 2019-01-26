using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YakMovement : MonoBehaviour {

	[SerializeField]
	float amountOfTimeToMoveFor = 5.0f;
	float movementTimeCounter = 0.0f;
	public float MovementTimeCounter { get { return movementTimeCounter; } set { movementTimeCounter = value; } }

	[SerializeField]
	float moveSpeed = 5.0f;
	[SerializeField]
	float rotationSpeed = 10f;
	[SerializeField]
	float gravityScale = 2.5f;

	CharacterController controller;

	Vector3 moveDir;
	public Vector3 MoveDir { get { return moveDir; } set { moveDir = value; } }
	bool followingTarget = false;
	public bool FollowingTarget { get { return followingTarget; } }
	bool runFromTarget = false;
	public bool RunFromTarget { get { return runFromTarget; } }

	GameObject target;
	public GameObject Target { get { return target; } set { target = value; } }

	bool stationary = true;
	public bool Stationary { get { return stationary; } }


	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

		if (followingTarget)
		{
			runFromTarget = false;
		}

		if (runFromTarget)
		{
			followingTarget = false;
		}

		if (movementTimeCounter <= 0)
		{
			// Stand still
			followingTarget = false;
			runFromTarget = false;
			stationary = true;
		}
		else
		{
			movementTimeCounter -= Time.deltaTime;

			stationary = false;

			if (followingTarget)
			{
				// Rotate to look at the target
				transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(target.transform.position - transform.position), rotationSpeed * Time.deltaTime);

				// Move towards the target
				transform.position += transform.forward * moveSpeed * Time.deltaTime;

			}
			else if (runFromTarget)
			{
				float tempY = transform.position.y;
				// Rotate to look away from the target
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - target.transform.position), rotationSpeed * Time.deltaTime);

				// Move away from target
				transform.position += transform.forward * moveSpeed * Time.deltaTime;
				transform.position = new Vector3(transform.position.x, tempY, transform.position.z);
			}

		}

		// Apply gravity
		moveDir.y += (Physics.gravity.y * gravityScale * Time.deltaTime);

		// Move the player
		controller.Move(moveDir * Time.deltaTime);
	}

	public void StartFollowingTarget()
	{
		followingTarget = true;
		movementTimeCounter = amountOfTimeToMoveFor;
	}

	public void StartRunningFromTarget()
	{
		runFromTarget = true;
		movementTimeCounter = amountOfTimeToMoveFor;
	}

	public void StopMoving()
	{
		Debug.Log("Stopping movement");
		movementTimeCounter = 0f;
		stationary = true;

	}
}
