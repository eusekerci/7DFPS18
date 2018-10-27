using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PicturePlugin : MonoBehaviour
{
	public string RoomNo;
	public MeshRenderer PictureRenderer;
	public bool IsAlreadyExist;
	public string SamePicture;
	public Collider[] Linkers;

	private GameObject _roomObject;
	private Camera _roomCamera;
	private MeshRenderer _copyFromRenderer;
	private static bool _isLinkerSelected;
	private static Portal _linkedPortal;
	private Portal[] _portals;

	private Collider _collider;
	
	void Start ()
	{
		_isLinkerSelected = false;
		
		InitiliazePicture();

		MessageBus.OnEvent<PlayerClickLinker>().Subscribe(evnt =>
		{
			for (int i=0;i<Linkers.Length; i++)
			{
				if (Linkers[i].GetInstanceID() == evnt.Col.GetInstanceID())
				{
					_isLinkerSelected = true;
					_linkedPortal = _portals[i];
					print("Linker Selected = " + i);
					break;
				}
			}
		});

		MessageBus.OnEvent<PlayerDropLinker>().Subscribe(evnt =>
		{
			if (_isLinkerSelected)
			{
				for (int i=0;i<Linkers.Length; i++)
				{
					if (Linkers[i].GetInstanceID() == evnt.Col.GetInstanceID() && _portals[i].GetInstanceID() != _linkedPortal.GetInstanceID())
					{
						PortalBindPlugin.Instance.BindPortals(_linkedPortal, _portals[i]);
						_isLinkerSelected = false;
						_linkedPortal = null;
						print("Linker Matched = " + i);
						break;
					}
				}
			}
		});
		
		MessageBus.OnEvent<PlayerLinkerReset>().Subscribe(evnt =>
		{
			_isLinkerSelected = false;
			_linkedPortal = null;
			print("Linkers are free");
		});

	}

	public void InitiliazePicture()
	{
		_collider = GetComponent<Collider>();
		_roomObject = GameObject.Find("Room" + RoomNo);
		if (!_roomObject)
			return;
		_roomCamera = _roomObject.GetComponentInChildren<Camera>();

		if (IsAlreadyExist)
		{
			GameObject samePic = GameObject.Find(SamePicture);
			PictureRenderer.material = samePic.transform.Find("Renderer")
				.GetComponent<MeshRenderer>().material;
		}
		else
		{
			if (_roomCamera.targetTexture != null)
			{
				_roomCamera.targetTexture.Release();
			}

			_roomCamera.targetTexture = new RenderTexture(256, 256, 24);
			PictureRenderer.materials[0] = new Material(Resources.Load<Shader>("Shaders/ScreenCutoutShader"));
			PictureRenderer.material.mainTexture = _roomCamera.targetTexture;
		}

		_portals = new Portal[Linkers.Length];

		for (int i = 0; i < Linkers.Length; i++)
		{
			GameObject go = GameObject.Find("Portal" + RoomNo + "_0" + (i + 1));
			if (go.GetComponent<Portal>() != null)
			{
				_portals[i] = go.GetComponent<Portal>();	
			}
			else if(go.GetComponent<PlaneTeleporter>() != null)
			{
				_portals[i] = go.GetComponent<PlaneTeleporter>();
			}
			
		}
	}
}
