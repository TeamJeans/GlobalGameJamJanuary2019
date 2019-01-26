using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

	[SerializeField]
	Transform target;

	[SerializeField]
	float sensitivityX = 1f;
	[SerializeField]
	float sensitivityY = 1f;

	[SerializeField]
	float maxViewAngle = 50f;
	[SerializeField]
	float minViewAngle = 0f;

	[SerializeField]
	bool invertY;
	[SerializeField]
	bool stationaryCamera;

	[SerializeField]
	Camera cam;

	[SerializeField]
	Vector3 offset;
	public Vector3 Offset { get { return offset; } set { offset = value; } }

	float currentX = 0f;
	float currentY = 0f;

	void Start()
	{
	}

	void Update()
	{
		currentX += Input.GetAxis("ProControllerRightJoystickHorizontal") * sensitivityX;
		if (invertY)
		{
			currentY += Input.GetAxis("ProControllerRightJoystickVertical") * sensitivityY;
		}
		else
		{
			currentY -= Input.GetAxis("ProControllerRightJoystickVertical") * sensitivityY;
		}

		currentY = Mathf.Clamp(currentY, minViewAngle, maxViewAngle);
	}

	void LateUpdate()
	{
		Quaternion rotation = Quaternion.Euler(currentY, currentX, 0f);
		if (!stationaryCamera)
		{
			cam.gameObject.transform.position = target.position + rotation * offset;
		}
		else
		{
			cam.gameObject.transform.position = target.position + offset;
		}

		if (cam.gameObject.transform.position.y < target.position.y - .5f)
		{
			cam.gameObject.transform.position = new Vector3(cam.gameObject.transform.position.x, target.position.y - .5f, cam.gameObject.transform.position.z);
		}

		cam.gameObject.transform.LookAt(target.position);
	}
}
