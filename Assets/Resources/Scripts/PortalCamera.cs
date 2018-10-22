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
	private float angularDiffBetweenPortals = 0;
	private Vector3 playerOffset;
	private Quaternion portalsRotationalDiff;
	private Vector3 newCameraDirection;
	private Vector3 rotatedVector;

	void Start()
	{
		playerCamera = Camera.main.transform;
	}
	
	void Update () 
	{	
		//float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation);
		if(!renderer || !portal || !otherPortal || !otherRenderer)
			return;
		
		angularDiffBetweenPortals = Vector3.SignedAngle(renderer.up, otherRenderer.up, Vector3.up) + 180;
		
		playerOffset = playerCamera.position - otherPortal.position;
		rotatedVector = Quaternion.AngleAxis(angularDiffBetweenPortals, Vector3.up) *
		                        playerOffset.normalized;
		transform.position = portal.position + playerOffset.magnitude * rotatedVector;

		portalsRotationalDiff = Quaternion.AngleAxis(angularDiffBetweenPortals, Vector3.up);
		newCameraDirection = (portalsRotationalDiff) * (playerCamera.forward);
		transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
	}
}
