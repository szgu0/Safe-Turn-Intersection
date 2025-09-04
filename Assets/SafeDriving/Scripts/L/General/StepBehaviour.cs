using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MPack;

public class StepBehaviour : MonoBehaviour
{
    public TextSetReference TextSetReference;

    [Space(5)]
    public bool ShowNextButton;
    public ValueWithEnable<int> OverrideNextButtonStep;

    [Space(5)]
    public bool ShowTriviaButton;
    public TrviaQuestion Question;


    [Header("Hint")]
    public ValueWithEnable<float> TriggerHintTime;
    public TextSetReference HintTextReference;


    [Header("Events")]
    public UnityEvent StartEvent;
    public UnityEvent EndEvent;

    public void Log(string message)
    {
        Debug.Log(message);
    }
}
