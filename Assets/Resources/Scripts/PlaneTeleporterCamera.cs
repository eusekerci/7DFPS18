using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlaneTeleporterCamera : MonoBehaviour
{
	public Camera Camera;
	public FPSExtension CameraExtension;
	public float FadeOutTime;
	public float FadeInTime;
	public bool FadeOutCompleted;
	public float MinFOV;
	public float MaxFOV;
	
	void Start ()
	{
		MessageBus.OnEvent<PlayerPlaneTeleportStart>().Subscribe(evnt => { StartTeleport(); });
		FadeOutCompleted = false;
	}

	void StartTeleport()
	{
		CameraExtension.LockRotation = true;
		StartCoroutine(FadeOut());
	}

	public void EndTeleport()
	{
		StartCoroutine(FadeIn());
	} 

	IEnumerator FadeOut()
	{
		print("Fade Out Started");
		Camera.fieldOfView = MaxFOV;
		float interval = (MaxFOV - MinFOV) / (FadeOutTime / Time.deltaTime);
		for (float i = MaxFOV; i > MinFOV; i-=interval)
		{
			yield return new WaitForEndOfFrame();
			Camera.fieldOfView = i;
		}
		Camera.fieldOfView = MinFOV;
		FadeOutCompleted = true;
	}
	
	IEnumerator FadeIn()
	{
		print("Fade In Started");
		Camera.fieldOfView = MinFOV;
		float interval = (MaxFOV - MinFOV) / (FadeInTime / Time.deltaTime);
		for (float i = MinFOV; i < MaxFOV; i+=interval)
		{
			yield return new WaitForEndOfFrame();
			Camera.fieldOfView = i;
		}

		Camera.fieldOfView = MaxFOV;
		FadeOutCompleted = false;
		CameraExtension.LockRotation = false;
	}
}
