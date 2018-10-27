using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfBinder : MonoBehaviour
{

	public string PortalAName;
	public string PortalBName;
	
	private bool _isActive;

	private void Start()
	{
		_isActive = true;
	}
	private void OnTriggerEnter(Collider other)
	{
		if (_isActive && other.CompareTag("Player"))
		{
			PortalBindPlugin.Instance.BindPortals(GameObject.Find(PortalAName).GetComponent<Portal>(), GameObject.Find(PortalBName).GetComponent<Portal>());

			_isActive = false;
		}
	}
}
