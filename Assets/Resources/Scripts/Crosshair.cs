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
	public Texture Crosshair05 = null;

	public float CrossHairMaxMultiplier;
	public float CrossHairMinMultiplier;
	
	public bool Hide = false;
		
	private float _crossHairCurrentMultiplier;
	private bool _isIncreasing;

	void OnGUI()
	{

		if (CrosshairDefault == null)
			return;

		if (Hide)
			return;

		GUI.color = new Color(1, 1, 1, 0.8f);
		GUI.DrawTexture(new Rect((Screen.width * 0.5f) - (CrosshairDefault.width * 0.5f * _crossHairCurrentMultiplier),
			(Screen.height * 0.5f) - (CrosshairDefault.height * 0.5f * _crossHairCurrentMultiplier), CrosshairDefault.width * _crossHairCurrentMultiplier,
			CrosshairDefault.height * _crossHairCurrentMultiplier), CrosshairDefault);
		GUI.color = Color.white;

	}

	private void Start()
	{
		_isIncreasing = false;
		_crossHairCurrentMultiplier = 1;
	}

	private void FixedUpdate()
	{
		if (CrosshairDefault == Crosshair04 || CrosshairDefault == Crosshair05)
		{
			if (_isIncreasing)
			{
				_crossHairCurrentMultiplier += Time.fixedDeltaTime;
				if (_crossHairCurrentMultiplier > CrossHairMaxMultiplier)
				{
					_isIncreasing = false;
					_crossHairCurrentMultiplier = CrossHairMaxMultiplier;
				}
			}
			else
			{
				_crossHairCurrentMultiplier -= Time.fixedDeltaTime;
				if (_crossHairCurrentMultiplier < CrossHairMinMultiplier)
				{
					_isIncreasing = true;
					_crossHairCurrentMultiplier = CrossHairMinMultiplier;
				}
			}
		}
		else
		{
			_crossHairCurrentMultiplier = 1;
		}
	}
}
