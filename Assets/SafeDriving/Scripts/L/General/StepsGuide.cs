using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DateTime = System.DateTime;
using HTC.UnityPlugin.Vive;

public class StepsGuide : MonoBehaviour
{
    public static bool s_isFreeMode;

    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private TextMeshProUGUI hintText;
    [SerializeField]
    private TriviaControl triviaControl;
    [SerializeField]
    private GameObject nextButton;
    [SerializeField]
    private GameObject triviaButton;
    [SerializeField]
    private GameObject menuButton;

    [Header("Steps")]
    [Space(8)]
    [SerializeField]
    private StepBehaviour[] teachingSteps;

    [Space(8)]
    [SerializeField]
    private StepBehaviour[] freeManipulateSteps;

#if UNITY_EDITOR
    [Header("Editor Only")]
    [SerializeField]
    private bool isFreeMode;
#endif

    private int _currentStep;
    private string _sceneName;

    private int _triviaComeBackStepIndex;

    private Coroutine _waitDisplayHint;

    void Awake()
    {
        if (triviaControl == null)
            triviaControl = FindObjectOfType<TriviaControl>();

        text.text = "";
        hintText.text = "";
        nextButton.SetActive(false);
    }

    void Start()
    {
        s_isFreeMode = MenuUIEvent.myMenuState ==  MenuState.FreeMenu;
#if UNITY_EDITOR
        s_isFreeMode = isFreeMode;
#endif

        _sceneName = SceneManager.GetActiveScene().name;

        // 創建任務資料
        if (!s_isFreeMode)
            UserTask.createTaskData(_sceneName);

        ExcuteCurrentStep();

        BasicGrabbable[] grabbables = FindObjectsOfType<BasicGrabbable>(true);
        for (int i = 0; i < grabbables.Length; i++)
        {
            GameObject grabbableObj = grabbables[i].gameObject;
            grabbables[i].afterGrabbed.AddListener(delegate { OnGrabBegin(grabbableObj); });
            grabbables[i].onDrop.AddListener(delegate { OnGrabbableGrabEnd(grabbableObj); });
        }

        Draggable[] draggables = FindObjectsOfType<Draggable>(true);

        for (int i = 0; i < draggables.Length; i++)
        {
            GameObject draggableObj = draggables[i].gameObject;
            draggables[i].afterGrabbed.AddListener(delegate { OnGrabBegin(draggableObj); });
            draggables[i].onDrop.AddListener(delegate { OnGrabbableGrabEnd(draggableObj); });
        }
    }

    void OnGrabBegin(GameObject grabbable)
    {
        Debug.Log(grabbable);
        UserTask.AddObjectStep(NewStepData.ModeType.GrabObject, grabbable.name);
    }

    void OnGrabbableGrabEnd(GameObject grabbable)
    {
        UserTask.AddObjectStep(NewStepData.ModeType.ReleaseObject, grabbable.name);
    }


#region Perform Steps
    void ExcuteCurrentStep()
    {
        if (s_isFreeMode)
        {
            if (_currentStep < freeManipulateSteps.Length)
                DisplayStep(freeManipulateSteps[_currentStep]);
        }
        else
        {
            // 加入一步驟
            UserTask.AddStepDataToNowTask(_currentStep);

            bool isLastStep;
            if (teachingSteps.Length == 0)
            {
                isLastStep = _currentStep == teachingSteps.Length - 1;
                DisplayStep(teachingSteps[_currentStep]);
            }
            else
            {
                isLastStep = _currentStep == teachingSteps.Length - 1;
                DisplayStep(teachingSteps[_currentStep]);
            }

            if (isLastStep)
            {
                Unlock();
                //menuButton.SetActive(true);
            }
        }
    }

    void DisplayStep(StepBehaviour step)
    {
        if (!step)
        {
            Debug.LogWarning("Step is null");
            return;
        }

        nextButton.SetActive(step.ShowNextButton);
        triviaButton.SetActive(step.ShowTriviaButton);

        LevelManager.Instance.GetTextAndAudioClip(step.TextSetReference.ID, out string _text, out AudioClip clip);
        text.text = _text;

        float clipLength = 0;
        if (clip)
        {
            clipLength = clip.length;
            AudioManager.ins.Play(clip);
        }
        
        hintText.text = "";
        if (_waitDisplayHint != null)
        {
            StopCoroutine(_waitDisplayHint);
            _waitDisplayHint = null;
        }

        if (step.TriggerHintTime.Enable)
        {
            _waitDisplayHint = StartCoroutine(C_ShowHint(clipLength, step));
        }

        step.StartEvent?.Invoke();
    }

    IEnumerator C_ShowHint(float delay, StepBehaviour step)
    {
        
        yield return new WaitForSeconds(delay + step.TriggerHintTime.Value);

        if (triviaControl && triviaControl.IsOn)
        {
            _waitDisplayHint = null;
            yield break;
        }

        if (LevelManager.Instance.TryGetTextAndAudioClip(step.HintTextReference.ID, out string _text, out AudioClip clip))
        {
            hintText.text = _text;
            if (clip)
                AudioManager.ins.Play(clip);
        }

        _waitDisplayHint = null;
    }

