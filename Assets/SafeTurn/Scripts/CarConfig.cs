using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CarConfig
{
    public string carName;
    public float wheelBase = 2.5f;   // 前後輪距
    public float trackWidth = 1.6f;  // 車寬
    public float speed = 5f;
}