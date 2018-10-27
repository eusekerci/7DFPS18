using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{	
	void UnlockDoor()
	{
		gameObject.GetComponent<Collider>().isTrigger = true;
		gameObject.GetComponent<Rigidbody>().isKinematic = true;
		iTween.MoveTo(gameObject, transform.position + new Vector3(0, -4, 0), 5);
	}
	
	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Key"))
		{
			MessageBus.Publish(new PlayerDropCarry()
			{
				PutPoint = new Vector3(-10000f, -10000f, -10000f),
				Normal = Vector3.zero
			});				
			UnlockDoor();
		}
	}
}
