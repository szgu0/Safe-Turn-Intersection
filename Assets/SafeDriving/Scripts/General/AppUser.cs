using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Collections;
using UnityEngine.Networking;

/* Teacher 所以任務皆可使用，也可以紀錄。
 * Student 要完成教學任務後，才會開啟其他任務
 * 
 *
 */
public enum userkind
{
    nouser = 0,
    admin = 1,
    teacher = 2,
    student = 3
};

public class UserData
{
    public String ID = "";
    public String PW = "";
    public userkind UserKind = userkind.nouser;
}

public class AppUser : MonoBehaviour
{
    //全域變數，可直接讀寫
    [Header("是否為測試：")]
    public bool ForTest = false;

    [Header("是否需要密碼：")]
    public bool needPW = true;

    public static string ClassID = "113-V14";

    public static bool IsLogined = false;
    public static string ID = "";//帳號
    public static string PW = "";//密碼
    public static userkind Userkind = userkind.nouser;//使用者類別

    //所有的帳號
    private static UserData[] UserArray;

    [SerializeField]
    private string FileName = "user.csv";
    // [SerializeField]
    // private String TaskName = "";
    // [SerializeField]
    // private string StartTime = "";
    // [SerializeField]
    // private string FinishTime = "";

    public InputField myUI_ID;
    public InputField myUI_PW;
    public Text myIDPlaceholder;

    public GameObject myLoginView;
    public MenuUIEvent myMenuUIEvent;

    private void Start()
    {
        /* -------------直接登入  For TEST ------------------ */
        TalesFromTheRift.CanvasKeyboard.Close();

        if (ForTest && Userkind == userkind.nouser)
        {
            //以教師身份登入
            myLoginView.SetActive(false);
            Userkind = userkind.teacher;
            //AppData.isGuide = true; //學生一律開啟導引
            myMenuUIEvent.callNewTeachMenu_func();
            return;
        }

        if (Userkind != userkind.nouser)
        {
            myLoginView.SetActive(false);
            myMenuUIEvent.callUnitAllMenu_func();
            return;
        }

        /* ------------正式登入------------------- */
        if (!ForTest && Userkind == userkind.nouser)
        {
            //讀取帳號密碼
            readUserIDPW(FileName);
            //myLoginView.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoginFunction();
        }
    }

    public void LoginFunction()
    {
        //不需要密碼
        if (!needPW)
        {
            ID = myUI_ID.text;
            if (ID == "")
            {
                myUI_ID.text = "";
                if (myUI_PW) myUI_PW.text = "";
                myIDPlaceholder.text = "帳號錯誤，請重新輸入";
                return;
            }

            IsLogined = true;

            UserTask.TaskFileName = ID + ".csv";
            Userkind = userkind.student;
            UserTask.loadUserTask(UserTask.TaskFileName);

            UserTask.PlayerLogin(ID);
            //進入大廳
            myMenuUIEvent.callNewTeachMenu_func();
            myLoginView.SetActive(false);
            return;
        }

        //登入
        if (checkIDPW(myUI_ID.text, myUI_PW.text) == true)
        {
            IsLogined = true;
            Debug.Log("登入成功");
            UserTask.TaskFileName = ID + ".csv";
            if (Userkind == userkind.student)
            {
                UserTask.loadUserTask(UserTask.TaskFileName);
            }

            UserTask.PlayerLogin(ID);

            //進入大廳
            myMenuUIEvent.callNewTeachMenu_func();
            myLoginView.SetActive(false);

            return;
        }

        myUI_ID.text = "";
        myUI_PW.text = "";
        myIDPlaceholder.text = "帳號或密碼錯誤，請重新輸入";
        Debug.Log("登入失敗");
    }

    public void QuitGame()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public static bool logout()
    {
        ID = "";
        PW = "";
        Userkind = userkind.nouser;
        return true;
    }

    private bool checkIDPW(string myid, string mypw)
    {
        //確認帳號密碼
        for (int i = 0; i < UserArray.Length; i++)
        {
            if (myid == UserArray[i].ID)
            {
                if (mypw == UserArray[i].PW)
                {
                    ID = myid;
                    PW = mypw;
                    Userkind = UserArray[i].UserKind;
                    break;
                }
            }
        }

        if (Userkind != userkind.nouser)
        {
            return true;
        }
        return false;
    }

