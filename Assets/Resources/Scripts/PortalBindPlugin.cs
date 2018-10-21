using System.Collections;
using System.Collections.Generic;
using ProBuilder2.Common;
using UnityEngine;

public class PortalBindPlugin : MonoBehaviour
{	
	#region Singleton

	private static PortalBindPlugin _instance;
	
	public static PortalBindPlugin Instance { get { return _instance; } }

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		} else {
			_instance = this;
		}
	}

	#endregion

	public void BindPortals(Portal PortalA, Portal PortalB)
	{
		if (PortalA.Camera.targetTexture != null)
		{
			PortalA.Camera.targetTexture.Release();
		}
		PortalA.Camera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
		PortalA.Renderer.material = null;
		PortalA.Renderer.material = new Material(Resources.Load<Shader>("Shaders/ScreenCutoutShader"));
		PortalA.Renderer.material.mainTexture = PortalA.Camera.targetTexture;

		if (PortalB.Camera.targetTexture != null)
		{
			PortalB.Camera.targetTexture.Release();
		}
		PortalB.Camera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
		PortalB.Renderer.material = null;
		PortalB.Renderer.material = new Material(Resources.Load<Shader>("Shaders/ScreenCutoutShader"));
		PortalB.Renderer.material.mainTexture = PortalB.Camera.targetTexture;

		print("Renderers are created");
		
		PortalA.Teleporter.Receiver = PortalB.Receiver;
		PortalB.Teleporter.Receiver = PortalA.Receiver;

		print("Colliders are set");
		
		PortalA.CameraMover.portal = PortalB.Root;
		PortalA.CameraMover.otherPortal = PortalA.Root;
		
		PortalB.CameraMover.portal = PortalA.Root;
		PortalB.CameraMover.otherPortal = PortalB.Root;
		
		print("Portals are ready");
	}
}
