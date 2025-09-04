using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;  // 引用UnityEvent命名空間
using UnityEngine.UI;
public class PutToTriggerEvent : MonoBehaviour
{
    public Sprite newSprite;         // 碰撞後要顯示的新圖片
    private Sprite originalSprite;   // 存儲原始圖片
    private SpriteRenderer spriteRenderer; // 2D/3D物件的 SpriteRenderer

    [Header("Trigger Event")]
    public UnityEvent onWeightCollision; // 定義一個Unity事件

    public bool isBallOnMe;
    public bool isBallDrop;

    void Start()
    {
        // 獲取圖片組件（UI圖片或2D/3D物件）
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 儲存原始圖片
        if (spriteRenderer != null)
        {
            originalSprite = spriteRenderer.sprite;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        // 檢查進入的物件名稱是否為 "砝碼"
        if (other.gameObject.name == "TheBall")
        {
            //other.gameObject.transform.position = gameObject.transform.position + new Vector3(0, 0f, 0);
            isBallOnMe = true;
            //onWeightCollision.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "TheBall")
        {
            isBallOnMe = false;
        }
    }
    public void BallGrab()
    {
        isBallDrop = false;
    }

    public void BallDrop()
    {
        isBallDrop = true;
    }

    void FixedUpdate()
    {
        if (isBallOnMe)
        {
            spriteRenderer.sprite = newSprite;
            if (isBallDrop)
            {
                onWeightCollision.Invoke();
                isBallOnMe = false;
                isBallDrop = false;
            }
        }
        else
        {
            spriteRenderer.sprite = originalSprite;
        }
    }
}
