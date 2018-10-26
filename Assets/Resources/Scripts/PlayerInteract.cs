using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public enum PlayerInteractStates
{
	Observe, 
	Drag, 
	Carry,
	TeleportChannel
};

public class PlayerClickLinker : MyEvent
{
	public Collider Col;
}

public class PlayerDropLinker : MyEvent
{
	public Collider Col;
}

public class PlayerStartCarry : MyEvent
{
	public Collider Col;
}

public class PlayerDropCarry : MyEvent
{
	public Vector3 PutPoint;
	public Vector3 Normal;
}

public class PlayerLinkerReset : MyEvent { }

public class PlayerPlaneTeleportStart : MyEvent
{
	public Collider Col;
}

public class PlayerInteract : MonoBehaviour
{

	public float InteractDistance;
	public Crosshair Crosshair;
	public PlayerInteractStates State;
	public LayerMask InteractLayers;

	void Start()
	{
		State = PlayerInteractStates.Observe;
	}
	
	void Update ()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		if (Physics.Raycast(ray, out hit, InteractDistance, InteractLayers))
		{
			if (hit.collider != null)
			{
				if (State == PlayerInteractStates.Observe)
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
					}
					else if (hit.collider.CompareTag("Box"))
					{
						Crosshair.Hide = false;
						Crosshair.CrosshairDefault = Crosshair.Crosshair02;
						if (Input.GetMouseButtonDown(0))
						{
							State = PlayerInteractStates.Carry;

							MessageBus.Publish(new PlayerStartCarry()
							{
								Col = hit.collider
							});
						}
					}
					else if (hit.collider.CompareTag("PlaneLinker"))
					{
						Crosshair.Hide = false;
						Crosshair.CrosshairDefault = Crosshair.Crosshair04;	

						if (Input.GetMouseButtonDown(0))
						{
							State = PlayerInteractStates.TeleportChannel;
							
							MessageBus.Publish(new PlayerPlaneTeleportStart()
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
				else if (State == PlayerInteractStates.Carry)
				{
					Crosshair.Hide = false;
					Crosshair.CrosshairDefault = Crosshair.Crosshair01;
					if (Input.GetMouseButtonDown(0))
					{
						State = PlayerInteractStates.Observe;
				
						print(hit.collider.gameObject.name);
						MessageBus.Publish(new PlayerDropCarry()
						{
							PutPoint = hit.point,
							Normal = hit.normal
						});						
					}
				}
				else if (State == PlayerInteractStates.Drag)
				{
					Crosshair.Hide = false;
					Crosshair.CrosshairDefault = Crosshair.Crosshair03;
					if (hit.collider.CompareTag("PortalLinker"))
					{
						if (Input.GetMouseButtonUp(0))
						{
							State = PlayerInteractStates.Observe;

							MessageBus.Publish(new PlayerDropLinker()
							{
								Col = hit.collider
							});
						}
					}
					else
					{
						if (Input.GetMouseButtonUp(0))
						{
							State = PlayerInteractStates.Observe;				
							MessageBus.Publish(new PlayerLinkerReset());
						}
					}
				}
				else if (State == PlayerInteractStates.TeleportChannel)
				{
					Crosshair.Hide = true;
				}
			}
			else
			{
				Crosshair.Hide = true;
			}
		}
		else
		{
			if (State == PlayerInteractStates.Drag)
			{
				Crosshair.Hide = false;
				Crosshair.CrosshairDefault = Crosshair.Crosshair01;

				State = PlayerInteractStates.Observe;				
				MessageBus.Publish(new PlayerLinkerReset());
			}
			else if (State == PlayerInteractStates.Observe || State == PlayerInteractStates.Carry)
			{
				Crosshair.Hide = true;
			}
			else if (State == PlayerInteractStates.TeleportChannel)
			{
				State = PlayerInteractStates.Observe;
			}
		}							
	}
}
