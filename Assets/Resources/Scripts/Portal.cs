using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
	public Transform Root;
	public Camera Camera;
	public Transform Receiver;
	public MeshRenderer Renderer;
	public PortalTeleporter Teleporter;
	public PortalCamera CameraMover;
	public Portal ConnectedPortal;
	public Material IconMaterial;
	
	public void ChangeIconColor(Material newMaterial)
	{
		Renderer iconRenderer = transform.Find("GFX/Icon").GetComponent<Renderer>();

		iconRenderer.material = newMaterial;
	}
}