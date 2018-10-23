using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerInventory : MonoBehaviour
{
	public Transform Camera;

	private bool _isHolding;
	private Vector3 _localHoldPosition;
	private Vector3 _localHoldRotation;
	private Transform _myObject;

	void Start()
	{
		_isHolding = false;
		_localHoldPosition = new Vector3(-0.7f, -0.7f, 0.7f);
		_localHoldRotation = new Vector3(0, -250, 0);
		
		MessageBus.OnEvent<PlayerStartCarry>().Subscribe(evnt => { HoldItem(evnt.Col); });
		MessageBus.OnEvent<PlayerDropCarry>().Subscribe(evnt => { PutItem(evnt.PutPoint, evnt.Normal); });
	}

	void HoldItem(Collider col)
	{
		if (_isHolding)
			return;
		_isHolding = true;
		col.gameObject.GetComponent<Rigidbody>().isKinematic = true;
		col.transform.parent = Camera;
		col.transform.localPosition = _localHoldPosition;
		col.transform.localEulerAngles = _localHoldRotation;
		_myObject = col.transform;
		print("Item Hold");
	}

	void PutItem(Vector3 point, Vector3 normal)
	{
		if(!_isHolding)
			return;

		_isHolding = false;
		_myObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
		_myObject.parent = null;
		_myObject.localEulerAngles = new Vector3(0f, 0f, 0f);
		_myObject.position = point + normal * 0.5f;
		_myObject = null;
		print("Item Drop");
	}
}
