using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class LifeCycleListener : MonoBehaviour
{
    public UnityEvent OnAwakeEvent;
    public UnityEvent OnStartEvent;

    public UnityEvent OnEnableEvent;
    public UnityEvent OnDisableEvent;

    void Awake()
    {
        OnAwakeEvent.Invoke();
    }

    void Start()
    {
        OnStartEvent.Invoke();
    }

    void OnEnable()
    {
        OnEnableEvent.Invoke();
    }

    void OnDisable()
    {
        OnDisableEvent.Invoke();
    }
}
