using UnityEngine;

[ExecuteInEditMode]
public class CopyPositionEditor : MonoBehaviour
{
    public Transform sourceObject;
    public Transform targetObject;

    void Update()
    {
        if (sourceObject == null)
        {
            sourceObject = Camera.main.transform;
        }
        if (sourceObject != null && targetObject != null)
        {
            targetObject.position = sourceObject.position;
            // targetObject.rotation = sourceObject.rotation;
            targetObject.rotation = Quaternion.Euler(0f, sourceObject.rotation.y, sourceObject.rotation.z);;
        }
    }
}
