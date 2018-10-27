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

	private void Start()
	{
		ActiveRoom = GameObject.Find("Room01");
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.K))
		{
			StartCoroutine(LoadRoomAsync("Room03"));
		}
	}

	public void LoadRoom(string roomName)
	{
		StartCoroutine(LoadRoomAsync(roomName));
	}
	
	IEnumerator LoadRoomAsync(string roomName)
	{
		AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(roomName);
		
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
		
		SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);
	}
}
