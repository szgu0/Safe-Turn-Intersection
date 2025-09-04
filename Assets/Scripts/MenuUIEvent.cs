using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using HTC.UnityPlugin.Vive;
using MPack;

public enum MenuState
{
    Lobby = 0,
    NewTeachingMenu = 1, // 新手教學選單
    FirstTeachingMenu = 2, //第一次教學選單
    TeachingMenu = 3, // 教學選單
    FreeMenu = 4, //自由選單
    ExploreMenu = 5, //探索選單
    GameMenu = 6, //遊戲選單
    ExploreOneMenu = 7, //探索選單 - 單人
    ExploreTwoMenu = 8 //探索選單 - 雙人
};

public class MenuUIEvent : MonoBehaviour
{
    public static MenuState myMenuState = MenuState.TeachingMenu;

    public AppSetting appSettings;

    public GameObject callMenuButton;//設定選單按鈕

    public GameObject Unit_New;//新手教學
    public GameObject Unit_All;//主單元選單
    public GameObject Unit_L;//L
    public GameObject Unit_L_type;//L
    public GameObject Unit_I;//I
    public GameObject Unit_I_type;//I

    public GameObject NewTeachingMenu; // 新手教學選單
    public GameObject FirstTeachingMenu; //第一次教學選單
    public GameObject TeachingMenu; // 教學選單
    public GameObject FreeMenu; //自由選單
    public GameObject ExploreMenu; //探索選單

    public GameObject setting_sound;//設定
    public GameObject setting_out;//設定

    //public GameObject myunit_c;//L3

    public GameObject Unit_Select;
    public GameObject myEventSystem;

    public BoolEventReference menuEvent;

    public bool checkTeachingIsFinished = true;

    public static string nowscenename = "";
    public static string beforscenename = "";
    public static int nowsceneindex = 0;

    //public static string teachscenename = "";
    //public static string explorescenename = "";

    //public static string cilckscenename = "";

    
    private GameObject AllEquipment; //所有器具，開啟選單時關閉器具


    //public bool hasFree = true; //是否有自由模式

    /*
    public static bool isteachtask = true; //是否為教學模式、或自由模式
    public static bool isfirstlearn = true; //是否為第一次進入的場景
    public static bool isfirstEnter = true; //是否為第一次開啟軟體，為開啟新手教學
    
    bool isLoddy = true; //是否為大廳
    */
    public float timescale = 1.0f;

    public GameObject loadingPanel;
    public Slider loadingBar;
    public Text loadingText;

    string gotoscenename = "";
    //讀場景
    public bool LoadLevel(string levelName)
    {
        if (Application.CanStreamedLevelBeLoaded(levelName))
        {
            StartCoroutine(LoadSceneAsync(levelName));
            return true;
        }
        return false;
    }

    IEnumerator LoadSceneAsync(string levelName)
    {
        MenuChange("");
        loadingPanel.SetActive(true);

        AsyncOperation op = SceneManager.LoadSceneAsync(levelName);

        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / .9f);
            //Debug.Log(op.progress);
            loadingBar.value = progress;

            if (loadingText)
                loadingText.text = progress * 100f + "%";

