using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlaneTeleporter : Portal {
	
	private Transform Player;
	private Transform PlayerCamera;
	private PlaneTeleporterCamera TeleporterCameraLogic;

	private bool TeleportingStart;
	
	void Start ()
	{
		MessageBus.OnEvent<PlayerPlaneTeleportStart>().Subscribe(evnt => { StartTeleport(); });
		Player = GameObject.Find("Player").transform;
		PlayerCamera = Player.Find("PlayerCamera");
		TeleporterCameraLogic = Player.GetComponent<PlaneTeleporterCamera>();

		TeleportingStart = false;
	}
	
	void LateUpdate ()
	{
		if (!TeleportingStart)
			return;

		if (TeleporterCameraLogic.FadeOutCompleted)
		{
			PlayerCamera.Rotate(90, 0, 0);
			Player.position = Receiver.position;
			TeleportingStart = false;
			TeleporterCameraLogic.EndTeleport();
		}
	}

	void StartTeleport()
	{
		TeleportingStart = true;
	}
}
