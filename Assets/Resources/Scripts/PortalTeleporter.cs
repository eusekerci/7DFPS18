using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System;


public class PortalTeleporter : MonoBehaviour {

	public Transform Receiver;

	private Transform player;
	private Transform _playerCamera;
	private bool _playerIsOverlapping = false;
	private string _myRoomName;
	
	void Start()
	{
		player = GameObject.Find("Player").transform;
		_playerCamera = player.GetComponentInChildren<Camera>().transform;
		_myRoomName = transform.parent.parent.gameObject.name;
	}
	
	void LateUpdate () 
	{
		if (_playerIsOverlapping && Receiver)
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

				_playerIsOverlapping = false;

				RoomManager.Instance.ActiveRoom = Receiver.transform.parent.parent.gameObject;
				
				//First room magic trick
				if(Receiver.transform.parent.parent.gameObject.name != _myRoomName || _myRoomName != "Room01")
					RoomManager.Instance.LoadRoom(_myRoomName);
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
		if (other.CompareTag("Player"))
		{
			_playerIsOverlapping = true;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.CompareTag("Player"))
		{
			_playerIsOverlapping = false;
		}
	}
}
