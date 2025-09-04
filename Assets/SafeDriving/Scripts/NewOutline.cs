using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewOutline : MonoBehaviour
{
    private const string OutlineColorName = "_OutlineColor", OutlineWidthName = "_OutlineWidth";

    [SerializeField]
    private float width = 0.02f;
    [SerializeField]
    private Color color;

    private Renderer _renderer;
    private MaterialPropertyBlock _propBlock;

    void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _propBlock = new MaterialPropertyBlock();
    }

    public void SetOutline(Color color, float width)
    {
        if (_propBlock == null)
            return;

        this.width = width;
        this.color = color;
        _propBlock.SetColor(OutlineColorName, color);
        _propBlock.SetFloat(OutlineWidthName, width);
        _renderer.SetPropertyBlock(_propBlock);
    }


    public void SetOutlineColor(Color color)
    {
        this.color = color;
        _propBlock.SetColor(OutlineColorName, color);
        _renderer.SetPropertyBlock(_propBlock);
    }

    void OnValidate()
    {
        SetOutline(color, width);
    }
}
