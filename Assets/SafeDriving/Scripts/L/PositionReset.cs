using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MPack;

public class PositionReset : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Rigidbody targetRigidbody;
    [SerializeField]
    private BoxShape boxShape;

    private Vector3 _originalPosition;
    private Quaternion _originalRotation;

    private Rigidbody _rigidbody;

    void Awake()
    {
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;
    }

    void LateUpdate()
    {
        if (boxShape.DeltaContains(transform.position))
            return;

        target.position = _originalPosition;
        target.rotation = _originalRotation;

        if (targetRigidbody)
        {
            targetRigidbody.velocity = Vector3.zero;
            targetRigidbody.angularVelocity = Vector3.zero;
        }
    }

    public void ForceReset()
    {
        target.position = _originalPosition;
        target.rotation = _originalRotation;

        if (targetRigidbody)
        {
            targetRigidbody.velocity = Vector3.zero;
        }
    }
}
