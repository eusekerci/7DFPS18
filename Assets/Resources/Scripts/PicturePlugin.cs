using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEditor.VersionControl;

public class PicturePlugin : MonoBehaviour
{
	public Camera RoomCamera;
	public MeshRenderer PictureRenderer;
	public Collider[] Linkers;
	public Portal[] Portals;

	private static bool _isLinkerSelected;
	private static Portal _linkedPortal;

	private Collider _collider;
	
	void Start ()
	{
		_isLinkerSelected = false;
		
		_collider = GetComponent<Collider>();
		
		if (RoomCamera.targetTexture != null)
		{
			RoomCamera.targetTexture.Release();
		}
		RoomCamera.targetTexture = new RenderTexture(256, 256, 24);
		PictureRenderer.materials[0] = new Material(Resources.Load<Shader>("Shaders/ScreenCutoutShader"));
		PictureRenderer.material.mainTexture = RoomCamera.targetTexture;
		
		MessageBus.OnEvent<PlayerClickLinker>().Subscribe(evnt =>
		{
			for (int i=0;i<Linkers.Length; i++)
			{
				if (Linkers[i].GetInstanceID() == evnt.Col.GetInstanceID())
				{
					_isLinkerSelected = true;
					_linkedPortal = Portals[i];
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
					if (Linkers[i].GetInstanceID() == evnt.Col.GetInstanceID() && Portals[i].GetInstanceID() != _linkedPortal.GetInstanceID())
					{
						PortalBindPlugin.Instance.BindPortals(_linkedPortal, Portals[i]);
						_isLinkerSelected = false;
						print("Linker Matched = " + i);
						break;
					}
				}
			}
		});
		
		MessageBus.OnEvent<PlayerLinkerReset>().Subscribe(evnt =>
		{
			_isLinkerSelected = false; 
			print("Linkers are free");
		});

	}
}
