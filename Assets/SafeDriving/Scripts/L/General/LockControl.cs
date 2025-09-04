using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LockControl : MonoBehaviour
{
    [SerializeField]
    private int lockCount;

    [SerializeField]
    private UnityEvent allUnlockEvent;

    private bool[] _locks;

    void Awake()
    {
        _locks = new bool[lockCount];
        for (int i = 0; i < _locks.Length; i++)
            _locks[i] = false;
    }

    public void Unlock(int index)
    {
        _locks[index] = true;

        for (int i = 0; i < _locks.Length; i++)
            if (!_locks[i])
                return;

        allUnlockEvent.Invoke();
    }


    public void Lock(int index)
    {
        _locks[index] = false;
    }
}
