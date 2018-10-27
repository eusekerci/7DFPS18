using System.Collections;
using System.Collections.Generic;
using ProBuilder2.Common;
using UnityEngine;
using UnityEditor.SceneManagement;

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

		Application.targetFrameRate = 120;
	}

	#endregion
	
	public Portal InitA;
	public Portal InitB;
	
	public void Start()
	{
		EditorSceneManager.preventCrossSceneReferences = false;
		BindPortals(InitA, InitB);
	}
	
	public void BindPortals(Portal PortalA, Portal PortalB)
	{
		if (PortalA.ConnectedPortal != null)
		{
			ResetPortal(PortalA.ConnectedPortal);
		}
		if (PortalB.ConnectedPortal != null)
		{
			ResetPortal(PortalB.ConnectedPortal);
		}
		
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
		PortalA.CameraMover.renderer = PortalA.Renderer.transform;
		PortalA.CameraMover.otherRenderer = PortalB.Renderer.transform;
		
		PortalB.CameraMover.portal = PortalA.Root;
		PortalB.CameraMover.otherPortal = PortalB.Root;
		PortalB.CameraMover.renderer = PortalB.Renderer.transform;
		PortalB.CameraMover.otherRenderer = PortalA.Renderer.transform;

		PortalA.ConnectedPortal = PortalB;
		PortalB.ConnectedPortal = PortalA;
		
		PortalA.ChangeIconColor(PortalB.IconMaterial);
		PortalB.ChangeIconColor(PortalA.IconMaterial);
		
		print("Portals are ready");
	}

	public void ResetPortal(Portal PortalA)
	{
		if (PortalA.Camera.targetTexture != null)
		{
			PortalA.Camera.targetTexture.Release();
		}

		PortalA.Teleporter.Receiver = null;
		PortalA.Renderer.material = null;
		PortalA.Renderer.material = new Material(Resources.Load<Material>("Materials/DoorBlack"));
		PortalA.Renderer.material.mainTexture = null;

		PortalA.CameraMover.portal = null;
		PortalA.CameraMover.otherPortal = null;
		PortalA.CameraMover.renderer = null;
		PortalA.CameraMover.otherRenderer = null;
		
		PortalA.ChangeIconColor(PortalA.Renderer.material);

		PortalA.ConnectedPortal = null;
	}
}
