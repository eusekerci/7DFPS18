using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
	public Transform portal;
	public Transform otherPortal;
	public Transform renderer;
	public Transform otherRenderer;
	
	private Transform playerCamera;

	void Start()
	{
		playerCamera = Camera.main.transform;
	}
	
	void Update () 
	{	
		//float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation);
		if(!renderer || !portal)
			return;
		
		float angularDifferenceBetweenPortalRotations = Vector3.SignedAngle(renderer.up, otherRenderer.up, Vector3.up) + 180;
		
		Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
		Vector3 rotatedVector = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up) *
		                        playerOffsetFromPortal.normalized;
		transform.position = portal.position + playerOffsetFromPortal.magnitude * rotatedVector;

		Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
		Vector3 newCameraDirection = (portalRotationalDifference) * (playerCamera.forward);
		transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
	}
}
