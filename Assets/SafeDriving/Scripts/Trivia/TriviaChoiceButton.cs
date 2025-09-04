using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TriviaChoiceButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI content;

    [SerializeField]
    private GameObject selectedBackground;

    public event System.Action<TriviaChoiceButton> OnSelectedChanged;

    private Button _button;
    private TrviaQuestion.Choice _choice;
    public TrviaQuestion.Choice Choice => _choice;

    private bool _selected;
    public bool Selected
    {
        get => _selected;
        set {
            _selected = value;
            selectedBackground.SetActive(value);
        }
    }

    void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ToggleChoice);

        selectedBackground.SetActive(false);
    }

    public void Setup(int index, TrviaQuestion.Choice choice, bool addPrefix=true)
    {
        _choice = choice;

        if (!content)
            return;

        if (addPrefix)
        {
            switch (index) {
                case 0:
                    content.text = "A " + choice.Content;
                    break;
                case 1:
                    content.text = "B " + choice.Content;
                    break;
                case 2:
                    content.text = "C " + choice.Content;
                    break;
                case 3:
                    content.text = "D " + choice.Content;
                    break;
            }
        }
        else
            content.text = choice.Content;
    }

    void ToggleChoice()
    {
        Selected = !_selected;
        
        OnSelectedChanged?.Invoke(this);
    }

    public void ResetChoice()
    {
        Selected = false;
    }
}
