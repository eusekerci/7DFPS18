using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{

	public Texture CrosshairDefault = null;
	public Texture Crosshair01 = null;
	public Texture Crosshair02 = null;
	public Texture Crosshair03 = null;
	public Texture Crosshair04 = null;

	public bool Hide = false;

	void OnGUI()
	{

		if (CrosshairDefault == null)
			return;

		if (Hide)
			return;

		GUI.color = new Color(1, 1, 1, 0.8f);
		GUI.DrawTexture(new Rect((Screen.width * 0.5f) - (CrosshairDefault.width * 0.5f),
			(Screen.height * 0.5f) - (CrosshairDefault.height * 0.5f), CrosshairDefault.width,
			CrosshairDefault.height), CrosshairDefault);
		GUI.color = Color.white;
	}
}
