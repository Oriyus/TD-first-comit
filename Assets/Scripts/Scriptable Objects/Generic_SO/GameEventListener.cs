using UnityEngine;
using UnityEngine.Events;
using System;

[Serializable]
public class TypeEvent<T> : UnityEvent<T> { }

public class GameEventListener<T> : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public GameEvent<T> Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public TypeEvent<T> Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(T value)
    {
        Response?.Invoke(value);
    }
}