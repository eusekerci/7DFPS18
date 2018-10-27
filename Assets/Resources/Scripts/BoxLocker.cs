using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLocker : MonoBehaviour {	
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Box"))
		{
			other.tag = "StaticBox";
			other.gameObject.GetComponent<Renderer>().material =
				RoomManager.Instance.ActiveRoom.GetComponent<Renderer>().material;
		}
	}
}
