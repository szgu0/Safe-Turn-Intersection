using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;


public class Button3D : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [Header("Components")]
    public SpriteRenderer spriteRenderer;
    public MeshRenderer meshRenderer;
    public TextMeshPro textRenderer;

    [Header("Color Changes")]
    public bool changeColor;
    public bool colorIsOverlay;
    public Color exitColor, enterColor, holdColor;
    private Color _defaultExitColor, _defaultEnterColor, _defaultHoldColor;

    [Header("Sprite Chage")]
    public Sprite enterSprite;
    public Sprite holdSprite;
    private Sprite _defaultSprite;

    [Header("Events")]
    public UnityEvent OnMouseButtonEnter;
    public UnityEvent OnMouseButtonExit;
    public UnityEvent OnMouseButtonDown;
    public UnityEvent OnMouseButtonUp;

    private ButtonState _state;
    private enum ButtonState { None, Hover, Hold }

    private bool _canInteract = true;
    public bool canInteract
    {
        get => _canInteract;
        set
        {
            _canInteract = value;
            gameObject.layer = _canInteract ? _interactableLayer : _uninteractableLayer;
        }
    }


    private int _interactableLayer;
    private int _uninteractableLayer;

    void Awake()
    {
        _interactableLayer = LayerMask.NameToLayer("UI");
        _uninteractableLayer = LayerMask.NameToLayer("Ignore Raycast");

        if (meshRenderer == null)
            meshRenderer = GetComponentInChildren<MeshRenderer>();

        _defaultExitColor = exitColor;
        _defaultEnterColor = enterColor;
        _defaultHoldColor = holdColor;
        SetBaseColor(textRenderer != null ? textRenderer.color : meshRenderer.material.color);

        if (spriteRenderer) _defaultSprite = spriteRenderer.sprite;
    }

    Color MultiplyColor(Color color1, Color color2)
    {
        return new Color(color1.r * color2.r, color1.g * color2.g, color1.b * color2.b, color1.a * color2.a);
    }

    public void SetBaseColor(Color newBaseColor)
    {
        if (colorIsOverlay)
        {
            exitColor = MultiplyColor(_defaultExitColor, newBaseColor);
            enterColor = MultiplyColor(_defaultEnterColor, newBaseColor);
            holdColor = MultiplyColor(_defaultHoldColor, newBaseColor);
        }

        SetRendererColor(_state == ButtonState.None ? exitColor : _state == ButtonState.Hover ? enterColor : holdColor);
    }

    public void SetBaseColor(Material material, Color newBaseColor)
    {
        if (colorIsOverlay)
        {
            exitColor = MultiplyColor(_defaultExitColor, newBaseColor);
            enterColor = MultiplyColor(_defaultEnterColor, newBaseColor);
            holdColor = MultiplyColor(_defaultHoldColor, newBaseColor);
        }

        if (meshRenderer)
            meshRenderer.material = material;
        SetRendererColor(_state == ButtonState.None ? exitColor : _state == ButtonState.Hover ? enterColor : holdColor);
    }

    public void SetRendererColor(Color color)
    {
        if (!changeColor)
            return;

        if (spriteRenderer != null)
            spriteRenderer.color = color;
        if (meshRenderer != null)
        {
            meshRenderer.material.SetColor("_Color", color);
            meshRenderer.material.SetColor("_BaseColor", color);
        }
        if (textRenderer != null) textRenderer.color = color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetRendererColor(enterColor);
        OnMouseButtonEnter.Invoke();

        _state = ButtonState.Hover;

        if (spriteRenderer && enterSprite) spriteRenderer.sprite = enterSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetRendererColor(exitColor);
        OnMouseButtonExit.Invoke();

        _state = ButtonState.None;

        if (spriteRenderer) spriteRenderer.sprite = _defaultSprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        SetRendererColor(holdColor);
        OnMouseButtonDown.Invoke();

        _state = ButtonState.Hold;

        if (spriteRenderer && holdSprite) spriteRenderer.sprite = holdSprite;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            _state = ButtonState.Hover;

            SetRendererColor(enterColor);
            OnMouseButtonUp.Invoke();

            if (spriteRenderer && enterSprite) spriteRenderer.sprite = enterSprite;
        }
    }

    public void Log(string message)
    {
        Debug.Log(message);
    }
}
