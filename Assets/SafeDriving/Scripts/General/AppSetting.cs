using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MPack;


// [CreateAssetMenu(menuName="App Setting")]
public class AppSetting : ScriptableObject
{
    public ControlMode Mode
    {
        get
        {
            if (!_initialized)
            {
                // Decide wether the game is using vr
                _initialized = true;

#if UNITY_ANDROID
                _mode = ControlMode.VR;
#endif

#if UNITY_EDITOR
                if (OverrideControlMode.Enable) _mode = OverrideControlMode.Value;
#endif
            }

            return _mode;
        }
    }
    public bool IsVR => Mode == ControlMode.VR;
    public bool IsPC => Mode == ControlMode.PC;

    [System.NonSerialized]
    private ControlMode _mode = ControlMode.PC;
    [System.NonSerialized]
    private bool _initialized = false;

    [Header("Editor Only")]
    [SerializeField]
    private ValueWithEnable<ControlMode> OverrideControlMode;

    
    public enum ControlMode
    {
        PC,
        VR,
    }
}
