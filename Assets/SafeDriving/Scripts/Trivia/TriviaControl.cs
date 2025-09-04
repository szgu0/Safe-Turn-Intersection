using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using MPack;
using TMPro;
using DateTime = System.DateTime;

public class TriviaControl : MonoBehaviour
{
    // public const int AnswerIsCorrect = 99,
    //                  AnswerIsWrong = 100,
    //                  AnswerIsA = 101,
    //                  AnswerIsB = 102,
    //                  AnswerIsC = 103,
    //                  AnswerIsD = 104;

    [SerializeField]
    private TrviaQuestion question;
    [SerializeField]
    private Canvas canvas;

    public bool IsOn => canvas.enabled;

    [Header("Question")]
    [SerializeField]
    private GameObject questionPanel;
    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private TextMeshProUGUI contentText;
    [SerializeField]
    private TriviaChoiceButton[] choiceButtons;
    [SerializeField]
    private Button submitButton;

    [Header("Answer")]
    [SerializeField]
    private GameObject answerPanel;
    [SerializeField]
    private TextMeshProUGUI answerTitleText;
    [SerializeField]
    private TextMeshProUGUI answerDiscriptionText;
    [SerializeField]
    private GameObject quitTriviaButton;

    [Header("Object Select")]
    [SerializeField]
    private ObjectSelectTrivia[] objectSelectTrivias;
    private int _objectSelectIndex;

    protected Coroutine _rechooseAnswerCouroutine;

    void Awake()
    {
        canvas.enabled = false;
        questionPanel.SetActive(false);
        answerPanel.SetActive(false);


        for (int i = 0; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].OnSelectedChanged += ChoiceButtonSelectedChange;
        }

