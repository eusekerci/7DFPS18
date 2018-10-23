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
		_rotateX += Input.GetAxis("RotateX") * RotationSpeed * Time.deltaTime;
		_rotateY -= Input.GetAxis("RotateY") * RotationSpeed * Time.deltaTime;
		_rotateY = Mathf.Clamp(_rotateY, -90f, 90f); 
		
		transform.eulerAngles = new Vector3(0f, _rotateX, 0f);
		Camera.eulerAngles = new Vector3(_rotateY, Camera.eulerAngles.y, Camera.eulerAngles.z);
	}
}

