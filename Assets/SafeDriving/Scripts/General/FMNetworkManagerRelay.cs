using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMNetworkManagerRelay : MonoBehaviour
{
    private FMNetworkManager manager {
        get {
            if (_manager == null)
                _manager = FMNetworkManager.instance;
            if (_manager == null)
                _manager = FindObjectOfType<FMNetworkManager>();
            return _manager;
        }
    }
    private FMNetworkManager _manager;

    public UnityEventString OnReceivedStringDataEvent;

    public void SendToAll(byte[] _byteData) { manager?.SendToAll(_byteData); }
    public void SendToServer(byte[] _byteData) { Debug.Log(manager); manager?.SendToServer(_byteData); }
    public void SendToOthers(byte[] _byteData) { manager?.SendToOthers(_byteData); }

    [SerializeField]
    private AppSetting appSetting;
    private GameViewEncoder _GameViewEncoder;

    void Start()
    {
        _manager = FMNetworkManager.instance;
        _GameViewEncoder = transform.Find("GameViewEncoder").GetComponent<GameViewEncoder>();

        if (appSetting.IsPC && _GameViewEncoder != null)
        {
            _GameViewEncoder.CaptureMode = GameViewCaptureMode.FullScreen;
        }
        if (appSetting.IsVR && _GameViewEncoder != null)
        {
            _GameViewEncoder.CaptureMode = GameViewCaptureMode.RenderCam;
            _GameViewEncoder.RenderCam = GameObject.Find("NetworkCamera").GetComponent<Camera>();
        }

        _GameViewEncoder?.OnDataByteReadyEvent.AddListener(manager.SendToServer);
    }

    void OnEnable()
    {
        manager?.OnReceivedStringDataEvent.AddListener(OnReceived);
    }

    void OnDisable()
    {
        manager?.OnReceivedStringDataEvent.RemoveListener(OnReceived);
    }

    void OnReceived(string message)
    {
        OnReceivedStringDataEvent.Invoke(message);
    }
}
