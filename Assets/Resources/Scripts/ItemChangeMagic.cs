using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChangeMagic : MonoBehaviour
{

	public GameObject[] ItemsToDisable;
	public GameObject[] ItemsToEnable;

	private bool _isActive;

	private void Start()
	{
		_isActive = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (_isActive && other.CompareTag("Player"))
		{
			foreach (var Item in ItemsToDisable)
			{
				Item.gameObject.SetActive(false);
			}
			foreach (var Item in ItemsToEnable)
			{
				Item.gameObject.SetActive(true);
			}

			_isActive = false;
		}
	}
}
