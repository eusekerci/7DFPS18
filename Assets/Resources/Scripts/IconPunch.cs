using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class IconPunch : MonoBehaviour {
	private void Start()
	{
		MessageBus.OnEvent<PlayerPictureTake>().Subscribe(evnt =>
		{
			iTween.PunchScale(gameObject, new Vector3(1f,1f,1f), 1f);
		});
	}
}
