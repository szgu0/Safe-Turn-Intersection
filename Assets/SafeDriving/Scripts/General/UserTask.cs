using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using System;
using System.IO;


public struct NewStepData
{
    // private const string s_ProjectID = "112-V09";

    public static NewStepData GetBaseData(string taskName, int index)
    {
        return new NewStepData()
        {
            ProjectID = AppUser.ClassID,
            TaskName = taskName,
            Index = index,
            Time = DateTime.Now,
        };
    }

    public string ProjectID; // 計畫代號
    public string TaskName; // 課程代號 L1-1, L1-1-E
    public int Index; // 任務索引，第幾次任務
    public ModeType Mode;
    public int Step; // 步驟 Index
    public ChoiceType Choice; // 0: A, 1: B, 2: C, 3: D
    public bool Correct; // 是否正確
    public string ObjectName; // 互動的物件名稱
    public DateTime Time; // 時間


    public enum ModeType
    {
        Login, // 專門給登入
        Start,
        Step,
        Trivia,
        GrabObject,
        ReleaseObject,
        ChooseObject,
        Complete,
        Quit,
        Logout,  // 專門給登出
    }

    public enum ChoiceType
    {
        None,
        A,
        B,
        C,
        D,
    }


    public void StringBuilderToCSV(System.Text.StringBuilder sb)
    {
        sb.Append(ProjectID);
        sb.Append(",");
        if (Mode != ModeType.Login && Mode != ModeType.Logout)
            sb.Append(TaskName);
        sb.Append(",");
        if (Mode != ModeType.Login && Mode != ModeType.Logout)
            sb.Append(Index);
        sb.Append(",");
        sb.Append(Mode.ToString());
        sb.Append(",");
        if (Mode == ModeType.Step)
            sb.Append(Step);
        sb.Append(",");
        if (Mode == ModeType.Trivia)
            sb.Append(Choice.ToString());
        sb.Append(",");
        if (Mode == ModeType.Trivia)
            sb.Append(Correct);
        sb.Append(",");
        if (Mode == ModeType.GrabObject || Mode == ModeType.ReleaseObject || Mode == ModeType.ChooseObject || Mode == ModeType.Login)
            sb.Append(ObjectName);
        sb.Append(",");
        sb.Append(Time.ToString());
    }

    public string ToCSV()
    {
        var sb = new System.Text.StringBuilder();
        StringBuilderToCSV(sb);
        return sb.ToString();
    }
}

public struct StepData
{
    //步驟資料：步驟, 時間
    public int StepIndex;
    public DateTime StepTime;
}

public class TaskData
{
    //任務資料
    public string TaskName = "";//任務名稱
    public int TaskIndex = 0;// 任務索引，第幾次任務
    public int TaskState = 0;// 0：第一次進入；1：完成教學任務；
    public DateTime StartTime = DateTime.Now;//教學任務開始時間
    public DateTime FinishTime = DateTime.Now;//教學任務完成時間
    public List<StepData> StepDataList = new List<StepData>();

    public void StringBuilderToCSV(System.Text.StringBuilder sb)
    {
        sb.Append(TaskName);
        sb.Append(",");
        sb.Append(TaskIndex);
        sb.Append(",");
        sb.Append(TaskState);
        sb.Append(",");
        sb.Append(StartTime.ToString());
        sb.Append(",");
        sb.Append(FinishTime.ToString());
        sb.Append(",");

        for (int i = 0; i < StepDataList.Count; i++)
        {
            sb.Append(StepDataList[i].StepIndex);
            sb.Append(",");
            sb.Append(StepDataList[i].StepTime.ToString());
            if (i < StepDataList.Count - 1)
            {
                sb.Append(",");
            }
        }
    }
}

public static class UserTask
{
    //任務檔名
    public static string TaskFileName = "";

    private readonly static Queue<NewStepData> s_stepQueue = new (20);
    private readonly static Dictionary<string, int> s_taskIndexDict = new ();
    private static NewStepData s_currentStepData = new ();


#region Create or Add Task 
    public static void PlayerLogin(string userID)
    {
        NewStepData data = new ()
        {
            ProjectID = AppUser.ClassID,
            Mode = NewStepData.ModeType.Login,
            ObjectName = userID,
            Time = DateTime.Now,
        };

        AddStepData(data);
        saveUserTask(TaskFileName);
    }

    public static void PlayerQuitGame(string userID)
    {
        NewStepData data = new ()
        {
            ProjectID = AppUser.ClassID,
            Mode = NewStepData.ModeType.Logout,
            ObjectName = userID,
            Time = DateTime.Now,
        };

        AddStepData(data);
        saveUserTask(TaskFileName);
    }

    // 開始一個新任務 (當玩家進到其中一個關卡時)
    public static void createTaskData(string taskname)
    {
        int taskIndex = 1;
        if (s_taskIndexDict.ContainsKey(taskname))
            taskIndex = s_taskIndexDict[taskname] + 1;

        s_currentStepData = NewStepData.GetBaseData(taskname, taskIndex);

        AddModeOnlyStep(NewStepData.ModeType.Start);
    }


