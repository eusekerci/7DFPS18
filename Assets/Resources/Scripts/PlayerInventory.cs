using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerInventory : MonoBehaviour
{
	public Transform Camera;

	private bool _isHolding;
	private Vector3 _localHoldPositionBox;
	private Vector3 _localHoldRotationBox;
	private Vector3 _localHoldPositionKey;
	private Vector3 _localHoldRotationKey;
	private Transform _myObject;

	void Start()
	{
		_isHolding = false;
		_localHoldPositionBox = new Vector3(-0.7f, -0.7f, 0.7f);
		_localHoldRotationBox = new Vector3(0, -250, 0);
		_localHoldPositionKey = new Vector3(-0.55f, -0.35f, 0.75f);
		_localHoldRotationKey = new Vector3(-100, 0, 0);
		
		MessageBus.OnEvent<	PlayerStartCarry>().Subscribe(evnt => { HoldItem(evnt.Col); });
		MessageBus.OnEvent<PlayerDropCarry>().Subscribe(evnt => { PutItem(evnt.PutPoint, evnt.Normal); });
	}

	void HoldItem(Collider col)
	{
		if (_isHolding)
			return;
		_isHolding = true;
		col.gameObject.GetComponent<Rigidbody>().isKinematic = true;
		col.transform.parent = Camera;
		if (col.CompareTag("Box"))
		{
			col.transform.localPosition = _localHoldPositionBox;
			col.transform.localEulerAngles = _localHoldRotationBox;
		}
		else if (col.CompareTag("Key"))
		{
			col.transform.localPosition = _localHoldPositionKey;
			col.transform.localEulerAngles = _localHoldRotationKey;
		}

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
