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
    public GameObject CrashUI;
    public GameObject MainUI;

    public string nowCar;
    public bool isShowPath, isShowTri;

    [Header("環境光設定")]
    public Color targetColor = new Color(0.4f, 0.1f, 0.1f); // 偏紅的顏色
    public float targetIntensity = 0.4f;  // 變暗程度（越小越暗）
    public float transitionTime = 2.0f;   // 變化所需秒數
    public Light sceneLight;

    public GameObject RedLight;

    private Color originalColor;
    private float originalIntensity;
    private Coroutine currentRoutine;

    public AudioSource audioSource;
    public AudioClip ambulanceClip;

    void Start()
    {
        busCarController.PreDrawPath();
        busCarController.TogglePath(isShowPath);
        Debug.Log("bus");

        originalColor = RenderSettings.ambientLight;
        originalIntensity = RenderSettings.ambientIntensity;

        RedLight.SetActive(false);
    }

    public void CarCrashs()
    {
        CrashUI.SetActive(true);
        MainUI.SetActive(false);
        carCarController.gameObject.SetActive(false);
        vanCarController.gameObject.SetActive(false);
        busCarController.gameObject.SetActive(false);

        audioSource.PlayOneShot(ambulanceClip);

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(ChangeAmbientLight(targetColor, targetIntensity));
    }

    IEnumerator ChangeAmbientLight(Color targetCol, float targetInt)
    {
        Color startColor = RenderSettings.ambientLight;
        Color scenestartColor = sceneLight.color;
        float startIntensity = RenderSettings.ambientIntensity;

        float timer = 0f;
        while (timer < transitionTime)
        {
            timer += Time.deltaTime;
            float t = timer / transitionTime;

            RenderSettings.ambientLight = Color.Lerp(startColor, targetCol, t);
            RenderSettings.ambientIntensity = Mathf.Lerp(startIntensity, targetInt, t);

            sceneLight.color = Color.Lerp(scenestartColor, targetCol, t);

            yield return null;
        }

        RenderSettings.ambientLight = targetCol;
        RenderSettings.ambientIntensity = targetInt;
        RedLight.SetActive(true);
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
                carCarController.frontWheelLine.enabled = isShowPath;
                carCarController.rearWheelLine.enabled = isShowPath;
                wheelBaseText.text = carCarController.config.wheelBase + " m";
                break;
            case "van":
                carCarController.gameObject.SetActive(false);
                vanCarController.gameObject.SetActive(true);
                busCarController.gameObject.SetActive(false);
                vanCarController.PreDrawPath();
                vanCarController.TogglePath(isShowPath);
                vanCarController.TogglePath(isShowTri);
                vanCarController.frontWheelLine.enabled = isShowPath;
                vanCarController.rearWheelLine.enabled = isShowPath;
                wheelBaseText.text = vanCarController.config.wheelBase + " m";
                break;
            case "bus":
                carCarController.gameObject.SetActive(false);
                vanCarController.gameObject.SetActive(false);
                busCarController.gameObject.SetActive(true);
                busCarController.PreDrawPath();
                busCarController.TogglePath(isShowPath);
                busCarController.TogglePath(isShowTri);
                busCarController.frontWheelLine.enabled = isShowPath;
                busCarController.rearWheelLine.enabled = isShowPath;
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

    public void GoToDrive()
    {
        SceneManager.LoadScene(1);
    }


}
