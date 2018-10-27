using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitiliazer : MonoBehaviour {

	void Start () {
		PortalBindPlugin.Instance.Init();
		RoomManager.Instance.Init();
	}
	

}
