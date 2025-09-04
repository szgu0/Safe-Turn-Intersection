using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayEvent : MonoBehaviour
{
    public float defaultDelay = 1f;
    public UnityEvent OnTrigger;

    private Coroutine _coroutine;

    public void Trigger()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(TriggerCoroutine(defaultDelay));
    }

    public void Trigger(float delay)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(TriggerCoroutine(delay));
    }

    public void Stop()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator TriggerCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        OnTrigger.Invoke();
        _coroutine = null;
    }
}
