using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSExtension : MonoBehaviour {

	public float RotationSpeed = 10.0F;
	public Transform Camera;
	public bool LockRotation;

	private void Start()
	{
		LockRotation = false;
	}

	void FixedUpdate()
	{
		if (LockRotation)
		{
			return;
		}
		float _rotateX = Input.GetAxis("RotateX") * RotationSpeed;
		float _rotateY = Input.GetAxis("RotateY") * RotationSpeed;
		_rotateX *= Time.deltaTime;
		_rotateY *= Time.deltaTime;
		transform.Rotate(0, _rotateX, 0);
		Camera.Rotate(-_rotateY, 0, 0);
	}
}

