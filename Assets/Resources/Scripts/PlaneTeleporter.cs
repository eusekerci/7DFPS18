using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlaneTeleporter : Portal {
	
	private Transform _player;
	private Transform _playerCamera;
	private PlaneTeleporterCamera _teleporterCameraLogic;

	private bool _teleportingStart;
	
	void Start ()
	{
		_player = GameObject.Find("Player").transform;
		_playerCamera = _player.Find("PlayerCamera");
		_teleporterCameraLogic = _player.GetComponent<PlaneTeleporterCamera>();

		_teleportingStart = false;
	}
	
	void LateUpdate ()
	{
		if (!_teleportingStart)
			return;

		if (_teleporterCameraLogic.FadeOutCompleted)
		{
			_playerCamera.Rotate(90, 0, 0);
			_player.position = Receiver.position;
			_teleportingStart = false;
			_teleporterCameraLogic.EndTeleport();
			RoomManager.Instance.ActiveRoom = Receiver.transform.parent.gameObject;
		}
	}

	public override void StartTeleport()
	{
		_teleportingStart = true;
	}
}
