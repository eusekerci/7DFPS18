using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PortalTeleporter : MonoBehaviour {

	public Transform Receiver;

	private Transform player;
	private bool playerIsOverlapping = false;

	void Start()
	{
		player = GameObject.Find("Player").transform;
	}
	
	void LateUpdate () 
	{
		if (playerIsOverlapping && Receiver)
		{
			Vector3 portalToPlayer = player.position - transform.position;
			float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

			// If this is true: The player has moved across the portal
			if (dotProduct < 0f)
			{
				// Teleport him!
				float rotationDiff = -Quaternion.Angle(transform.rotation, Receiver.rotation);
				rotationDiff += 180;	

				print(player.rotation.eulerAngles + " " + rotationDiff);
				player.Rotate(Vector3.up, rotationDiff);
				print(player.rotation.eulerAngles + " " + rotationDiff);

				Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
				player.position = Receiver.position + positionOffset;

				playerIsOverlapping = false;
			}
		}
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
