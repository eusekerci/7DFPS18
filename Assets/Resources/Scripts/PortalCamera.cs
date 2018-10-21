using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
	//public Transform playerPosition;
	public Transform playerCamera;
	public Transform portal;
	public Transform otherPortal;

	private float cameraOffset = 1.8f;
	
	// Update is called once per frame
	void Update () {
		//Vector3 playerOffsetFromPortal = playerPosition.position - otherPortal.position;
		//transform.position = portal.position + playerOffsetFromPortal + new Vector3(0, cameraOffset, 0);
		Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
		transform.position = portal.position + playerOffsetFromPortal;

		float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation);

		Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
		Vector3 newCameraDirection = (portalRotationalDifference) * (playerCamera.forward);
		transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
	}
}
