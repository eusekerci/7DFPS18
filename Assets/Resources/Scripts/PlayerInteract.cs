using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public enum PlayerInteractStates
{
	Observe, 
	Drag, 
	Drop
};

public class PlayerClickLinker : MyEvent
{
	public Collider Col;
}

public class PlayerDropLinker : MyEvent
{
	public Collider Col;
}

public class PlayerLinkerReset : MyEvent { }

public class PlayerInteract : MonoBehaviour
{

	public float InteractDistance;
	public Crosshair Crosshair;
	public PlayerInteractStates State;

	void Start()
	{
		State = PlayerInteractStates.Observe;
	}
	
	void Update ()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		if (Physics.Raycast(ray, out hit))
		{
			if (hit.collider != null && hit.distance < InteractDistance)
			{
				if (hit.collider.CompareTag("Picture"))
				{
					Crosshair.Hide = false;
					Crosshair.CrosshairDefault = Crosshair.Crosshair01;
				}
				else if (hit.collider.CompareTag("PortalLinker"))
				{
					Crosshair.Hide = false;
					Crosshair.CrosshairDefault = Crosshair.Crosshair02;
					if (Input.GetMouseButtonDown(0))
					{
						State = PlayerInteractStates.Drag;
						
						MessageBus.Publish(new PlayerClickLinker()
						{
							Col = hit.collider
						});
					}
					else if (Input.GetMouseButtonUp(0))
					{
						State = PlayerInteractStates.Drop;
						
						MessageBus.Publish(new PlayerDropLinker()
						{
							Col = hit.collider
						});
					}
				}
				else
				{
					Crosshair.Hide = true;
				}
			}
			else
			{
				Crosshair.Hide = true;
			}
		}
					
		if (State == PlayerInteractStates.Drag)
		{
			Crosshair.Hide = false;
			Crosshair.CrosshairDefault = Crosshair.Crosshair03;
			if (Input.GetMouseButtonUp(0))
			{
				State = PlayerInteractStates.Drop;
				
				MessageBus.Publish(new PlayerLinkerReset());
			}
		}
	}
}