        submitButton.onClick.AddListener(OnSubmitClick);
    }

    public void StartTrivia(TrviaQuestion _question)
    {
        question = _question;
        StartTrivia();
    }
    public void StartTrivia()
    {
        canvas.enabled = true;

        questionPanel.SetActive(true);
        answerPanel.SetActive(false);

        titleText.text = LevelTextData.StripUselessCharacters(question.Title);
        contentText.text = LevelTextData.StripUselessCharacters(question.Content);
        AudioManager.ins?.Play(question.QuestionClip);

        if (question.UseObjectSelection)
        {
            for (int i = 0; i < choiceButtons.Length; i++)
                choiceButtons[i].gameObject.SetActive(false);

            submitButton.gameObject.SetActive(false);

            _objectSelectIndex = -1;
            for (int i = 0; i < objectSelectTrivias.Length; i++)
            {
                if (objectSelectTrivias[i].Name == question.ObjectSelectionName)
                {
                    _objectSelectIndex = i;
                    break;
                }
            }

            if (_objectSelectIndex == -1)
                return;

            var trivia = objectSelectTrivias[_objectSelectIndex];
            trivia.Parent?.SetActive(true);

            for (int i = 0; i < trivia.Answers.Length; i++)
            {
                trivia.Answers[i].OnSelect += OnObjectSelectionSubmit;
            }
        }
        else
        {
            for (int i = 0; i < choiceButtons.Length; i++)
            {
                var button = choiceButtons[i];

                if (i >= question.Choices.Length)
                {
                    button.gameObject.SetActive(false);
                    continue;
                }

                button.gameObject.SetActive(true);

                button.Setup(i, question.Choices[i], question.AddChoicePrefix);
                button.OnSelectedChanged += ChoiceButtonSelectedChange;
            }

            submitButton.gameObject.SetActive(true);
            submitButton.interactable = false;
        }
    }

    void ChoiceButtonSelectedChange(TriviaChoiceButton button)
    {
        if (button.Selected)
        {
            for (int i = 0; i < choiceButtons.Length; i++)
            {
                if (choiceButtons[i] == button)
                    continue;

                choiceButtons[i].Selected = false;
            }
            submitButton.interactable = true;
        }
        else
        {
            submitButton.interactable = false;
        }
    }

    void OnSubmitClick()
    {
        questionPanel.SetActive(false);
        answerPanel.SetActive(true);
        quitTriviaButton.SetActive(true);

        bool isCorrect = false;
        TrviaQuestion.Choice correctChoice = new TrviaQuestion.Choice();
        string correctIndex = "";

        NewStepData.ChoiceType choiceType = NewStepData.ChoiceType.None;

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            TriviaChoiceButton button = choiceButtons[i];
            if (button.Selected)
            {
                switch (i)
                {
                    case 0:
                        choiceType = NewStepData.ChoiceType.A;
                        break;
                    case 1:
                        choiceType = NewStepData.ChoiceType.B;
                        break;
                    case 2:
                        choiceType = NewStepData.ChoiceType.C;
                        break;
                    case 3:
                        choiceType = NewStepData.ChoiceType.D;
                        break;
                }
            }

            if (button.Choice.IsCorrect)
            {
                correctChoice = button.Choice;
                isCorrect = button.Selected;

                switch (i)
                {
                    case 0:
                        correctIndex = "A ";
                        break;
                    case 1:
                        correctIndex = "B ";
                        break;
                    case 2:
                        correctIndex = "C ";
                        break;
                    case 3:
                        correctIndex = "D ";
                        break;
                }
            }
        }

        UserTask.AddTriviaStep(choiceType, isCorrect);

        if (isCorrect)
        {
            answerTitleText.text = LevelTextData.StripUselessCharacters(question.CorrectTitle);
            answerDiscriptionText.text = LevelTextData.StripUselessCharacters(question.CorrectDiscription);

            AudioManager.ins?.Play(question.CorrectClip);


            if (question.CorrectPlayExplain && question.DetailExplain)
                AudioManager.ins?.QueuedPlay(question.DetailExplain);
        }
        else
        {
            answerTitleText.text = LevelTextData.StripUselessCharacters(question.IncorrectTitle);
            answerDiscriptionText.text = LevelTextData.StripUselessCharacters(string.Format(question.IncorrectDiscription, correctIndex + correctChoice.Content));

            AudioManager.ins?.Play(question.IncorrectClip);

            if (question.DetailExplain)
                AudioManager.ins?.QueuedPlay(question.DetailExplain);
        }
    }

    void OnObjectSelectionSubmit(bool isCorrect)
    {
        if (_rechooseAnswerCouroutine != null)
        {
            StopCoroutine(_rechooseAnswerCouroutine);
        }

        var trivia = objectSelectTrivias[_objectSelectIndex];
        if (!isCorrect && question.RechooseUntilCorrect)
        {
            _rechooseAnswerCouroutine = StartCoroutine(A());
            return;
        }

        trivia.Parent?.SetActive(false);

        questionPanel.SetActive(false);
        answerPanel.SetActive(true);
        quitTriviaButton.SetActive(true);

        for (int i = 0; i < trivia.Answers.Length; i++)
        {
            trivia.Answers[i].OnSelect -= OnObjectSelectionSubmit;
        }

        if (isCorrect)
        {
            answerTitleText.text = LevelTextData.StripUselessCharacters(question.CorrectTitle);
            answerDiscriptionText.text = LevelTextData.StripUselessCharacters(question.CorrectDiscription);

            AudioManager.ins?.Play(question.CorrectClip);


            if (question.CorrectPlayExplain && question.DetailExplain)
                AudioManager.ins?.QueuedPlay(question.DetailExplain);
        }
        else
        {
            answerTitleText.text = LevelTextData.StripUselessCharacters(question.IncorrectTitle);
            answerDiscriptionText.text = LevelTextData.StripUselessCharacters(question.IncorrectDiscription);

            AudioManager.ins?.Play(question.IncorrectClip);

            if (question.DetailExplain)
                AudioManager.ins?.QueuedPlay(question.DetailExplain);
        }
    }

    IEnumerator A()
    {
        questionPanel.SetActive(false);
        answerPanel.SetActive(true);
        quitTriviaButton.SetActive(false);

        AudioManager.ins?.Play(question.IncorrectClip);

        answerTitleText.text = LevelTextData.StripUselessCharacters(question.IncorrectTitle);
        answerDiscriptionText.text = LevelTextData.StripUselessCharacters(question.IncorrectDiscription);

        yield return new WaitForSeconds(3);

        questionPanel.SetActive(true);
        answerPanel.SetActive(false);
        quitTriviaButton.SetActive(true);

        _rechooseAnswerCouroutine = null;
    }

    public void ResetForNextQuestion()
    {
        canvas.enabled = false;

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].ResetChoice();
        }
    }

    public void ForceCorrect()
    {
        questionPanel.SetActive(false);
        answerPanel.SetActive(true);
        quitTriviaButton.SetActive(true);

        // UserTask.addStepDataToNowTask(AnswerIsA, DateTime.Now);

        answerTitleText.text = LevelTextData.StripUselessCharacters(question.CorrectTitle);
        answerDiscriptionText.text = LevelTextData.StripUselessCharacters(question.CorrectDiscription);

        AudioManager.ins?.Play(question.CorrectClip);

        UserTask.AddTriviaStep(NewStepData.ChoiceType.None, true);


        if (question.CorrectPlayExplain && question.DetailExplain)
            AudioManager.ins?.QueuedPlay(question.DetailExplain);
    }


    [System.Serializable]
    public struct ObjectSelectTrivia
    {
        public string Name;
        public GameObject Parent;
        public SelectableAnswer[] Answers;
    }
}
