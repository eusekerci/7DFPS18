using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour {

	#region Singleton

	private static RoomManager _instance;
	
	public static RoomManager Instance { get { return _instance; } }

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		} else {
			_instance = this;
		}
	}

	#endregion
	
	public GameObject ActiveRoom;

	public void Init()
	{
		LoadRoom("Room01", true);
		LoadRoom("Room02");
		LoadRoom("Room03");
		LoadRoom("Room04");
		LoadRoom("Room05");
		ActiveRoom = GameObject.Find("Room01");
	}

	public void LoadRoom(string roomName, bool firstTime = false)
	{
		StartCoroutine(LoadRoomAsync(roomName, firstTime));
	}
	
	IEnumerator LoadRoomAsync(string roomName, bool firstTime = false)
	{
		if (SceneManager.GetSceneByName(roomName).isLoaded)
		{
			AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(roomName);

			while (!asyncUnload.isDone)
			{
				yield return null;
			}
		}

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

		if (firstTime)
		{
			while (!asyncLoad.isDone)
			{
				yield return null;
			}
			
			PortalBindPlugin.Instance.BindPortals(GameObject.Find("Portal01_01").GetComponent<Portal>(), GameObject.Find("Portal01_02").GetComponent<Portal>());
		}
	}

	public void UnloadRoom(string roomName)
	{
		StartCoroutine(UnloadRoomAsync(roomName));
	}
	
	IEnumerator UnloadRoomAsync(string roomName)
	{
		if (SceneManager.GetSceneByName(roomName).isLoaded)
		{
			AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(roomName);

			while (!asyncLoad.isDone)
			{
				yield return null;
			}
		}
	}
}