            yield return null;
        }
    }

    private void Awake()
    {
        AllEquipment = GameObject.Find("Equipment");
        if (AllEquipment == null)
        {
            Debug.Log("don't find AllObject");
        }

        beforscenename = nowscenename; //保存前一場景名
        //確定目前的場景名
        nowscenename = SceneManager.GetActiveScene().name;
        nowsceneindex = SceneManager.GetActiveScene().buildIndex;

        if (nowscenename == AppData.MainSceneName)
        {
            //判斷是否在大廳
            myMenuState = MenuState.Lobby;
        }
        else if (nowscenename == "L0-VR" || nowscenename == "L0-PC")
        {
            //判斷是否在新手教學
            myMenuState = MenuState.NewTeachingMenu;
        }
        else
        {

        }

        //建立所有button的事件
        Button[] buttons = this.GetComponentsInChildren<Button>(true);

        for (int i = 0; i < buttons.Length; i++)
        {
            GameObject mybtngb = buttons[i].gameObject;

            buttons[i].onClick.AddListener(delegate ()
            {
                this.OnButtonClick(mybtngb);
            });
        }
    }

    void Start()
    {
        if (appSettings.IsPC)
        {
            if (callMenuButton != null)
            {
                callMenuButton.SetActive(true);
            }
            if (SceneManager.GetActiveScene().name == "Lobby")
            {
                callMenuButton.SetActive(false);
            }
            if (myEventSystem != null)
            {
                myEventSystem.SetActive(true);
            }
        }
        else if (appSettings.IsVR)
        {
            if (callMenuButton != null)
            {
                callMenuButton.SetActive(false);
            }
            if (myEventSystem != null)
            {
                myEventSystem.SetActive(false);
            }
        }

        //開啟語音
        if (AudioPlayerControl.BN_Player != null)
        {
            AudioPlayerControl.BN_Player.enabled = true;
        }
    }

    void Update()
    {
        if (appSettings.IsPC)
        {
            //PC模式，可使用ESC
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //要登入才能呼叫選單，以及不在大廳
                if (AppUser.Userkind == userkind.nouser || SceneManager.GetActiveScene().name == "Lobby")
                    return;

                callMenuButton_func();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //不在大廳時才能用暫停
                if (SceneManager.GetActiveScene().name == "Lobby")
                {
                    return;
                }

                if (timescale == 0.0f)
                {
                    timescale = 1.0f;
                }
                else
                {
                    timescale = 0.0f;
                }
                Time.timeScale = timescale;

            }
                return;
        }
        else
        {
            if (ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.Menu))//|| ViveInput.GetPressDown(HandRole.LeftHand, ControllerButton.Menu))
            {
                //叫出選單
                //要登入才能呼叫選單，以及不在大廳
                if (SceneManager.GetActiveScene().name == "Lobby")
                    return;

                Debug.Log("Press");

                if (AppUser.Userkind == userkind.nouser)
                {
    #if UNITY_EDITOR
                    callMenuButton_func();
    #endif
                    return;
                }

                callMenuButton_func();
            }
        }

        // if (ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.Pad))
        // {
        //     //不在大廳時才能用暫停
        //     if (SceneManager.GetActiveScene().name == "Lobby")
        //     {
        //         return;
        //     }

        //     if (timescale == 0.0f)
        //     {
        //         timescale = 1.0f;
        //     }
        //     else
        //     {
        //         timescale = 0.0f;
        //     }
        //     Time.timeScale = timescale;
        // }

    }

    //確認是否為第一次進入教學
    public bool checkFirstTeach(string scenename)
    {
        //只有學生需要確認
        if (AppUser.Userkind != userkind.student)
        {
            return false;
        }
        if (UserTask.haveTaskData(scenename))
        {
            return false;
        }
        return true;
    }

    //切換選單
    public void MenuChange(string menuname)
    {
        Unit_New.SetActive(false);
        Unit_All.SetActive(false);
        Unit_L.SetActive(false);
        Unit_L_type.SetActive(false);
        Unit_I.SetActive(false);
        Unit_I_type.SetActive(false);
        Unit_Select.SetActive(false);

        FirstTeachingMenu.SetActive(false);
        TeachingMenu.SetActive(false);
        FreeMenu.SetActive(false);
        NewTeachingMenu.SetActive(false);
        ExploreMenu.SetActive(false);

        setting_sound.SetActive(false);
        setting_out.SetActive(false);

        loadingPanel.SetActive(false);

        switch (menuname)
        {
            case "FirstTeachingMenu":
                FirstTeachingMenu.SetActive(true);
                break;
            case "TeachingMenu":
                TeachingMenu.SetActive(true);
                break;
            case "FreeMenu":
                FreeMenu.SetActive(true);
                break;
            case "NewTeachingMenu":
                NewTeachingMenu.SetActive(true);
                break;
            case "ExploreMenu":
                ExploreMenu.SetActive(true);
                break;
            case "Unit_New":
                Unit_New.SetActive(true);
                break;
            case "Unit_Select":
                Unit_Select.SetActive(true);
                break;
            case "Unit_All":
                Unit_All.SetActive(true);
                break;
            case "setting_sound":
                setting_sound.SetActive(true);
                break;
            case "Unit_L":
                Unit_L.SetActive(true);
                break;
            case "Unit_I":
                Unit_I.SetActive(true);
                break;
            case "Unit_L_type":
                Unit_L_type.SetActive(true);
                break;
            case "Unit_I_type":
                Unit_I_type.SetActive(true);
                break;
        }
        beforeMenuName = nowMenuName;
        nowMenuName = menuname;
        Time.timeScale = 1;
    }

    string nowMenuName = "";
    string beforeMenuName = "";
    bool isMenu = false; //選單是否開啟

    //呼叫選單出現
    public void callMenuButton_func()
    {
        isMenu = !isMenu;

        if (isMenu)
        {
            //開啟選單
            //關閉語音
            if (AudioPlayerControl.BN_Player != null)
            {
                AudioPlayerControl.BN_Player.enabled = false;
            }
            //關閉教學道具
            AllEquipment?.SetActive(false);
            Time.timeScale = 0;

            //臨時測試
            string sceneName = SceneManager.GetActiveScene().name;
            if (sceneName.StartsWith("E"))
            {
                myMenuState = MenuState.ExploreMenu;
            }

            //選擇選單
            switch (myMenuState)
            {
                case MenuState.Lobby:
                    MenuChange("Unit_All");
                    break;
                case MenuState.NewTeachingMenu:
                    MenuChange("NewTeachingMenu");
                    break;
                case MenuState.FirstTeachingMenu:
                    MenuChange("FirstTeachingMenu");
                    break;
                case MenuState.TeachingMenu:
                    MenuChange("TeachingMenu");
                    break;
                case MenuState.FreeMenu:
                    MenuChange("FreeMenu");
                    break;
                case MenuState.ExploreMenu:
                    MenuChange("ExploreMenu");
                    break;
                case MenuState.GameMenu:
                    MenuChange("NewTeachingMenu");
                    break;
            }

            menuEvent.Invoke(true);
        }
        else
        {
            menuEvent.Invoke(false);
            // pcMenuBackground.enabled = false;

            //開啟語音
            if (AudioPlayerControl.BN_Player != null)
            {
                AudioPlayerControl.BN_Player.enabled = true;
            }
            //關閉選單
            AllEquipment?.SetActive(true);
            Time.timeScale = 1;
            MenuChange("");
        }
    }

    //呼叫新手教學選單
    public void callNewTeachMenu_func()
    {
        //登入後第一開啟
        MenuChange("Unit_New");
        isMenu = true;
        transform.parent.GetComponent<Canvas>().enabled = true;
    }

    public void callUnitAllMenu_func()
    {
        MenuChange("Unit_All");
        isMenu = true;
        transform.parent.GetComponent<Canvas>().enabled = true;
    }

    public void OnButtonClick(GameObject sender)
    {
        switch (sender.name)
        {
            case "return":
                //繼續
                callMenuButton_func();
                break;
            case "reload":
                LoadLevel(nowscenename);
                Debug.Log("reload");
                break;
            case "off":
                LoadLevel(AppData.MainSceneName);
                Debug.Log("off");
                break;
            case "teach":
                myMenuState = MenuState.TeachingMenu;
                LoadLevel(nowscenename);
                Debug.Log("teach");
                break;
            case "free":
                myMenuState = MenuState.FreeMenu;
                LoadLevel(nowscenename);
                Debug.Log("free");
                break;
            case "explore":
                //myMenuState = MenuState.ExploreMenu;
                //LoadLevel(explorescenename);
                //Debug.Log("explore");
                break;


            case "callMenuButton":

                callMenuButton_func();
                Debug.Log("callMenuButton");
                break;
            case "BTN_Exit":
                //MenuChange("setting_out");
                break;
            case "BTN_OutCancel":
                //MenuChange("Unit_All");
                break;
            case "BTN_OutEnter":
                //離開軟體
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                Debug.Log("out");
                break;

            //Teach
            case "BTN_setting":
                MenuChange("setting_sound");
                Debug.Log("setting_sound");
                break;

            case "BTN_back":

                if (nowMenuName == "setting_sound")
                {
                    AudioPlayerControl.saveAudioSetupFile();
                }

                switch (nowMenuName)
                {
                    case "Unit_New":
                        //離開，回到登入
                        //MenuChange(beforeMenuName);

                        break;
                    case "Unit_L":
                        MenuChange("Unit_All");
                        break;
                    case "Unit_L_type":
                        MenuChange("Unit_L");
                        break;
                    case "Unit_I":
                        MenuChange("Unit_All");
                        break;
                    case "Unit_I_type":
                        MenuChange("Unit_I");
                        break;
                    case "setting_sound":
                        MenuChange(beforeMenuName);
                        //MenuChange("UnitAll");
                        break;
                }

                Debug.Log("back");
                break;

            case "BTN_new":
                //進入新手教學
                if (appSettings.IsVR)
                    LoadLevel("L0-VR");
                else
                    LoadLevel("L0-PC");
                break;
            case "BTN_newteaching":
                //進入新手教學
                if (appSettings.IsVR)
                    LoadLevel("L0-VR");
                else
                    LoadLevel("L0-PC");
                break;
            case "BTN_ignorenew":
                //跳過新手教學
                MenuChange("Unit_All");
                break;

            //L
            case "BTN_Card_L":
                MenuChange("Unit_L");
                Debug.Log("Unit_L");
                break;

            case "BTN_Card_I":
                MenuChange("Unit_I");
                Debug.Log("Unit_I");
                break;
            
            //教學任務
            //L
            case "L1":
            case "L2":
            case "L3":
            case "L4":
            case "L5":
            case "L6":
                gotoscenename = sender.name;
                //LoadLevel(sender.name);
                MenuChange("Unit_L_type");
                Debug.Log(sender.name);
                break;

            //探索任務
            //I
            case "I1":
            case "I2":
            case "I3":
            case "I4":
            case "I5":
            case "I6":
            case "I7":
            case "I8":
                gotoscenename = sender.name;
                //LoadLevel(sender.name);
                //MenuChange("Unit_I_type");
                myMenuState = MenuState.ExploreMenu;
                if (gotoscenename == "I1")
                {
                    gotoscenename = "E1";
                }
                if (gotoscenename == "I2")
                {
                    gotoscenename = "E2";
                }
                if (gotoscenename == "I3")
                {
                    gotoscenename = "E3";
                }
                if (gotoscenename == "I4")
                {
                    gotoscenename = "E4";
                }
                if (gotoscenename == "I5")
                {
                    gotoscenename = "E5";
                }
                if (gotoscenename == "I6")
                {
                    gotoscenename = "E6";
                }
                if (gotoscenename == "I7")
                {
                    gotoscenename = "E7";
                }
                if (gotoscenename == "I8")
                {
                    gotoscenename = "E8";
                }
                LoadLevel(gotoscenename);
                Debug.Log(sender.name);
                break;
            case "Unit_L_one":
                myMenuState = MenuState.TeachingMenu;
                LoadLevel(gotoscenename);
                Debug.Log(sender.name);
                break;
            case "Unit_L_two":
                myMenuState = MenuState.FreeMenu;
                LoadLevel(gotoscenename);
                Debug.Log(sender.name);
                break;
            case "Unit_I_one":
                myMenuState = MenuState.ExploreMenu;
                if(gotoscenename == "I1")
                {
                    gotoscenename = "E1";
                }
                if (gotoscenename == "I2")
                {
                    gotoscenename = "E2";
                }
                if (gotoscenename == "I3")
                {
                    gotoscenename = "E3";
                }
                if (gotoscenename == "I4")
                {
                    gotoscenename = "E4";
                }
                if (gotoscenename == "I5")
                {
                    gotoscenename = "E5";
                }
                if (gotoscenename == "I6")
                {
                    gotoscenename = "E6";
                }
                if (gotoscenename == "I7")
                {
                    gotoscenename = "E7";
                }
                if (gotoscenename == "I8")
                {
                    gotoscenename = "E8";
                }
                LoadLevel(gotoscenename);
                Debug.Log(sender.name);
                break;
            case "Unit_I_two":
                myMenuState = MenuState.ExploreMenu;
                LoadLevel(gotoscenename);
                Debug.Log(sender.name);
                break;
            default:
                break;
        }
    }

}
