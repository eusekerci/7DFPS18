using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
	public GameObject[] Objects;
	public GameObject[] Prefabs;
	public Vector3[] Positions;
	public Vector3[] Rotations;

	public bool IsPlayerInMe;

	private void Awake()
	{
		IsPlayerInMe = false;
	}

	private void Start()
	{
		
		for (int i = 0; i < Objects.Length; i++)
		{
			
		}
	}
}
