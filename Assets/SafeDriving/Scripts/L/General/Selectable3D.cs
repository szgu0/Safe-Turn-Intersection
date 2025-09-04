using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Selectable3D : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private NewOutline outline;

    [SerializeField]
    private Color normalColor, hoverColor, pressedColor, disabledColor;

    public UnityEvent OnClick;

    [field: SerializeField]
    private bool interactble { get; set; }


    public void SetInteractable(bool interactable)
    {
        if (this.interactble == interactable)
            return;

        this.interactble = interactable;

        outline?.SetOutlineColor(interactable ? normalColor : disabledColor);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!interactble)
            return;

        outline?.SetOutlineColor(hoverColor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!interactble)
            return;

        outline?.SetOutlineColor(normalColor);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!interactble)
            return;

        outline?.SetOutlineColor(pressedColor);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!interactble)
            return;

        outline?.SetOutlineColor(hoverColor);
        OnClick.Invoke();
    }
}
