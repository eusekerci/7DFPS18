using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;



public class Room : MonoBehaviour
{
	private void Start()
	{
		IEnumerable<GameObject> pictures = GameObject.FindGameObjectsWithTag("Picture");
		foreach (var picture in pictures)
		{
			picture.GetComponent<PicturePlugin>().InitiliazePicture();
		}

		IEnumerable<Portal> portals = gameObject.GetComponentsInChildren<Portal>();
		foreach (var portal in portals)
		{
			PortalBindPlugin.Instance.RestoreBind(portal);
		}
	}
}
