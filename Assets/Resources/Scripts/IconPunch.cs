using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class IconPunch : MonoBehaviour
{
	private IDisposable _myEvent;
	private void Start()
	{
		_myEvent = MessageBus.OnEvent<PlayerPictureTake>().Subscribe(evnt =>
		{
			iTween.PunchScale(gameObject, new Vector3(1f,1f,1f), 1f);
		});
	}

	private void OnDestroy()
	{
		_myEvent.Dispose();
	}
}
