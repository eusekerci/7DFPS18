using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour {

	void UnlockDoor()
	{
		iTween.MoveTo(gameObject, transform.position - new Vector3(0,5,0), 5f);
	}
	
	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Key"))
		{
			UnlockDoor();
		}
	}
}
