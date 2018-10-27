using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeTrigger : MonoBehaviour {

	public string[] RoomsToUnload;
	public string[] RoomsToLoad;

	private bool _isActive;

	private void Start()
	{
		_isActive = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (_isActive && other.CompareTag("Player"))
		{
			foreach (var Room in RoomsToUnload)
			{
				RoomManager.Instance.UnloadRoom(Room);
			}
			foreach (var Room in RoomsToLoad)
			{
				RoomManager.Instance.LoadRoom(Room);
			}

			_isActive = false;
		}
	}
}
