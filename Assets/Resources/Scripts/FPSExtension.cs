using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSExtension : MonoBehaviour {

	public float RotationSpeed = 10.0F;
	public Transform Camera;

	private float _rotateX;
	private float _rotateY;
	
	void FixedUpdate()
	{
		float _rotateX = Input.GetAxis("RotateX") * RotationSpeed;
		float _rotateY = Input.GetAxis("RotateY") * RotationSpeed;

		_rotateX *= Time.deltaTime;
		_rotateY *= Time.deltaTime;

		transform.Rotate(0, _rotateX, 0);
		Camera.Rotate(-_rotateY, 0, 0);
	}
}

