using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneRotator : MonoBehaviour
{

	public float RotateSpeed;
	
	void FixedUpdate () {
		transform.Rotate(Vector3.up, RotateSpeed * Time.deltaTime);
	}
}
