
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarRunEnd : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI ResultBoardText;

    /*
    public float E1_UserAnswer = 0.0f;
    public float E2_UserAnswer = 0.0f;
    public float E3_UserAnswer = 0.0f;
    public float E4_UserAnswer = 0.0f;
    public float E5_UserAnswer = 0.0f;
    */

    float[] E1_R_Result = { 125.6f, 125.6f, 125.6f, 125.6f, 125.6f };
    float[] E2_R_Result = { 125.6f, 125.6f, 125.6f, 125.6f, 125.6f };
    float[] E3_R_Result = { 125.6f, 125.6f, 125.6f, 125.6f, 125.6f };
    float[] E4_R_Result = { 125.6f, 125.6f, 125.6f, 125.6f, 125.6f };
    float[] E5_R_Result = { 125.6f, 125.6f, 125.6f, 125.6f, 125.6f };
    // float[] carWeightData = { 1000, 1500, 2000, 2500, 3000 };
    // float[] road_0_R = { 10, 30, 50, 80, 100 };

    public CarPathFollower carPathFollower;

    private void Start()
    {
        carPathFollower = gameObject.GetComponent<CarPathFollower>();
    }
    public void ShowResult()
    {
        string nowscenename = SceneManager.GetActiveScene().name;

        if (nowscenename == "E1_Driver_Car")
        {
            //�u��E1�Ψ�
            ResultBoardText.text = "�A�����׬O�G" + AppData.selectCardInputData.ToString() + "����<br>";
            ResultBoardText.text += "<br>";
            ResultBoardText.text += "���T���׬O�G" + E1_R_Result[(int)AppData.selectCard.road_1 - 1] + "����";
        }
        if (nowscenename == "E2_Driver_Car")
        {
            ResultBoardText.text = "�A�����׬O�G" + carPathFollower.CarSpeedKMHr.ToString("#0.0") + "km/hr <br>";
            ResultBoardText.text += "<br>";
            ResultBoardText.text += "���T���t�G" + carPathFollower.rightCarSpeedKMHr.ToString("#0.0") + "km/hr";
        }
        if (nowscenename == "E3_Driver_Car")
        {
            ResultBoardText.text = "�A�����׬O�G" + carPathFollower.CarSpeedKMHr.ToString("#0.0") + "km/hr <br>";
            ResultBoardText.text += "<br>";
            ResultBoardText.text += "���T���t�G" + carPathFollower.rightCarSpeedKMHr.ToString("#0.0") + "km/hr";
            ResultBoardText.text += "<br>";
            ResultBoardText.text += "���T�ɶ��G" + carPathFollower.E3_CarTime.ToString("#0.0") + "��";
        }
        if (nowscenename == "E4_Driver_Car")
        {
            ResultBoardText.text = "�A�����׬O�G" + carPathFollower.CarSpeedKMHr.ToString("#0.0") + "km/hr <br>";
            ResultBoardText.text += "<br>";
            ResultBoardText.text += "���T���t�G" + carPathFollower.rightCarSpeedKMHr.ToString("#0.0") + "km/hr ";
        }
        if (nowscenename == "E5_Driver_Car")
        {
            ResultBoardText.text = "�A�����׬O�G" + carPathFollower.CarSpeedKMHr.ToString("#0.0") + "km/hr <br>";
            ResultBoardText.text += "<br>";
            ResultBoardText.text += "���T���t�G" + carPathFollower.rightCarSpeedKMHr.ToString("#0.0") + "km/hr ";
        }
        if (nowscenename == "E6_Driver_Car")
        {
            ResultBoardText.text = "�A�����׬O�G" + carPathFollower.CarSpeedKMHr.ToString("#0.0") + "km/hr <br>";
            ResultBoardText.text += "<br>";
            ResultBoardText.text += "���T���t�G" + carPathFollower.rightCarSpeedKMHr.ToString("#0.0") + "km/hr ";
        }
        if (nowscenename == "E7_Driver_Car")
        {
            ResultBoardText.text = "�A�����׬O�G" + carPathFollower.CarSpeedKMHr.ToString("#0.0") + "km/hr <br>";
            ResultBoardText.text += "<br>";
            ResultBoardText.text += "���T���t�G" + carPathFollower.rightCarSpeedKMHr.ToString("#0.0") + "km/hr ";
        }
        if (nowscenename == "E8_Driver_Car")
        {
            ResultBoardText.text = "�A�����׬O�G" + carPathFollower.CarSpeedKMHr.ToString("#0.0") + "km/hr <br>";
            ResultBoardText.text += "<br>";
            ResultBoardText.text += "���T���t�G" + carPathFollower.rightCarSpeedKMHr.ToString("#0.0") + "km/hr ";
        }
    }
}
