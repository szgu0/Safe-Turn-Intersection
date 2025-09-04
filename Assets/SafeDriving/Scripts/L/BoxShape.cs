using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxShape : MonoBehaviour
{
    [SerializeField]
    private Vector3 boxSize;

    public bool Contains(Vector3 position)
    {
        Vector3 localPosition = transform.InverseTransformPoint(position);
        Vector3 size = transform.TransformVector(boxSize);

        return Mathf.Abs(localPosition.x) < size.x / 2 && Mathf.Abs(localPosition.y) < size.y / 2 && Mathf.Abs(localPosition.z) < size.z / 2;
    }

    public bool DeltaContains(Vector3 position)
    {
        Vector3 localPosition = position - transform.position;
        return Mathf.Abs(localPosition.x) < boxSize.x / 2 && Mathf.Abs(localPosition.y) < boxSize.y / 2 && Mathf.Abs(localPosition.z) < boxSize.z / 2;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.1f);
        Gizmos.DrawCube(transform.position, boxSize);
        Gizmos.color = new Color(0, 1, 0, 0.8f);
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
