using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class myClientData : MonoBehaviour
{
    public Text IPtext;
    public Text mytext;
    public InputField myinput;
    //public myTcpClient myclient;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IPtext.text = myTcpClient.Instance.ipAddress;
    }

    public void senddata()
    {
        //myTcpClient.Instance.SendDataToServer(myinput.text);
        myTcpClient.Instance.SendDataToServer("@1, TaskName, 1, L1-1$");
        myTcpClient.Instance.SendDataToServer("@1,StartTime,1,2022/6/1 ¤W¤È 11:06:25$");
        myTcpClient.Instance.SendDataToServer("@1,StepData,1,2022/6/3 ¤W¤È 11:06:25$");
        //myclient.ToEvent(myclient.ID, myinput.text);
    }
    public void mydata(string a)
    {
        mytext.text = a;
        Debug.Log(a);
    }

    public void myevent(string s, string a)
    {
        mytext.text = s+":"+a;
        Debug.Log(a);

    }
}