    public void ShowHint()
    {
        if (_waitDisplayHint != null)
        {
            StopCoroutine(_waitDisplayHint);
            _waitDisplayHint = null;
        }

        if (LevelManager.Instance.TryGetTextAndAudioClip(teachingSteps[_currentStep].HintTextReference.ID, out string _text, out AudioClip clip))
        {
            hintText.text = _text;
            if (clip)
                AudioManager.ins.Play(clip);
        }
    }

    void EndStep()
    {
        if (_waitDisplayHint != null)
        {
            StopCoroutine(_waitDisplayHint);
            _waitDisplayHint = null;
        }

        if (s_isFreeMode)
        {
            if (_currentStep < freeManipulateSteps.Length)
                freeManipulateSteps[_currentStep].EndEvent.Invoke();
        }
        else
        {
            if (_currentStep < teachingSteps.Length)
                teachingSteps[_currentStep].EndEvent.Invoke();
        }
    }
#endregion


    public void Unlock()
    {
        if (MenuUIEvent.myMenuState == MenuState.FirstTeachingMenu)
            MenuUIEvent.myMenuState = MenuState.TeachingMenu;

        // 把使用者任務紀錄中加入本任務資料
        // UserTask.nowTask.FinishTime = DateTime.Now;
        // UserTask.addNowTaskToList();
        UserTask.AddModeOnlyStep(NewStepData.ModeType.Complete);

        if (AppUser.Userkind == userkind.student)
        {
            UserTask.saveUserTask(UserTask.TaskFileName);
        }
    }


#region Step Control
    public void NextStep()
    {
        EndStep();

        int nextStep = _currentStep + 1;

        if (s_isFreeMode)
        {
            if (freeManipulateSteps[_currentStep].OverrideNextButtonStep.Enable)
                nextStep = freeManipulateSteps[_currentStep].OverrideNextButtonStep.Value;
        }
        else
        {
            if (teachingSteps[_currentStep].OverrideNextButtonStep.Enable)
                nextStep = teachingSteps[_currentStep].OverrideNextButtonStep.Value;
        }

        _currentStep = nextStep;
        if (!s_isFreeMode)
        {
            if (_currentStep < teachingSteps.Length)
                ExcuteCurrentStep();
            else
            {
                text.text = "";
                hintText.text = "";
                
                nextButton.gameObject.SetActive(false);
            }
        }
        else
        {
            if (_currentStep < freeManipulateSteps.Length)
                ExcuteCurrentStep();
            else
            {
                text.text = "";
                hintText.text = "";
                
                nextButton.gameObject.SetActive(false);
            }
        }
    }

    public void SkipStep(int index)
    {
        if (_currentStep == index)
        {
            NextStep();
        }
    }

    public void SkipTeachingStep(int index)
    {
        if (s_isFreeMode)
            return;
        if (_currentStep == index)
        {
            NextStep();
        }
    }

    public void SkipFreeStep(int index)
    {
        if (!s_isFreeMode)
            return;
        if (_currentStep == index)
        {
            NextStep();
        }
    }

    public void driveCurrent()
    {

    }

    public void JumpToStep(int index)
    {
        if (index == -1)
            index = s_isFreeMode ? freeManipulateSteps.Length - 1 : (teachingSteps.Length > 0? teachingSteps.Length - 1: teachingSteps.Length - 1);
        
        if (index == _currentStep)
            return;

        gameObject.SetActive(true);

        EndStep();
        _currentStep = index;
        ExcuteCurrentStep();
    }


    public void ShowNextButton()
    {
        nextButton.SetActive(true);
    }
    public void ShowNextButtonIfStepIndexMatch(int stepIndex)
    {
        if (_currentStep == stepIndex)
            nextButton.SetActive(true);
    }
#endregion


    public void OpenMenu()
    {
        FindAnyObjectByType<MenuUIEvent>().callMenuButton_func();
    }

    public void OnTriviaStart()
    {
        EndStep();

        canvas.enabled = false;
        _triviaComeBackStepIndex = _currentStep + 1;
        triviaControl.StartTrivia(teachingSteps[_currentStep].Question);
    }

    public void StartTrivia(TrviaQuestion trivia)
    {
        EndStep();

        canvas.enabled = false;
        _triviaComeBackStepIndex = _currentStep + 1;
        triviaControl.StartTrivia(trivia);
    }

    public void OnTriviaEnd()
    {
        canvas.enabled = true;
        JumpToStep(_triviaComeBackStepIndex);
    }



    void OnDestroy()
    {
        if (AppUser.Userkind == userkind.student)
        {
            UserTask.AddModeOnlyStep(NewStepData.ModeType.Quit);
            UserTask.saveUserTask(UserTask.TaskFileName);
            UserTask.ClearCurrentStepData();
        }
    }
}
