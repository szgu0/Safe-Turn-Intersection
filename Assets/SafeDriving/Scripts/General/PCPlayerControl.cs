using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PCPlayerControl : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 1f;

    private GameObject _lastSelected;
    private bool _enableRotation = true;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        if (Mathf.Abs(x) > 0.01f)
        {
            if (_lastSelected != EventSystem.current.currentSelectedGameObject)
            {
                _lastSelected = EventSystem.current.currentSelectedGameObject;

                if (_lastSelected)
                    _enableRotation = _lastSelected.GetComponent<TMP_InputField>() == null &&  _lastSelected.GetComponent<InputField>() == null;
                else
                    _enableRotation = true;
            }

            if (_enableRotation)
                transform.Rotate(0, x * rotateSpeed * Time.deltaTime, 0);
        }
    }
}
