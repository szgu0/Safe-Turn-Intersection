using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;  // 引用UnityEvent命名空間
using UnityEngine.UI; 

public class ChangeImageAndTriggerEvent : MonoBehaviour
{
    public Sprite newSprite;         // 碰撞後要顯示的新圖片
    private Sprite originalSprite;   // 存儲原始圖片
    private Image imageComponent;    // UI圖片的 Image 組件
    private SpriteRenderer spriteRenderer; // 2D/3D物件的 SpriteRenderer

    [Header("Trigger Event")]
    public UnityEvent onWeightCollision; // 定義一個Unity事件

    void Start()
    {
        // 獲取圖片組件（UI圖片或2D/3D物件）
        imageComponent = GetComponent<Image>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 儲存原始圖片
        if (imageComponent != null)
        {
            originalSprite = imageComponent.sprite;
        }
        else if (spriteRenderer != null)
        {
            originalSprite = spriteRenderer.sprite;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 檢查進入的物件名稱是否為 "砝碼"
        if (other.gameObject.name == "Wei")
        {
            // 改變圖片為指定的新圖片
            if (imageComponent != null)
            {
                imageComponent.sprite = newSprite;
            }
            else if (spriteRenderer != null)
            {
                spriteRenderer.sprite = newSprite;
            }
            other.gameObject.transform.position = gameObject.transform.position + new Vector3(0,0.05f,0);
            // 觸發事件
            onWeightCollision.Invoke();
        }
    }

    // void OnTriggerExit(Collider other)
    // {
    //     // 當 "砝碼" 離開時恢復原始圖片
    //     if (other.gameObject.name == "Wei")
    //     {
    //         if (imageComponent != null)
    //         {
    //             imageComponent.sprite = originalSprite;
    //         }
    //         else if (spriteRenderer != null)
    //         {
    //             spriteRenderer.sprite = originalSprite;
    //         }
    //     }
    // }
}
