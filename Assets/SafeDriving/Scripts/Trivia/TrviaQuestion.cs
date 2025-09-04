using UnityEngine;
using UnityEngine.Serialization;
using MPack;


[CreateAssetMenu]
public class TrviaQuestion : ScriptableObject
{
    public string Title;
    [TextArea]
    public string Content;
    public AudioClip QuestionClip;

    public bool AddChoicePrefix = true;
    public Choice[] Choices;

    public bool UseObjectSelection;
    public string ObjectSelectionName;

    public bool RechooseUntilCorrect;

    [Space(8)]
    [Header("Correct Window")]
    public string CorrectTitle;
    [TextArea]
    public string CorrectDiscription;
    public AudioClip CorrectClip;
    public bool CorrectPlayExplain = false;

    [Space(8)]
    [Header("Incorrect Window")]
    public string IncorrectTitle;
    [TextArea]
    [FormerlySerializedAs("WrongAnswerExplain")]
    public string IncorrectDiscription;
    public AudioClip IncorrectClip;

    public AudioClip DetailExplain;

    // [Header("Audio")]
    // public AudioClipSet QuestionAudio;
    // public AudioClipSet CorrectAudio;
    // public AudioClipSet IncorrectAudio;

    public void StartTrivia()
    {
        // FindObjectOfType<TriviaControl>().StartTrivia(this);
        FindObjectOfType<StepsGuide>().StartTrivia(this);
    }

    [System.Serializable]
    public struct Choice
    {
        [Multiline]
        public string Content;
        public bool IsCorrect;
    }
}
