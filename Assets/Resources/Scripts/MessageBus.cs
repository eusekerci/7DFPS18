using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class MyEvent { }

public static class MessageBus
{
	public static void Publish<T>(T evnt) where T : MyEvent 
	{
		UniRx.MessageBroker.Default.Publish(evnt);
	}

	public static UniRx.IObservable<T> OnEvent<T>() where T : MyEvent 
	{
		return UniRx.MessageBroker.Default.Receive<T>();
	}

	public static void ClearAllEvents()
	{
		Type[] allTypes = GetChildrenTypesOf<MyEvent>();
		foreach (var type in allTypes)
		{
			UniRx.MessageBroker.Default.Remove(type);
		}
	}
	
	public static Type[] GetChildrenTypesOf<T>()
	{
		return (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
			from assemblyType in domainAssembly.GetTypes()
			where typeof(T).IsAssignableFrom(assemblyType) && assemblyType != typeof(T)
			select assemblyType).ToArray();
	}
}