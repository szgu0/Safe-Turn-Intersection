using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CarManager : MonoBehaviour
{
    public CarController carCarController, vanCarController, busCarController;
    public TextMeshProUGUI wheelBaseText;

    public string nowCar;
    public bool isShowPath, isShowTri;

    void Start()
    {
        busCarController.PreDrawPath();
        busCarController.TogglePath(isShowPath);
        Debug.Log("bus");
    }

    public void CarsSetSteeringAngle(Toggle toggle)
    {
        if (!toggle.isOn) return;
        float Angle = float.Parse(toggle.name.Split("_")[1]);
        carCarController.SetSteeringAngle(Angle);
        vanCarController.SetSteeringAngle(Angle);
        busCarController.SetSteeringAngle(Angle);
    }

    public void chooseCar(Toggle toggle)
    {
        if (!toggle.isOn) return;
        nowCar = toggle.name.Split("_")[1];
        switch (nowCar)
        {
            case "car":
                carCarController.gameObject.SetActive(true);
                vanCarController.gameObject.SetActive(false);
                busCarController.gameObject.SetActive(false);
                carCarController.PreDrawPath();
                carCarController.TogglePath(isShowPath);
                carCarController.TogglePath(isShowTri);
                wheelBaseText.text = carCarController.config.wheelBase + " m";
                break;
            case "van":
                carCarController.gameObject.SetActive(false);
                vanCarController.gameObject.SetActive(true);
                busCarController.gameObject.SetActive(false);
                vanCarController.PreDrawPath();
                vanCarController.TogglePath(isShowPath);
                vanCarController.TogglePath(isShowTri);
                wheelBaseText.text = vanCarController.config.wheelBase + " m";
                break;
            case "bus":
                carCarController.gameObject.SetActive(false);
                vanCarController.gameObject.SetActive(false);
                busCarController.gameObject.SetActive(true);
                busCarController.PreDrawPath();
                busCarController.TogglePath(isShowPath);
                busCarController.TogglePath(isShowTri);
                wheelBaseText.text = busCarController.config.wheelBase + " m";
                break;
            default:
                break;
        }
    }

    public void StartPlay()
    {
        switch (nowCar)
        {
            case "car":
                carCarController.StartMoving();
                break;
            case "van":
                vanCarController.StartMoving();
                break;
            case "bus":
                busCarController.StartMoving();
                break;
            default:
                break;
        }
    }

    public void ShowPath(Toggle change)
    {
        isShowPath = change.isOn;
        carCarController.TogglePath(isShowPath);
        vanCarController.TogglePath(isShowPath);
        busCarController.TogglePath(isShowPath);

    }
    public void ShowTri(Toggle change)
    {
        isShowTri = change.isOn;
        carCarController.ToggleTri(isShowTri);
        vanCarController.ToggleTri(isShowTri);
        busCarController.ToggleTri(isShowTri);

    }
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