    private static void AddStepData(NewStepData newStepData)
    {
        s_stepQueue.Enqueue(newStepData);

        if (s_stepQueue.Count >= 20)
            saveUserTask(TaskFileName);

        if (myTcpClient.Instance)
            myTcpClient.Instance.SendDataToServer('@' + newStepData.ToCSV() + '$');
    }

    //目前任務中增加一個步驟
    public static bool AddStepDataToNowTask(int stepIndex)
    {
        if (string.IsNullOrEmpty(s_currentStepData.TaskName))
            return false;

        s_currentStepData.Mode = NewStepData.ModeType.Step;
        s_currentStepData.Step = stepIndex;
        s_currentStepData.Time = DateTime.Now;
        AddStepData(s_currentStepData);
        return false;
    }

    public static bool AddModeOnlyStep(NewStepData.ModeType modeType)
    {
        if (string.IsNullOrEmpty(s_currentStepData.TaskName))
            return false;

        s_currentStepData.Mode = modeType;
        s_currentStepData.Step = 0;
        AddStepData(s_currentStepData);
        return false;
    }

    public static void AddTriviaStep(NewStepData.ChoiceType choiceType, bool correct)
    {
        if (string.IsNullOrEmpty(s_currentStepData.TaskName))
            return;

        s_currentStepData.Mode = NewStepData.ModeType.Trivia;
        s_currentStepData.Choice = choiceType;
        s_currentStepData.Correct = correct;
        s_currentStepData.Time = DateTime.Now;
        AddStepData(s_currentStepData);
    }

    public static void AddObjectStep(NewStepData.ModeType modeType, string objectName)
    {
        if (string.IsNullOrEmpty(s_currentStepData.TaskName))
            return;

        s_currentStepData.Mode = modeType;
        s_currentStepData.ObjectName = objectName;
        s_currentStepData.Time = DateTime.Now;
        AddStepData(s_currentStepData);
    }
#endregion


    //任務是否存在
    public static bool haveTaskData(string taskname)
    {
        return s_taskIndexDict.ContainsKey(taskname);
    }

    public static void ClearCurrentStepData()
    {
        s_currentStepData = new ();
    }


#region Load or Save Task
    static bool CheckFileExist(string fileName, out string filePath)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            filePath = "";
            return false;
        }

        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                filePath = Application.persistentDataPath + "/" + fileName;
                break;
            default:
                filePath = Application.streamingAssetsPath + "/" + fileName;
                break;
        }

        return File.Exists(filePath);
    }

    //讀取紀錄檔
    public static bool loadUserTask(string FileName)
    {
        if (!CheckFileExist(FileName, out string filePath))
            return false;


        //清空
        // TaskList.Clear();

        s_taskIndexDict.Clear();

        // List<string[]> stringlist = new List<string[]>();
        string line;
        StreamReader stream = new StreamReader(filePath);

        while ((line = stream.ReadLine()) != null)
        {
            line.Trim();
            if (line.StartsWith("#"))
                continue;

            string[] elements = line.Split(',');
            string taskName = elements[1];

            // Debug.Log(elements[2]);
            // int taskIndex = int.Parse(elements[2]);

            if (int.TryParse(elements[2], out int taskIndex))
            {
                if (s_taskIndexDict.ContainsKey(taskName))
                {
                    if (s_taskIndexDict[taskName] < taskIndex)
                        s_taskIndexDict[taskName] = taskIndex;
                }
                else
                {
                    s_taskIndexDict.Add(taskName, taskIndex);
                }
            }
        }
        stream.Close();
        stream.Dispose();

        return true;
    }


    //儲存紀錄檔
    public static bool saveUserTask(string FileName)
    {
        if (s_stepQueue.Count == 0)
            return false;

        var sb = new System.Text.StringBuilder();

        if (!CheckFileExist(FileName, out string filePath))
        {
            if (filePath == "")
                return false;

            File.CreateText(filePath).Dispose();


            sb.Append("#ID:" + AppUser.ID);
            sb.AppendLine();
            sb.Append("#CLASS:" + AppUser.ClassID);
            sb.AppendLine();
        }

        Stream stream = new FileStream(filePath, FileMode.Append, FileAccess.Write);
        StreamWriter streamWriter = new StreamWriter(stream, System.Text.Encoding.UTF8);

        if (sb.Length > 0)
        {
            streamWriter.Write(sb.ToString());
            sb.Clear();
        }

        NewStepData newStepData;
        while (s_stepQueue.Count > 0)
        {
            newStepData = s_stepQueue.Dequeue();
            newStepData.StringBuilderToCSV(sb);
            streamWriter.WriteLine(sb.ToString());
            sb.Clear();
        }


        streamWriter.Close();
        streamWriter.Dispose();


#if UNITY_ANDROID
        //String sourcePath = Application.persistentDataPath + "/" + FileName;
        //string[] temp = (Application.persistentDataPath.Replace("Android", "")).Split(new string[] { "//" }, System.StringSplitOptions.None);
        //String destinationPath = temp[0] + "/Download/" + FileName;
        //File.Copy(sourcePath, destinationPath, true);
#endif


        return true;
    }
#endregion
}
