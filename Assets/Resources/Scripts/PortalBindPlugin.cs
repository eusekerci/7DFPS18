using System.Collections;
using System.Collections.Generic;
using ProBuilder2.Common;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class Pair<T, U> {
	public Pair() {
	}

	public Pair(T first, U second) {
		this.First = first;
		this.Second = second;
	}

	public T First { get; set; }
	public U Second { get; set; }
};

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
		EditorSceneManager.preventCrossSceneReferences = false;
	}

	#endregion
	
	public string InitA;
	public string InitB;

	private List<Pair<string, string>> Binds;
	
	public void Start()
	{
		Binds = new List<Pair<string, string>>();
		BindPortals(GameObject.Find(InitA).GetComponent<Portal>(), GameObject.Find(InitB).GetComponent<Portal>());
	}
	
	public void BindPortals(Portal portalA, Portal portalB, bool reOpen = false)
	{
		if (portalA.ConnectedPortal != null)
		{
			ResetPortal(portalA.ConnectedPortal);
		}
		if (portalB.ConnectedPortal != null)
		{
			ResetPortal(portalB.ConnectedPortal);
		}
		
		if (portalA.Camera.targetTexture != null)
		{
			portalA.Camera.targetTexture.Release();
		}
		portalA.Camera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
		portalA.Renderer.material = null;
		portalA.Renderer.material = new Material(Resources.Load<Shader>("Shaders/ScreenCutoutShader"));
		portalA.Renderer.material.mainTexture = portalA.Camera.targetTexture;

		if (portalB.Camera.targetTexture != null)
		{
			portalB.Camera.targetTexture.Release();
		}
		portalB.Camera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
		portalB.Renderer.material = null;
		portalB.Renderer.material = new Material(Resources.Load<Shader>("Shaders/ScreenCutoutShader"));
		portalB.Renderer.material.mainTexture = portalB.Camera.targetTexture;

		print("Renderers are created");
		
		portalA.Teleporter.Receiver = portalB.Receiver;
		portalB.Teleporter.Receiver = portalA.Receiver;

		print("Colliders are set");
		
		portalA.CameraMover.portal = portalB.Root;
		portalA.CameraMover.otherPortal = portalA.Root;
		portalA.CameraMover.renderer = portalA.Renderer.transform;
		portalA.CameraMover.otherRenderer = portalB.Renderer.transform;
		
		portalB.CameraMover.portal = portalA.Root;
		portalB.CameraMover.otherPortal = portalB.Root;
		portalB.CameraMover.renderer = portalB.Renderer.transform;
		portalB.CameraMover.otherRenderer = portalA.Renderer.transform;

		portalA.ConnectedPortal = portalB;
		portalB.ConnectedPortal = portalA;
		
		portalA.ChangeIconColor(portalB.IconMaterial);
		portalB.ChangeIconColor(portalA.IconMaterial);
		
		if(!reOpen)
			Binds.Add(new Pair<string, string> (portalA.gameObject.name, portalB.gameObject.name));
		
		print("Portals are ready");
	}

	public void ResetPortal(Portal portalA)
	{
		if (portalA.Camera.targetTexture != null)
		{
			portalA.Camera.targetTexture.Release();
		}

		portalA.Teleporter.Receiver = null;
		portalA.Renderer.material = null;
		portalA.Renderer.material = new Material(Resources.Load<Material>("Materials/DoorBlack")) {mainTexture = null};
		portalA.CameraMover.portal = null;
		portalA.CameraMover.otherPortal = null;
		portalA.CameraMover.renderer = null;
		portalA.CameraMover.otherRenderer = null;
		
		portalA.ChangeIconColor(portalA.Renderer.material);

		portalA.ConnectedPortal = null;

		for (int i = 0; i < Binds.Count; i++)
		{
			if (Binds[i].First != portalA.gameObject.name && Binds[i].Second != portalA.gameObject.name) continue;
			Binds.RemoveAt(i);
			break;
		}
	}

	public void RestoreBind(Portal portalA)
	{
		for (int i = 0; i < Binds.Count; i++)
		{
			if (Binds[i].First == portalA.gameObject.name)
			{
				BindPortals(portalA, GameObject.Find(Binds[i].Second).GetComponent<Portal>());
			}
			else if (Binds[i].Second == portalA.gameObject.name)
			{
				BindPortals(portalA, GameObject.Find(Binds[i].First).GetComponent<Portal>());
			}				
		}	
	}
}
