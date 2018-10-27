using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System;


public class PortalTeleporter : MonoBehaviour {

	public Transform Receiver;

	private Transform player;
	private Transform _playerCamera;
	private bool playerIsOverlapping = false;

	void Start()
	{
		player = GameObject.Find("Player").transform;
		_playerCamera = player.GetComponentInChildren<Camera>().transform;
	}
	
	void LateUpdate () 
	{
		if (playerIsOverlapping && Receiver)
		{
			Vector3 portalToPlayer = player.position - transform.position;
			float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

			if (dotProduct < 0f)
			{
				float rotationDiff = -Quaternion.Angle(transform.rotation, Receiver.rotation);
				if(IsAdjustmentNeeded(transform, Receiver))
					rotationDiff += 180;	

				player.Rotate(Vector3.up, rotationDiff);

				Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
				player.position = Receiver.position + positionOffset;

				playerIsOverlapping = false;
			}
		}
	}

	bool IsAdjustmentNeeded(Transform transformA, Transform transformB)
	{
		float angle = Vector3.SignedAngle(transformA.up, transformB.up, Vector3.up);

		return angle < 0.1f || angle > 179.9f;
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
		{
			playerIsOverlapping = true;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player")
		{
			playerIsOverlapping = false;
		}
	}
}
