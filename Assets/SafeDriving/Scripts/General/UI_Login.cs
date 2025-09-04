using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Login : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField myUI_ID;
    public InputField myUI_PW;
//    public TalesFromTheRift.CanvasKeyboard mycanvasKeyboard;

    void Start()
    {
        myUI_ID.Select();
//        mycanvasKeyboard.inputObject = myUI_ID.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (myUI_ID.isFocused)
            {
                myUI_PW.Select();
            }
            else if (myUI_PW.isFocused)
            {
                myUI_ID.Select();
            }
        }
    }


}
