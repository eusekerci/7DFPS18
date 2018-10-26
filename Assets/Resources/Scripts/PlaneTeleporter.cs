using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlaneTeleporter : Portal {
	
	public PlaneTeleporterCamera TeleporterCameraLogic;
	public Transform Player;
	public Transform PlayerCamera;

	private bool TeleportingStart;
	
	void Start ()
	{
		MessageBus.OnEvent<PlayerPlaneTeleportStart>().Subscribe(evnt => { StartTeleport(); });	
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