    private void readUserIDPW(string FileName)
    {
        //讀取帳號
        string filePath = Application.streamingAssetsPath + "/Setup/" + FileName;

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
        filePath = "file://" + filePath;
        StartCoroutine(GetRequest(filePath));
#else
        StartCoroutine(GetRequest(filePath));
#endif

        /*
        List<string[]> myUserList = new List<string[]>();
        string line;
        StreamReader stream = new StreamReader(filePath);
        while ((line = stream.ReadLine()) != null)
        {
            line.Trim();
            if (!line.StartsWith("#"))
            {
                myUserList.Add(line.Split(','));
            }
        }
        stream.Close();
        stream.Dispose();


        UserArray = new UserData[myUserList.Count];
        for (int i = 0; i < myUserList.Count; i++)
        {
            UserArray[i] = new UserData();
            UserArray[i].ID = myUserList[i][0];
            UserArray[i].PW = myUserList[i][1];
            switch (Convert.ToInt32(myUserList[i][2]))
            {
                case 0:
                    UserArray[i].UserKind = userkind.nouser;
                    break;
                case 1:
                    UserArray[i].UserKind = userkind.admin;
                    break;
                case 2:
                    UserArray[i].UserKind = userkind.teacher;
                    break;
                case 3:
                    UserArray[i].UserKind = userkind.student;
                    break;
            }
        }
        */
    }

    public void sortUserIDPW(string incontentstring)
    {
        string[] contentstringarray = incontentstring.Split('\n');
        List<string[]> myUserList = new List<string[]>();
        string line;
        for (int i = 0; i < contentstringarray.Length; i++)
        {
            line = contentstringarray[i].Trim();
            if (!line.StartsWith("#"))
            {
                string[] myUser = line.Split(',');
                if (myUser.Length == 3)
                {
                    myUserList.Add(line.Split(','));
                }
            }
        }
        /*
        StreamReader stream = new StreamReader(filePath);
        while ((line = stream.ReadLine()) != null)
        {
            line.Trim();
            if (!line.StartsWith("#"))
            {
                myUserList.Add(line.Split(','));
            }
        }
        stream.Close();
        stream.Dispose();
        */

        UserArray = new UserData[myUserList.Count];
        for (int i = 0; i < myUserList.Count; i++)
        {
            UserArray[i] = new UserData();
            UserArray[i].ID = myUserList[i][0];
            UserArray[i].PW = myUserList[i][1];
            switch (Convert.ToInt32(myUserList[i][2]))
            {
                case 0:
                    UserArray[i].UserKind = userkind.nouser;
                    break;
                case 1:
                    UserArray[i].UserKind = userkind.admin;
                    break;
                case 2:
                    UserArray[i].UserKind = userkind.teacher;
                    break;
                case 3:
                    UserArray[i].UserKind = userkind.student;
                    break;
            }
        }
    }

    string contentstring = "";

    IEnumerator GetRequest(string url)
    {
        //用UnityWebRequest，Android才讀得到Application.streamingAssetsPath
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            //Debug.Log(GetType() + "/UnityWebRequest.Get()/");
            yield return webRequest.SendWebRequest();

            //Debug.Log("webRequest.uploadProgress " + webRequest.uploadProgress);

            if (webRequest.isHttpError || webRequest.isNetworkError)
            {
                Debug.LogError(webRequest.error + "\n" + webRequest.downloadHandler.text);
            }
            else
            {
                string weatherJsonStr = webRequest.downloadHandler.text;
                //Debug.Log(GetType() + "/GetRequest()/ JsonStr : " + weatherJsonStr);

                contentstring = weatherJsonStr;
                sortUserIDPW(contentstring);
            }
        }
    }

    public void testtaskfunction()
    {
        //讀取使用者任務紀錄檔
        // UserTask.loadUserTask("TEST.csv");

        // //讀取特定任務
        // UserTask.createTaskData("L1_1_1");
        // //提定目前任務之開始時間
        // UserTask.nowTask.StartTime = DateTime.Now;
        // //提定目前任務之結束時間
        // UserTask.nowTask.FinishTime = DateTime.Now;
        // //目前任務中加入一步驟資料
        // UserTask.addStepDataToNowTask(1, DateTime.Now);
        // //目前使用者任務紀錄中加入本任務資料
        // UserTask.addNowTaskToList();
        // //儲存使用者任務紀錄檔
        // UserTask.saveUserTask("TEST.csv");


        // TaskName = UserTask.nowTask.TaskName;
        // StartTime = UserTask.nowTask.StartTime.ToString();
        // FinishTime = UserTask.nowTask.FinishTime.ToString();
    }

}
