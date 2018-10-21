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
}