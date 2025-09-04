using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using MPack;
using QuickOutline;


public class SelectableAnswer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private Outline outline;
    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    private ColorReference normalColor;
    [SerializeField]
    private ColorReference hoverColor;
    [SerializeField]
    private ColorReference clickColor;

    public bool IsCorrectAnswer;

    public event System.Action<bool> OnSelect;

    private MaterialPropertyBlock _propertyBlock;

    void Awake()
    {
        // if (!outline)
        //     outline = GetComponent<Outline>();
        if (meshRenderer)
        {
            _propertyBlock = new MaterialPropertyBlock();
            _propertyBlock.SetColor("_BaseColor", normalColor.Value);
            meshRenderer.SetPropertyBlock(_propertyBlock);
        }
        else if (outline)
            outline.OutlineColor = normalColor.Value;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSelect?.Invoke(IsCorrectAnswer);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!enabled)
            return;
        if (meshRenderer)
        {
            _propertyBlock = new MaterialPropertyBlock();
            _propertyBlock.SetColor("_BaseColor", hoverColor.Value);
            meshRenderer.SetPropertyBlock(_propertyBlock);
        }
        if (outline)
            outline.OutlineColor = hoverColor.Value;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!enabled)
            return;
        if (meshRenderer)
        {
            _propertyBlock = new MaterialPropertyBlock();
            _propertyBlock.SetColor("_BaseColor", normalColor.Value);
            meshRenderer.SetPropertyBlock(_propertyBlock);
        }
        if (outline)
            outline.OutlineColor = normalColor.Value;
    }
}
