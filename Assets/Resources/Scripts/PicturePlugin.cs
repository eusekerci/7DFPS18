using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicturePlugin : MonoBehaviour
{

	public Camera RoomCamera;
	public MeshRenderer PictureRenderer;
	
	void Start () 
	{
		if (RoomCamera.targetTexture != null)
		{
			RoomCamera.targetTexture.Release();
		}
		RoomCamera.targetTexture = new RenderTexture(256, 256, 24);
		PictureRenderer.materials[0] = new Material(Resources.Load<Shader>("Shaders/ScreenCutoutShader"));
		PictureRenderer.material.mainTexture = RoomCamera.targetTexture;
	}
	
	void Update () 
	{
		
	}
}
