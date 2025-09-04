using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;


// [RequireComponent(typeof(SpriteRenderer))]
public class SpriteButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private SpriteRenderer[] _spriteRenderers;

    [field:SerializeField]
    private bool interactble;
    [SerializeField]
    private Color normalColor, hoverColor, pressedColor, disabledColor;

    public UnityEvent OnClick;
    public UnityEvent OnDisable;

    void Awake()
    {
        if (_spriteRenderers == null || _spriteRenderers.Length == 0)
            _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    public void SetInteractable(bool interactable)
    {
        if (this.interactble == interactable)
            return;

        this.interactble = interactable;

        foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
        {
            spriteRenderer.color = interactable ? normalColor : disabledColor;
        }

        if (!interactable)
            OnDisable.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!interactble)
            return;

        foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
        {
            spriteRenderer.color = hoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!interactble)
            return;
        foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
        {
            spriteRenderer.color = normalColor;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!interactble)
            return;
        foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
        {
            spriteRenderer.color = pressedColor;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!interactble)
            return;
        foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
        {
            spriteRenderer.color = hoverColor;
        }

        OnClick.Invoke();
    }
}
